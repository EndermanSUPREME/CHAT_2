using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueObject")]
public class DialogObject : ScriptableObject
{
    public string AI_Dialogue;
    public DialogueResponses[] playerResponces = new DialogueResponses[3];
    DialogObject nextDialogueObject;
    public bool isThisTheEnd;

//==============================================================================================
//==============================================================================================
    // click events
    public string PlayerPicksResponse_1()
    {
        nextDialogueObject = playerResponces[0].nextDialogueObject;
        isThisTheEnd = playerResponces[0].isEndOfDialogueBranch;

        return playerResponces[0].playerResponse;
    }

    public string PlayerPicksResponse_2()
    {
        nextDialogueObject = playerResponces[1].nextDialogueObject;
        isThisTheEnd = playerResponces[1].isEndOfDialogueBranch;

        return playerResponces[1].playerResponse;
    }

    public string PlayerPicksResponse_3()
    {
        nextDialogueObject = playerResponces[2].nextDialogueObject;
        isThisTheEnd = playerResponces[2].isEndOfDialogueBranch;

        return playerResponces[2].playerResponse;
    }
    // Hover Events
    public string ShowChoiceOne()
    {
        return playerResponces[0].playerResponse;
    }

    public string ShowChoiceTwo()
    {
        return playerResponces[1].playerResponse;
    }

    public string ShowChoiceThree()
    {
        return playerResponces[2].playerResponse;
    }

//==============================================================================================
//==============================================================================================

    public DialogObject GetNextObject()
    {
        return nextDialogueObject;
    }

    public bool CheckIfDialogueEnded()
    {
        return isThisTheEnd;
    }
}

[System.Serializable]
public class DialogueResponses
{
    public string playerResponse;
    public DialogObject nextDialogueObject;

    // public UnityEvent gameEvent = new UnityEvent();

    public bool isEndOfDialogueBranch = false;
}