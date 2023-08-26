using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public ActionTrigger currentActionTrigger;
    public ActionManager actionManager;
    public BottomBarController bottomBar;
    //public BackgroundController backgroundController;
    public AudioManager audioManager;
    public Image backgorundImage;

    public bool autoClicker = false;
    public float autoClickSpeed = 2f;
    private float clockAuto = 0;


    void Start()
    {
        bottomBar.PlayAction(currentActionTrigger);
        backgorundImage.sprite = currentActionTrigger.background;
    }

    void Update()
    {    
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (bottomBar.IsCompleted() && actionManager.playerInteraction)
            {
                CheckForNextAction();
            }
            else
            {
                bottomBar.Faster();
            }
        }

        if (autoClicker)
        {
            clockAuto += Time.deltaTime;
            if (clockAuto >= autoClickSpeed)
            {
                if (bottomBar.IsCompleted() && actionManager.playerInteraction && clockAuto >= autoClickSpeed)
                {
                    clockAuto = 0;

                    CheckForNextAction();
                }
                clockAuto = 0;

            }
        }
    }

    public void CheckForNextAction()
    {
        actionManager.playerInteraction = false;

        if (bottomBar.IsLastSentence())
        {
            ChangeActionTrigger(currentActionTrigger.nextActionTrigger);
        }

        bottomBar.PlayNextAction();
        
    }
    
    public void ChangeActionTrigger(ActionTrigger at)
    {
        currentActionTrigger = at;
        backgorundImage.sprite = currentActionTrigger.background;
        audioManager.SwitchMusic(currentActionTrigger.locationMusic);
        bottomBar.PlayAction(currentActionTrigger);
    }
}
