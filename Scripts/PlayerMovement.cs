using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speedConst;
    [SerializeField] Camera playerCam;

    [SerializeField] [Range(70, 100)] int newFOV;

    [SerializeField] [Range(0.1f, 0.4f)] float rOc;
    [SerializeField] [Range(0.25f, 0.75f)] float rOc2;

    float currentSpeed;
    Vector3 vel;
    bool playerCantMove = false;

    public void StopPlayer()
    {
        rb.velocity = Vector3.zero;
        playerCantMove = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 65;

        if (!playerCantMove)
        {
            GeneralMovement();
        } else
            {
                rb.velocity = Vector3.zero;
            }
    }

    void GeneralMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode. LeftShift))
        {
            if (currentSpeed < speedConst * 1.75f)
            {
                currentSpeed += rOc;
            } else
                {
                    currentSpeed = speedConst * 1.75f;
                }

            if (playerCam.fieldOfView < newFOV)
            {
                playerCam.fieldOfView += rOc2;
            } else
                {
                    playerCam.fieldOfView = newFOV;
                }
        } else
            {
                if (currentSpeed > speedConst)
                {
                    currentSpeed -= rOc;
                } else
                    {
                        currentSpeed = speedConst;
                    }

                if (playerCam.fieldOfView > 60)
                {
                    playerCam.fieldOfView -= rOc2;
                } else
                    {
                        playerCam.fieldOfView = 60;
                    }
            }

        vel = (transform.right * x + transform.forward * z) * currentSpeed;
        vel.y = 0;

        rb.velocity = vel;
    }

    public void UsingComputer()
    {
        currentSpeed = 0;
        playerCantMove = true;
    }

    public void AllowMovement()
    {
        currentSpeed = speedConst;
        playerCantMove = false;
    }
}//EndScript