using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virusWindow : MonoBehaviour
{
    AudioSource targetSound;
    [SerializeField] GameObject WindowMain;
    [SerializeField] Sprite[] images;
    bool minigameEndReached = false;

    void Start()
    {
        VirusEvent virusEventScript = Object.FindObjectOfType<VirusEvent>();
        targetSound = virusEventScript.GetVirusSoundList()[Random.Range(0, virusEventScript.GetVirusSoundList().Length)];

        if (!targetSound.isPlaying)
        {
            targetSound.Play();
        }

        GetComponent<UnityEngine.UI.Image>().sprite = images[Random.Range(0, images.Length)];
    }

    public void ClickingOffVirusWindow()
    {
        targetSound.mute = true;

        if (WindowMain.transform.parent.childCount == 8)
        {
            VirusEvent viEventScript = Object.FindObjectOfType<VirusEvent>();

            foreach (AudioSource virusSound in viEventScript.GetVirusSoundList())
            {
                virusSound.mute = true;
            }
        }

        Destroy(WindowMain);
    }

    public void MiniGameEndTriggered(bool b)
    {
        minigameEndReached = b;
    }

    void Update()
    {
        if (!minigameEndReached)
        {
            if (targetSound.isPlaying)
            {
                if (targetSound.mute)
                {
                    targetSound.mute = false;
                }
            }
        } else
            {
                targetSound.mute = true;
            }
    }
}//EndScript