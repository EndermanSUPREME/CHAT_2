using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame_taskbar_icon : MonoBehaviour
{
    [SerializeField] minigame_handler minigame_Handler;

    public void FocusOnMinigame()
    {
        minigame_Handler.PullWindowUpFront();
    }
}//EndScript