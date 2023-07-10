using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class CHAT_App_Script : MonoBehaviour
{
    [SerializeField] Image CHAT_AI_PFP;
    [SerializeField] Text PlayerDialogue_txt, AI_Dialogue_txt;
    [SerializeField] Sprite pfp1, pfp2, pfp3, hiddenImg;
    [SerializeField] emailAppScript emailApp;
    [SerializeField] terminalScript CMD_Script;
    [SerializeField] DialogObject[] chatDialogue, triggerOne_Dialogue; // for when ai become triggered we will perform chatDialogue[0] = triggerOne_Dialogue[0];
    DialogObject[] currentDialogue = new DialogObject[3];
    [SerializeField] DialogObject DialogueEndObject;
    [SerializeField] TV_Event televisionEventScript;

    [SerializeField] int ai1_status = 0, ai2_status = 0, ai3_status = 0;
    int activeChannelIndex = -1, fontSetSize;

    bool previewOne = false, previewTwo = false, previewThree = false, trueEndingNearby = false;
    [SerializeField] bool waitingOnReply = false, waitingOnReply2 = false, waitingOnReply3 = false;
    [SerializeField] bool textEntered = false, seeChoice = true;
    [SerializeField] GameObject[] ChoiceButtons;
    
    [SerializeField] Animator FadeOutAnim;
    [SerializeField] VideoPlayer ClipPlayer;
    [SerializeField] VideoClip JosephEnding, JacolynEnding, NikalasEnding;
    [SerializeField] GameObject GameArea, CutsceneObject, PlayerClock;
    [SerializeField] GameClock clockScript;

    void Start()
    {
        CutsceneObject.SetActive(false);
        fontSetSize = AI_Dialogue_txt.fontSize;
        
        if (ai1_status > 0)
        {
            // triggered
            currentDialogue[0] = triggerOne_Dialogue[0];
        } else
            {
                // regular
                currentDialogue[0] = chatDialogue[0];
            }   

        if (ai2_status > 0)
        {
            // triggered
            currentDialogue[1] = triggerOne_Dialogue[1];
        } else
            {
                // regular
                currentDialogue[1] = chatDialogue[1];
            }    

        if (ai3_status > 0)
        {
            // triggered
            currentDialogue[2] = triggerOne_Dialogue[2];
        } else
            {
                // regular
                currentDialogue[2] = chatDialogue[2];
            }
    }

    void Update()
    {
        if ((ai1_status > 0 && ai2_status > 0) || (ai2_status > 0 && ai3_status > 0) || (ai3_status > 0 && ai1_status > 0))
        {
            trueEndingNearby = true;
        }

        string testStr = "Just the normal no intense swearing, no racist stuff, if you see suspicious users cause we've had trolls and other hostiles bother folks just report them and etc";
        if (AI_Dialogue_txt.text.Length >= testStr.Length - 15)
        {
            AI_Dialogue_txt.fontSize = fontSetSize - 4;
        } else
            {
                AI_Dialogue_txt.fontSize = fontSetSize;
            }

        // Debug.Log(activeChannelIndex);

        if (activeChannelIndex == 0)
        {
            if (!waitingOnReply)
            {
                TalkToAI();
            } else
                {
                    WaitOnAIToReply();
                }
        } else if (activeChannelIndex == 1)
            {
                if (!waitingOnReply2)
                {
                    TalkToAI();
                } else
                    {
                        WaitOnAIToReply();
                    }
            } else if (activeChannelIndex == 2)
                {
                    if (!waitingOnReply3)
                    {
                        TalkToAI();
                    } else
                        {
                            WaitOnAIToReply();
                        }
                } else
                    {
                        for (int i = 0; i < ChoiceButtons.Length; i++)
                        {
                            ChoiceButtons[i].SetActive(false);
                        }

                        AI_Dialogue_txt.text = "Enter a Chat Channel!";
                        PlayerDialogue_txt.text = "";
                    }
    }

    public int GetActiveChatChannel()
    {
        return activeChannelIndex;
    }

    void TalkToAI()
    {
        AI_Dialogue_txt.text = currentDialogue[activeChannelIndex].AI_Dialogue;

        for (int i = 0; i < ChoiceButtons.Length; i++)
        {
            if (i < currentDialogue[activeChannelIndex].playerResponces.Length)
            {
                ChoiceButtons[i].SetActive(true);
            }
            else
            {
                ChoiceButtons[i].SetActive(false);
            }
        }

        if (currentDialogue[activeChannelIndex].playerResponces.Length == 3)
        {
            if (PlayerDialogue_txt.text.Equals(currentDialogue[activeChannelIndex].ShowChoiceOne()) || PlayerDialogue_txt.text.Equals(currentDialogue[activeChannelIndex].ShowChoiceTwo()) || PlayerDialogue_txt.text.Equals(currentDialogue[activeChannelIndex].ShowChoiceThree())) textEntered = true;
        }
        else if (currentDialogue[activeChannelIndex].playerResponces.Length == 2)
        {
            if (PlayerDialogue_txt.text.Equals(currentDialogue[activeChannelIndex].ShowChoiceOne()) || PlayerDialogue_txt.text.Equals(currentDialogue[activeChannelIndex].ShowChoiceTwo())) textEntered = true;
        }
        else if (currentDialogue[activeChannelIndex].playerResponces.Length == 1)
        {
            if (PlayerDialogue_txt.text.Equals(currentDialogue[activeChannelIndex].ShowChoiceOne())) textEntered = true;
        }
    }

    void WaitOnAIToReply()
    {
        string TarString = "I do want you say this though, I don't know what all is really going to show for new users like you, so if you see non text files (.txt) please don't touch those ok? Reminder: Type ftp 64.155.13.52 to gain a session!";
        if (currentDialogue[activeChannelIndex].AI_Dialogue.Equals(TarString)) // ftp related dialogue flows here
        {
            AI_Dialogue_txt.text = currentDialogue[activeChannelIndex].AI_Dialogue;
        } else
            {
                string aiEnding_1 = "... Honestly.. Yea I'd like that; be good to get out and see people, I'll show up an hour before it gets started and being some food unless your handling food lol";
                string aiEnding_2 = "U-uh.. Yeah of course sure thing! Sorry I.. wasn't expecting you to say yes, I'll meet you later tonight!";
                // string aiEnding_3 = "";

                if (currentDialogue[activeChannelIndex].AI_Dialogue.Equals(aiEnding_1)) // check strings for when the player has reached a normal ending with an AI
                {
                    // ClipPlayer.clip = JosephEnding;
                    ClipPlayer.url = "https://endermansupreme.github.io/my_WEBGL_Cutscenes/Joseph_FinalCutscene.mp4";
                    TransitionForAI_Ending();
                } else if (currentDialogue[activeChannelIndex].AI_Dialogue.Equals(aiEnding_2))
                    {
                        // ClipPlayer.clip = JacolynEnding;
                        ClipPlayer.url = "https://endermansupreme.github.io/my_WEBGL_Cutscenes/Jacolyn_FinalCutscene.mp4";
                        TransitionForAI_Ending();
                    }

                AI_Dialogue_txt.text = "Is Typing. . .";
            }

        for (int i = 0; i < ChoiceButtons.Length; i++)
        {
            ChoiceButtons[i].SetActive(false);
        }
    }

    void TransitionForAI_Ending()
    {
        clockScript.ReachedTheTrueEnd();
        PlayerClock.SetActive(false);

        FadeOutAnim.Play("FadeIn");
        Invoke("PlayEndingCutscene", 1.25f);
    }

    void PlayEndingCutscene()
    {
        GameArea.SetActive(false);
        CutsceneObject.SetActive(true);
    }
//======================== Button Clicked =========================
    public void GetChoiceOne()
    {
        if (textEntered)
        {
            PlayerDialogue_txt.text = currentDialogue[activeChannelIndex].PlayerPicksResponse_1();

            if (activeChannelIndex == 0)
            {
                StartCoroutine(AI_Replying());
            } else if (activeChannelIndex == 1)
                {
                    StartCoroutine(AI_Replying2());
                } else if (activeChannelIndex == 2)
                    {
                        StartCoroutine(AI_Replying3());
                    }
        }
    }

    public void GetChoiceTwo()
    {
        if (textEntered)
        {
            PlayerDialogue_txt.text = currentDialogue[activeChannelIndex].PlayerPicksResponse_2();

            if (activeChannelIndex == 0)
            {
                StartCoroutine(AI_Replying());
            } else if (activeChannelIndex == 1)
                {
                    StartCoroutine(AI_Replying2());
                } else if (activeChannelIndex == 2)
                    {
                        StartCoroutine(AI_Replying3());
                    }
        }
    }

    public void GetChoiceThree()
    {
        if (textEntered)
        {
            PlayerDialogue_txt.text = currentDialogue[activeChannelIndex].PlayerPicksResponse_3();

            if (activeChannelIndex == 0)
            {
                StartCoroutine(AI_Replying());
            } else if (activeChannelIndex == 1)
                {
                    StartCoroutine(AI_Replying2());
                } else if (activeChannelIndex == 2)
                    {
                        StartCoroutine(AI_Replying3());
                    }
        }
    }

    IEnumerator AI_Replying()
    {
        waitingOnReply = true;
        DialogObject nextBranch = currentDialogue[activeChannelIndex].GetNextObject();

        if (currentDialogue[activeChannelIndex].AI_Dialogue.Equals("יש חש כְּשֶׁהוּא מי וכשרונותיו אל בר ממקומותיהם כר. וענוהו הדיבור ובאיזה"))
        {
            televisionEventScript.BeginRandom_TVEvent();
        }

        yield return new WaitForSeconds(1);

        if (currentDialogue[activeChannelIndex].CheckIfDialogueEnded())
        {
            // Dialogue Branch Ended
            currentDialogue[activeChannelIndex] = DialogueEndObject;
        } else
            {
                // Dialogue Continues
                currentDialogue[activeChannelIndex] = nextBranch;
            }

        waitingOnReply = false;
    }

    IEnumerator AI_Replying2()
    {
        waitingOnReply2 = true;
        DialogObject nextBranch = currentDialogue[activeChannelIndex].GetNextObject();

        yield return new WaitForSeconds(1);

        if (currentDialogue[activeChannelIndex].CheckIfDialogueEnded())
        {
            // Dialogue Branch Ended
            currentDialogue[activeChannelIndex] = DialogueEndObject;
        } else
            {
                // Dialogue Continues
                currentDialogue[activeChannelIndex] = nextBranch;
            }
        waitingOnReply2 = false;
    }

    IEnumerator AI_Replying3() // THIS AI HAS TH FTP SESSION EVENT
    {
        waitingOnReply3 = true;
        DialogObject nextBranch = currentDialogue[activeChannelIndex].GetNextObject();

        yield return new WaitForSeconds(1);

        string TarString = "I do want you say this though, I don't know what all is really going to show for new users like you, so if you see non text files (.txt) please don't touch those ok? Reminder: Type ftp 64.155.13.52 to gain a session!";
        if (currentDialogue[activeChannelIndex].AI_Dialogue.Equals(TarString)) // ftp related dialogue flows here
        {
            if (CMD_Script.HasPlayerLoggedViaFTP())
            {
                if (currentDialogue[activeChannelIndex].CheckIfDialogueEnded())
                {
                    // Dialogue Branch Ended
                    currentDialogue[activeChannelIndex] = DialogueEndObject;
                } else
                    {
                        // Dialogue Continues
                        currentDialogue[activeChannelIndex] = nextBranch;
                    }
                waitingOnReply3 = false;
            } else
                {
                    StartCoroutine(AI_Replying3());
                }
        } else // regular and triggered dialogue passes here
            {
                if (currentDialogue[activeChannelIndex].CheckIfDialogueEnded())
                {
                    // Dialogue Branch Ended
                    currentDialogue[activeChannelIndex] = DialogueEndObject;
                } else
                    {
                        // Dialogue Continues
                        currentDialogue[activeChannelIndex] = nextBranch;
                    }
                waitingOnReply3 = false;
            }
    }
//======================== Button Hover =========================

    public void See_ChoiceOne()
    {
        if (activeChannelIndex > -1 && seeChoice)
        {
            if (!previewOne)
            {
                previewOne = true;
                previewTwo = false;
                previewThree = false;

                textEntered = false;

                if (PlayerDialogue_txt.text.Equals(""))
                {
                    StartCoroutine(LoadText(currentDialogue[activeChannelIndex].ShowChoiceOne()));
                    seeChoice = false;
                } else
                    {
                        PlayerDialogue_txt.text = " ";
                        StartCoroutine(LoadText(currentDialogue[activeChannelIndex].ShowChoiceOne()));
                        seeChoice = false;
                    }
            }
        }
    }

    public void See_ChoiceTwo()
    {
        if (activeChannelIndex > -1 && seeChoice)
        {
            if (!previewTwo)
            {
                previewOne = false;
                previewTwo = true;
                previewThree = false;
    
                textEntered = false;

                if (PlayerDialogue_txt.text.Equals(""))
                {
                    StartCoroutine(LoadText(currentDialogue[activeChannelIndex].ShowChoiceTwo()));
                    seeChoice = false;
                } else
                    {
                        PlayerDialogue_txt.text = " ";
                        StartCoroutine(LoadText(currentDialogue[activeChannelIndex].ShowChoiceTwo()));
                        seeChoice = false;
                    }
            }
        }
    }

    public void See_ChoiceThree()
    {
        if (activeChannelIndex > -1 && seeChoice)
        {
            if (!previewThree)
            {
                previewOne = false;
                previewTwo = false;
                previewThree = true;
    
                textEntered = false;

                if (PlayerDialogue_txt.text.Equals(""))
                {
                    StartCoroutine(LoadText(currentDialogue[activeChannelIndex].ShowChoiceThree()));
                    seeChoice = false;
                } else
                    {
                        PlayerDialogue_txt.text = " ";
                        StartCoroutine(LoadText(currentDialogue[activeChannelIndex].ShowChoiceThree()));
                        seeChoice = false;
                    }
            }
        }
    }

//==============================================

    public void OpenChannelOne()
    {
        if (!trueEndingNearby)
        {
            CHAT_AI_PFP.sprite = pfp1;
        } else
            {
                StopCoroutine(ImageSwitching(null, 0));
                StartCoroutine(ImageSwitching(pfp1, Random.Range(0.25f, 0.75f)));
            }

        activeChannelIndex = 0;
    }

    public void OpenChannelTwo()
    {
        if (!trueEndingNearby)
        {
            CHAT_AI_PFP.sprite = pfp2;
        } else
            {
                StopCoroutine(ImageSwitching(null, 0));
                StartCoroutine(ImageSwitching(pfp2, Random.Range(0.25f, 0.75f)));
            }

        activeChannelIndex = 1;
    }

    public void OpenChannelThree()
    {
        if (!trueEndingNearby)
        {
            CHAT_AI_PFP.sprite = pfp3;
        } else
            {
                StopCoroutine(ImageSwitching(null, 0));
                StartCoroutine(ImageSwitching(pfp3, Random.Range(0.25f, 0.75f)));
            }

        activeChannelIndex = 2;
    }

    IEnumerator ImageSwitching(Sprite normSprite, float t)
    {
        CHAT_AI_PFP.sprite = normSprite;
        yield return new WaitForSeconds(t);
        CHAT_AI_PFP.sprite = hiddenImg;
        yield return new WaitForSeconds(1);
        StartCoroutine(ImageSwitching(normSprite, Random.Range(0.25f, 0.75f)));
    }

//==============================================
//==============================================
//==============================================

    public void TriggerAI_One()
    {
        if (ai1_status < 1) // ftp
        {
            ai1_status++;
            
            currentDialogue[0] = triggerOne_Dialogue[0];
        }
    }

    public void TriggerAI_Two()
    {    
        if (ai2_status < 1) // smb
        {
            ai2_status++;
            
            currentDialogue[1] = triggerOne_Dialogue[1];
        }
    }

    public void TriggerAI_Three()
    {
        if (ai3_status < 1) // ftp
        {
            ai3_status++;
            
            currentDialogue[2] = triggerOne_Dialogue[2];
        }
    }

//=================================================================

    IEnumerator LoadText(string str)
    {
        PlayerDialogue_txt.text = "";
        string currentSentence = str;

        if (!PlayerDialogue_txt.text.Equals(currentSentence))
        {
            foreach (char letter in currentSentence.ToCharArray())
            {
                PlayerDialogue_txt.text += letter;
                yield return null;
            }

            seeChoice = true;
        }
    }

    public void RevertButtonAfterPress(Button pressedButton)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

}//EndScript