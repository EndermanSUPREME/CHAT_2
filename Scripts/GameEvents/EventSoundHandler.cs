using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSoundHandler : MonoBehaviour
{
    [SerializeField] AudioSource footstepSound, fallingImpactSound;
    [SerializeField] Transform ChairObject, newChairObject, footstepEndPosition;
    bool footstepEvent = false, chairEventOccured = false;

    void Start()
    {
        ChairObject.gameObject.SetActive(true);
        newChairObject.gameObject.SetActive(false);
    }

    void Update()
    {
        if (footstepEvent)
        {
            if (footstepSound.transform.position != footstepEndPosition.position)
            {
                footstepSound.transform.position = Vector3.MoveTowards(footstepSound.transform.position, footstepEndPosition.position, 5 * Time.deltaTime);
            } else
                {
                    footstepEvent = false;
                }
        }
    }
    
    public void FootStepsComingCloser()
    {
        footstepEvent = true;
        footstepSound.Play();
    }

    public void ChairMoved()
    {
        if (!chairEventOccured)
        {
            ChairObject.gameObject.SetActive(false);
            newChairObject.gameObject.SetActive(true);
    
            fallingImpactSound.Play();

            chairEventOccured = true;
        }
    }
}//EndScript