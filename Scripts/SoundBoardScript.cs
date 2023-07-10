using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBoardScript : MonoBehaviour
{
    [SerializeField] AudioSource[] keyboardSounds;
    [SerializeField] AudioSource mouseClick;

    public void PlayKeyboardSound()
    {
        if (keyboardSounds != null)
        {
            keyboardSounds[Random.Range(0, keyboardSounds.Length)].Play();
        }
    }

    public void PlayMouseSound()
    {
        mouseClick.Play();
    }
}//EndScript