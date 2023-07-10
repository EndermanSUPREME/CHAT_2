using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameClock : MonoBehaviour
{
    [SerializeField] Text ComputerClockText, HUDClockText;
    [SerializeField] emailAppScript emailApp;
    float timePast = 0;
    string newTimeText;
    int maxTimeAllowed = 30, sentRandomEmailTime, sendRandomEmailTime2;
    bool trueEndingReached = false, gameHintAppeared = true;
    [SerializeField] Animator FadeOutAnim;
    [SerializeField] AudioSource alarmSound;

    void Awake()
    {
        Invoke("StartTheClock", 1.5f);
    }

    void StartTheClock()
    {
        gameHintAppeared = false;
    }

    void Start()
    {
        sentRandomEmailTime = Random.Range(5, 10);
        sendRandomEmailTime2 = Random.Range(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameHintAppeared)
        {
            ClockRunning();
        }
    }

    void ClockRunning()
    {
        timePast += Time.deltaTime;
        int minutesPast = (int)(timePast/60);
        int hourIndex = minutesPast / 5;

        Debug.Log(minutesPast + " : " + hourIndex);

        switch (hourIndex)
        {
            case 0:
                newTimeText = "12:00 AM";
            break;
            case 1:
                newTimeText = "1:00 AM";
            break;
            case 2:
                newTimeText = "2:00 AM";
            break;
            case 3:
                newTimeText = "3:00 AM";
            break;
            case 4:
                newTimeText = "4:00 AM";
            break;
            case 5:
                newTimeText = "5:00 AM";
            break;
            case 6:
                newTimeText = "6:00 AM";
            break;
            
            default:
            break;
        }
        
        ComputerClockText.text = newTimeText;
        HUDClockText.text = newTimeText;

        if (!trueEndingReached)
        {
            CheckTheTime(minutesPast);
        }
    }

    void CheckTheTime(int minutesPast)
    {
        if (minutesPast >= maxTimeAllowed) // 30 minutes
        {
            OutOfTimeEnding();
        } else if (minutesPast == sentRandomEmailTime)
            {
                emailApp.RecievedEmail(2);
            } else if (minutesPast == sendRandomEmailTime2)
                {
                    emailApp.RecievedEmail(3);
                }
    }

    public void ReachedTheTrueEnd()
    {
        trueEndingReached = true;
    }

    public void PauseClock(bool v)
    {
        gameHintAppeared = v;
    }

    void OutOfTimeEnding()
    {
        alarmSound.Play();
        FadeOutAnim.Play("FadeIn");
        Invoke("LoadGameOverScene", 1.25f);
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene(4);
    }
}//EndScript