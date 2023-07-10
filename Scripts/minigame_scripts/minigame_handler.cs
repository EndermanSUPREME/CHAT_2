using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame_handler : MonoBehaviour
{
    [SerializeField] minigameStatus minigame_Stat;
    [SerializeField] Transform minigamePlayer, startingPoint, WindowPivot;
    [SerializeField] GameObject minigame_window, minigame_area, minigame_mapArea, jumpscareObject, scareFrameObject, executableTaskBarIcon;
    [SerializeField] GameObject[] areaChunks;
    bool executableCanRun = true, execRunning = false, screenClustered = false;

    public void StartExecutable()
    {
        if (executableCanRun)
        {
            if (!execRunning)
            {
                VirusEvent viEvent = Object.FindObjectOfType<VirusEvent>();
                viEvent.startVirusTimer();

                float x = PlayerPrefs.GetFloat("ScreenWidth") / 2;
                float y = PlayerPrefs.GetFloat("ScreenHeight") / 2;
                minigame_window.transform.localPosition = new Vector3(x, y, 0);
                float width = PlayerPrefs.GetFloat("ScreenWidth") - 120;
                float height = PlayerPrefs.GetFloat("ScreenHeight") - 80;
                minigame_window.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

                minigame_window.SetActive(true);
                minigame_area.SetActive(true);
                minigame_mapArea.SetActive(true);
                executableTaskBarIcon.SetActive(true);

                PullWindowUpFront();

                foreach (GameObject chunkSection in areaChunks)
                {
                    chunkSection.SetActive(false);
                }

                areaChunks[0].SetActive(true);

                minigamePlayer.position = startingPoint.position;
                execRunning = true;
            }
        }
    }

    void Update()
    {
        if (WindowPivot.GetSiblingIndex() != 6)
        {
            minigamePlayer.GetComponent<minigame_player>().canPlayerMove(false);
        } else
            {
                if (WindowPivot.parent.childCount < 10)
                {
                    if (!screenClustered)
                    {
                        minigamePlayer.GetComponent<minigame_player>().canPlayerMove(true);
                    } else
                        {
                            if (WindowPivot.parent.childCount == 7)
                            {
                                screenClustered = false;

                                VirusEvent viEventScript = Object.FindObjectOfType<VirusEvent>();
                                
                                foreach (AudioSource virusSound in viEventScript.GetVirusSoundList())
                                {
                                    virusSound.mute = true;
                                }
                            }
                        }
                } else
                    {
                        screenClustered = true;
                        minigamePlayer.GetComponent<minigame_player>().canPlayerMove(false);
                    }
            }
    }

    public void PullWindowUpFront()
    {
        WindowPivot.SetSiblingIndex(6);
        Debug.Log(WindowPivot.GetSiblingIndex());
    }

    public void CorruptExecutable()
    {
        executableCanRun = false;

        Minigame_JumpScare();
    }

    void Minigame_JumpScare()
    {
        minigame_mapArea.SetActive(false);

        if (jumpscareObject != null)
        {
            virusWindow[] viWindows = Object.FindObjectsOfType<virusWindow>();

            foreach (virusWindow vWin in viWindows)
            {
                vWin.MiniGameEndTriggered(true);
            }

            scareFrameObject.SetActive(false);
            jumpscareObject.SetActive(true);

            Invoke("ShowScareFrame", 10);
        }
    }

    void ShowScareFrame()
    {
        scareFrameObject.SetActive(true);
        Invoke("StopExecutable", 2.75f);
    }

    public void StopExecutable()
    {
        virusWindow[] viWindows = Object.FindObjectsOfType<virusWindow>();

        foreach (virusWindow vWin in viWindows)
        {
            vWin.MiniGameEndTriggered(false);
        }

        minigame_window.SetActive(false);
        minigame_area.SetActive(false);
        minigame_mapArea.SetActive(false);
        executableTaskBarIcon.SetActive(false);
        executableTaskBarIcon.SetActive(false);
        
        jumpscareObject.SetActive(false);
        scareFrameObject.SetActive(false);

        minigamePlayer.position = startingPoint.position;
        execRunning = false;
    }

}//EndScript