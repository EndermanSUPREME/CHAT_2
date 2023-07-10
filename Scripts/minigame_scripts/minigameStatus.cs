using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameStatus : MonoBehaviour
{
    [SerializeField] minigame_handler mini_handler;

    void ReachedTheEnd()
    {
        mini_handler.CorruptExecutable();
    }

    void OnTriggerEnter(Collider collider)
    {
        ReachedTheEnd();
    }
}//EndScript