using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame_buttons : MonoBehaviour
{
    [SerializeField] minigame_handler mini_handler;

    public void CloseExecutable()
    {
        mini_handler.StopExecutable();
    }
}//EndScript