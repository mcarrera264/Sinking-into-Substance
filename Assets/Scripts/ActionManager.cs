using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public FadeImage screenFader;
    public ActionTrigger actionTrigger;
    public BottomBarController bbc;
    public bool playerInteraction = true;

    public FadeImage targetImage;
    public FadeImage debugImage;
    public FadeImage imageM, imageR, imageL, imageMR, imageML;
    

    public void ApplyPosition(string s)
    {
        FadeImage fi = debugImage;
        if (s == "Middle") fi = imageM;
        else if (s == "Right") fi = imageR;
        else if (s == "Left") fi = imageL;
        else if (s == "MiddleRight") fi = imageMR;
        else if (s == "MiddleLeft") fi = imageML;

        if (targetImage != fi)
        {
            FadeOut(targetImage);
            targetImage = fi;
            FadeIn(targetImage);

        }
    }

    public void ApplyCharacterSprite(Sprite sprite)
    {
        targetImage.targetImage.sprite = sprite;
    }

    public void ApplyDisplayMethod(string s)
    {
        if (s == "Stay")
            return;
        else if (s == "MoveIn")
            FadeIn(targetImage);
        else if (s == "MoveOut")
        {
            FadeOut(targetImage);
            print("movingOut");

        }
    }

    public void ApplyDisplayDirection(bool b)
    {
        Vector3 flipScale;

        if (b)
            flipScale = new Vector3(-1,1,1);
        else
            flipScale = new Vector3(1, 1, 1);

        targetImage.targetImage.rectTransform.localScale = flipScale;

    }

    public void ApplyFollowUpMethod(string s)
    {
        if (s == "EnableInteraction")
            EnableInteraction();
        else if (s == "FadeIn")
            FadeIn(screenFader);
        else if (s == "FadeOut")
            FadeOut(screenFader);
        else if (s == "CallNextAction")
            CallNextAction();
        else if (s == "Decide")
            Decide();
    }

    public void EnableInteraction()
    {
        playerInteraction = true;
    }

    public void CallNextAction()
    {
        StartCoroutine(CallNextActionCoroutine());

    }

    public void Decide()
    {
        //bbc.barText.text = "";
        actionTrigger.decisionButtons.SetActive(true);

    }

    public void FadeIn(FadeImage fi)
    {
        StartCoroutine(FadeInCoroutine(fi));
    }

    public void FadeOut(FadeImage fi)
    {
        StartCoroutine(FadeOutCoroutine(fi));

    }

    private IEnumerator FadeInCoroutine(FadeImage fi)
    {
        // Fade out
        fi.FadeIn();

        // Wait for fading to complete
        while (fi.isFading)
        {
            yield return null;
        }
    }

    private IEnumerator FadeOutCoroutine(FadeImage fi)
    {
        // Fade out
        fi.FadeOut();

        // Wait for fading to complete
        while (fi.isFading)
        {
            yield return null;
        }
    }

    public IEnumerator CallNextActionCoroutine()
    {
        yield return new WaitForSeconds(0.75f);
        bbc.PlayNextAction();

    }
}
