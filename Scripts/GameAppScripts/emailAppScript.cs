using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class emailAppScript : MonoBehaviour
{
    [SerializeField] Transform[] inboxLocations;
    [SerializeField] GameObject[] inboxButtons, messageContent;
    int currentInboxPosition = 0;
    [SerializeField] AudioSource notificationAudio;

    // int selectedEmail -> corresponds to and index inside the inboxButtons GameObject Array
    
    public void RecievedEmail(int selectedEmail) // triggered by phish attacks AND/OR in game triggers
    {
        if (!inboxButtons[selectedEmail].activeSelf) // protection against trying to recieve the same email
        {
            notificationAudio.Play();
            inboxButtons[selectedEmail].transform.position = inboxLocations[currentInboxPosition].position;
            currentInboxPosition++;
            inboxButtons[selectedEmail].SetActive(true);
        }
    }

    public void ReadInbox()
    {
        string inboxClicked = EventSystem.current.currentSelectedGameObject.transform.parent.name;
        print(inboxClicked);

        for (int i = 0; i < inboxButtons.Length; i++)
        {
            if (inboxButtons[i].transform.name == inboxClicked) // show contents
            {
                messageContent[i].SetActive(true);
            } else // hide other content objects
                {
                    messageContent[i].SetActive(false);
                }
        }
    }
}//EndScript