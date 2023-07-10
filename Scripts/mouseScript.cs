using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseScript : MonoBehaviour
{
    public float sensitivity = 200f;
    float xRotation = 0f, mouseX, mouseY;
    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 65;

        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;

        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
    }
    
}//EndScript