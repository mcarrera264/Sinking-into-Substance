using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionTrigger : MonoBehaviour
{
    public List<Action> actions;
    public Sprite background;
    public AudioClip locationMusic;
    public ActionTrigger nextActionTrigger;
    public GameObject decisionButtons;

    //public enum Character { Sean, Jacqueline, Teacher, Carlos, Mom, Narrator, Unknown };
    public enum Position { NONE, Left, MiddleLeft, Middle, MiddleRight, Right, CloseUp };
    public enum DisplayMethod { Stays, MovesIn, MovesOut}
    public enum FollowUpMethod { EnableInteraction, FadeIn, FadeOut, CallNextAction, Decide };

    public string GetFollowUpMethodIndex(Action action)
    {
        string s = "EnableInteraction";
        if (action.followUp == FollowUpMethod.EnableInteraction)
            s = "EnableInteraction";
        else if (action.followUp == FollowUpMethod.FadeIn)
            s = "FadeIn";
        else if (action.followUp == FollowUpMethod.FadeOut)
            s = "FadeOut";
        else if (action.followUp == FollowUpMethod.CallNextAction)
            s = "CallNextAction";
        else if (action.followUp == FollowUpMethod.Decide)
            s = "Decide";

        return s;
    }

    public string GetPositionIndex(Action action)
    {
        string s = "NONE";
        if (action.displayPosition == Position.Middle)
            s = "Middle";
        else if (action.displayPosition == Position.Right)
            s = "Right";
        else if (action.displayPosition == Position.Left)
            s = "Left";
        else if (action.displayPosition == Position.MiddleRight)
            s = "MiddleRight";
        else if (action.displayPosition == Position.MiddleLeft)
            s = "MiddleLeft";

        return s;
    }

    //public string GetDisplayMethodIndex(Action action)
    //{
    //    string s = "Stays";
    //    if (action.displayMethod == DisplayMethod.Stays)
    //        s = "Stays";
    //    else if (action.displayMethod == DisplayMethod.MovesIn)
    //        s = "MoveIn";
    //    else if (action.displayMethod == DisplayMethod.MovesOut)
    //        s = "MoveOut";

    //    return s;
    //}

    [System.Serializable]
    public struct Action
    {
        [Header("Text")]
        public string displayText;
        public Speaker speaker;

        [Header("Graphic")]
        //public string DevInfo_G;
        public Sprite displaySprite;
        //DisplayMethod displayMethod;
        public Position displayPosition;
        public bool displayDirection;

        //[Header("Sound")]
        //public string DevInfo_S;
        //public AudioClip SFX;

        [Header("Configuration")]
        public FollowUpMethod followUp;
    }
    
}
