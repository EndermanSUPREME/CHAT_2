using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame_player : MonoBehaviour
{
    Rigidbody playerRb;
    [SerializeField] float speed;
    Renderer rend;
    [SerializeField] Texture leftOne, LeftTwo, RightOne, RightTwo;
    Texture one, two;
    [SerializeField] PlayerMovement mainPlayerMovement;
    bool allowMovement = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();

        one = RightOne;
        two = RightTwo;
        StartCoroutine(materialMoveAnim());
    }
    
    void Update()
    {
        if (!mainPlayerMovement.enabled) // main player is at the computer
        {
            if (allowMovement)
            {
                Movement();
            } else
                {
                    playerRb.velocity = Vector3.zero;
                }
        }
    }

    IEnumerator materialMoveAnim()
    {
        rend.material.mainTexture = one;
        yield return new WaitForSeconds(0.75f);
        rend.material.mainTexture = two;
        yield return new WaitForSeconds(0.75f);

        StartCoroutine(materialMoveAnim());
    }

    public void canPlayerMove(bool b)
    {
        allowMovement = b;
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x < 0)
        {
            one = leftOne;
            two = LeftTwo;
        } else if (x > 0)
            {
                one = RightOne;
                two = RightTwo;
            }

        playerRb.velocity = new Vector3(x, z, 0) * speed;
    }
}//EndScript