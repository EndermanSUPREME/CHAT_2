using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_Event : MonoBehaviour
{
    [SerializeField] GameObject StaticScreenDisplay, RandomVideoDisplay;
    [SerializeField] AudioSource StaticSound, VideoSound;
    bool eventTriggered = false;
    [SerializeField] bool testTV_Event;

    void Start()
    {
        eventTriggered = false;
        
        StaticScreenDisplay.SetActive(false);
        StaticSound.Stop();
        RandomVideoDisplay.SetActive(false);
        VideoSound.Stop();
    }

    void Update()
    {
        if (testTV_Event)
        {
            // BeginRandom_TVEvent();
            testTV_Event = false;
        }
    }

    public void BeginRandom_TVEvent()
    {
        if (!eventTriggered)
        {
            StartCoroutine(Television_Event());
            eventTriggered = true;
        }
    }

    IEnumerator Television_Event()
    {
//=========== Play Static ===============================
        StaticScreenDisplay.SetActive(true);
        StaticSound.Play();
        RandomVideoDisplay.SetActive(false);
        VideoSound.Stop();
        yield return new WaitForSeconds(2);
//=========== Play Video ===============================
        StaticScreenDisplay.SetActive(false);
        StaticSound.Stop();
        RandomVideoDisplay.SetActive(true);
        VideoSound.Play();
        yield return new WaitForSeconds(59.5f);
//=========== Play Static ===============================
        StaticScreenDisplay.SetActive(true);
        StaticSound.Play();
        RandomVideoDisplay.SetActive(false);
        VideoSound.Stop();
        yield return new WaitForSeconds(2);
//=========== TV Turn Off ===============================
        StaticScreenDisplay.SetActive(false);
        StaticSound.Stop();
        RandomVideoDisplay.SetActive(false);
        VideoSound.Stop();
    }
}//EndScript