using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingame_PopUps : MonoBehaviour
{
    [SerializeField] GameClock clockScript;
    [SerializeField] PlayerMovement movementScript;
    [SerializeField] mouseScript mouseCode;
    [SerializeField] UI_Interface uiScript;
    [SerializeField] Interaction playerInteraction;

    bool popUpAppeared = false, showedSSH = false, showedHash = false;
    [SerializeField] GameObject[] popUpObjects;
    [SerializeField] GameObject TutorialPopUp, PlayerObject;
    [SerializeField] Image DisplayImage;
    [SerializeField] Sprite[] pages;
    int pN = 0;

    void Start()
    {
        transform.GetComponent<Collider>().enabled = true;

        TutorialPopUp.SetActive(false);

        playerInteraction.InGameTutorialModifier(false);

        foreach (GameObject PopUp in popUpObjects)
        {
            PopUp.SetActive(false);
        }
    }

    void DelayForReading()
    {
        popUpAppeared = true;
    }

    void Update()
    {
        if (popUpAppeared)
        {
            if (Input.GetKeyDown(KeyCode. Return))
            {
                popUpAppeared = false;
                clockScript.PauseClock(false);
    
                foreach (GameObject PopUp in popUpObjects)
                {
                    PopUp.SetActive(false);
                }
    
                if (TutorialPopUp.activeSelf)
                {
                    movementScript.AllowMovement();
                    TutorialPopUp.SetActive(false);
    
                    uiScript.DisplayingTutorial(false);
    
                    playerInteraction.InGameTutorialModifier(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                    mouseCode.enabled = true;
                }
            }
        }
    }
    
//===============================================================

    void ShowTutorial()
    {
        transform.GetComponent<Collider>().enabled = false;
        movementScript.UsingComputer();

        mouseCode.enabled = false;

        Invoke("DelayForReading", 1);
        TutorialPopUp.SetActive(true);
        DisplayImage.sprite = pages[0];

        uiScript.DisplayingTutorial(true);

        playerInteraction.InGameTutorialModifier(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ShowNextPage()
    {
        if (pN < pages.Length - 1)
        {
            pN++;
            DisplayImage.sprite = pages[pN];
        }
    }

    public void ShowPreviousPage()
    {
        if (pN > 0)
        {
            pN--;
            DisplayImage.sprite = pages[pN];
        }
    }

//===============================================================

    public void FoundFirstHash()
    {
        if (!showedHash && this.enabled)
        {
            showedHash = true;
            
            clockScript.PauseClock(true);
            popUpObjects[0].SetActive(true);
            Invoke("DelayForReading", 2);
        }
    }

    public void FoundEncrypted_SSH_Hash()
    {
        if (!showedSSH && this.enabled)
        {
            showedSSH = true;

            clockScript.PauseClock(true);
            popUpObjects[1].SetActive(true);
            Invoke("DelayForReading", 2);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform == PlayerObject.transform)
        {
            ShowTutorial();
        }
    }

}//EndScript