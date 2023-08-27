using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;

    public float drawSpeed = 5;
    public float defaultDrawSpeed = 5;
    public bool fast;
    public bool faster;

    private int actionIndex = -1;
    private ActionTrigger actionTrigger;
    public ActionManager actionManager;
    public AudioManager audioManager;
    private State state = State.COMPLETED;

    private enum State
    {
        PLAYING, COMPLETED
    }


    public void PlayAction(ActionTrigger at)
    {
        actionTrigger = at;
        actionManager.actionTrigger = at;
        actionIndex = -1;

    }

    public void PlayNextAction()
    {
        // This triggers all code developed while reading a new Line
        StartCoroutine(TypeText(actionTrigger.actions[++actionIndex].displayText));

        SetUpSpeaker(actionTrigger.actions[actionIndex].speaker);
        //barText.color = actionTrigger.actions[actionIndex].speaker.textColor;
        //personNameText.text = actionTrigger.actions[actionIndex].speaker.speakerName;

        actionManager.ApplyPosition(actionTrigger.GetPositionIndex(actionTrigger.actions[actionIndex]));
        actionManager.ApplyCharacterSprite(actionTrigger.actions[actionIndex].displaySprite);
        //actionManager.ApplyDisplayMethod(actionTrigger.GetDisplayMethodIndex(actionTrigger.actions[actionIndex]));
        actionManager.ApplyDisplayDirection(actionTrigger.actions[actionIndex].displayDirection);

        SetDefaultLineSpeed();
    }

    public void SetUpSpeaker(Speaker speaker)
    {
        personNameText.text = speaker.speakerName;
        personNameText.color = speaker.nameColor;
        barText.color = speaker.textColor;

        audioManager.SetAudioMixer(speaker.voiceMixerGroup);

        if(speaker.bold)
            barText.fontStyle = FontStyles.Bold;
        else if (speaker.italic)
            barText.fontStyle = FontStyles.Italic;
        else
            barText.fontStyle = FontStyles.Normal;

    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        return actionIndex + 1 == actionTrigger.actions.Count;
    }

    private IEnumerator TypeText(string text)
    {
        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            if(text.Length < 1)
            {
                actionManager.ApplyFollowUpMethod(actionTrigger.GetFollowUpMethodIndex(actionTrigger.actions[actionIndex]));

                state = State.COMPLETED;
                break;

            }

            char currentChar = text[wordIndex];
            barText.text += currentChar;

            if (currentChar == ' ' && text.Length > 3)
            {
                if (text[wordIndex-1] == '.' || text[wordIndex - 1] == '?')
                {
                    if (text[wordIndex - 2] == '.')
                        yield return new WaitForSeconds(0.1f);
                    else if (text[wordIndex - 2] == 'r')
                        yield return new WaitForSeconds(0.01f);
                    else
                        yield return new WaitForSeconds(0.35f);
                }

                if (text[wordIndex-1] == ',' || text[wordIndex - 1] == '|')
                {
                    yield return new WaitForSeconds(.1f);
                }
            }
            else
            {
                audioManager.PlayRandomClick();
            }

            yield return new WaitForSeconds(drawSpeed/100);

            if(++wordIndex == text.Length)
            {
                // This triggers all code developed while ending a new Line
                actionManager.ApplyFollowUpMethod(actionTrigger.GetFollowUpMethodIndex(actionTrigger.actions[actionIndex]));

                state = State.COMPLETED;
                break;
            }
        }
    }

    public void Faster()
    {
        if (!fast)
        {
            fast = true;
            drawSpeed /= 2;

        }
        else if (!faster)
        {
            faster = true;
            drawSpeed /= 2;

        }
    }

    public void SetDefaultLineSpeed()
    {
        drawSpeed = defaultDrawSpeed;
        fast = false;
        faster = false;

    }    
}
