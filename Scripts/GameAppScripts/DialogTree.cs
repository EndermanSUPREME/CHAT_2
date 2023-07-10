using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using myTree;

namespace myTree
{
    public class DialogInfo
    {
        public static int nB_ID;
    }
}

public class DialogTree : MonoBehaviour
{
    [SerializeField] DialogBranch[] Branches;
    bool inputAllowed = true;
    int currentBranchIndex = 0, n_B;
    string aiDialogue;

    void Start()
    {
        aiDialogue = Branches[currentBranchIndex].AI_Dialogue;
    }

    public string Get_AI_Dialogue()
    {
        return aiDialogue;
    }

    public bool isEndOfTree()
    {
        return Branches[currentBranchIndex].EndOfTree;
    }

    public DialogBranch GetCurrentBranch()
    {
        return Branches[currentBranchIndex];
    }

//=========================== CLICK ==================================
    public string Select_Player_Dialogue()
    {
        if (inputAllowed)
        {
            if (!Branches[currentBranchIndex].EndOfTree)
            {
                Invoke("AI_Reaction", 2f);
                Invoke("MoveBranch", 2.01f);
                Invoke("AllowUserInput", 5);
                inputAllowed = false;
    
                aiDialogue = "Typing. . .";
            } else
                {
                    aiDialogue = "User Has Left. . .";
                }
        }

        n_B = Branches[currentBranchIndex].responses[0].nextBranch;

        return Branches[currentBranchIndex].responses[0].playerResponse;
    }

    public string Select_Player_Dialogue2()
    {
        if (inputAllowed)
        {
            if (!Branches[currentBranchIndex].EndOfTree)
            {
                Invoke("AI_Reaction", 2f);
                Invoke("MoveBranch", 2.01f);
                Invoke("AllowUserInput", 5);
                inputAllowed = false;
    
                aiDialogue = "Typing. . .";
            } else
                {
                    aiDialogue = "User Has Left. . .";
                }
        }
        
        n_B = Branches[currentBranchIndex].responses[1].nextBranch;

        return Branches[currentBranchIndex].responses[1].playerResponse;
    }

    public string Select_Player_Dialogue3()
    {
        if (inputAllowed)
        {
            if (!Branches[currentBranchIndex].EndOfTree)
            {
                Invoke("AI_Reaction", 2f);
                Invoke("MoveBranch", 2.01f);
                Invoke("AllowUserInput", 5);
                inputAllowed = false;
    
                aiDialogue = "Typing. . .";
            } else
                {
                    aiDialogue = "User Has Left. . .";
                }
        }

        n_B = Branches[currentBranchIndex].responses[2].nextBranch;

        return Branches[currentBranchIndex].responses[2].playerResponse;
    }

//===============================================================================================

    public string Get_Player_Dialogue()
    {
        return Branches[currentBranchIndex].responses[0].playerResponse;
    }

    public string Get_Player_Dialogue2()
    {
        return Branches[currentBranchIndex].responses[1].playerResponse;
    }
    
    public string Get_Player_Dialogue3()
    {
        return Branches[currentBranchIndex].responses[2].playerResponse;
    }

//=======================================================

    void MoveBranch()
    {
        currentBranchIndex = myTree.DialogInfo.nB_ID;
        aiDialogue = Branches[currentBranchIndex].AI_Dialogue;
    }

    void AllowUserInput()
    {
        inputAllowed = true;
    }

    void AI_Reaction()
    {
        myTree.DialogInfo.nB_ID = n_B;
    }
}

[System.Serializable]
public class DialogBranch
{
    public string BranchName;

    public DialogResponse[] responses;
    public bool EndOfTree;

    public string AI_Dialogue;
}

[System.Serializable]
public class DialogResponse
{
    public int nextBranch;
    public string playerResponse;
    public UnityEvent gameEvent; // on choice select if there is an event we will gameEvent.Invoke();
}