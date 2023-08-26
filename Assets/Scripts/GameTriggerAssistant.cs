using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTriggerAssistant : MonoBehaviour
{
    public ActionManager am;
    public bool working;

    private void Awake()
    {
        if (working)
        am.playerInteraction = false;

    }

    public void StartGame()
    {
        am.playerInteraction = false;
        am.bbc.PlayNextAction();

    }
}
