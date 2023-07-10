using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorScript : MonoBehaviour
{
    bool AllowInteractionWithDoor = false;
    public LayerMask interactionLayer;
    Transform lastHighLightedObject;
    Material lastMaterial;
    public Material HighLightMat;
    [SerializeField] TrueEndingEvent TrueEndScript;

    void Update()
    {
        if (AllowInteractionWithDoor)
        {
            Raycasting();
        }
    }

    void Raycasting()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2.5f, interactionLayer))
        {
            if (lastHighLightedObject == null)
            {
                lastHighLightedObject = hit.transform;
                lastMaterial = hit.transform.GetComponent<Renderer>().material;
                hit.transform.GetComponent<Renderer>().material = HighLightMat;
            } else // highlighted previously
                {
                    lastHighLightedObject.GetComponent<Renderer>().material = lastMaterial; // return object to normal material

                    lastHighLightedObject = hit.transform;
                    lastMaterial = hit.transform.GetComponent<Renderer>().material;
                    hit.transform.GetComponent<Renderer>().material = HighLightMat;
                }

            if (Input.GetButtonDown("Fire2")) // click on door
            {
                TrueEndScript.TransitionToEnd();
            }
        } else // not looking at interactables
            {
                if (lastHighLightedObject != null)
                {
                    lastHighLightedObject.GetComponent<Renderer>().material = lastMaterial; // return object to normal material
                    lastHighLightedObject = null;
                }
            }
    }

    public void FinalEndingReached()
    {
        AllowInteractionWithDoor = true;
    }

    public void InteractionPeriodEnded()
    {
        AllowInteractionWithDoor = false;
    }

}//EndScript