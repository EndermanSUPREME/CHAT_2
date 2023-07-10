using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antiVirusPopUp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode. Return))
        {
            gameObject.SetActive(false);
        }        
    }
}//EndScript