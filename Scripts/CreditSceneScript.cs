using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneScript : MonoBehaviour
{
    void Start()
    {
        SetAudio();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void SetAudio()
    {
        GameObject[] MusicComponent = GameObject.FindGameObjectsWithTag("Music");
        GameObject[] SFXComponent = GameObject.FindGameObjectsWithTag("SFX");

        if (MusicComponent != null)
        {
            foreach (GameObject Sound in MusicComponent)
            {
                Sound.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVol");
            }
        }

        if (SFXComponent != null)
        {
            foreach (GameObject Sound in SFXComponent)
            {
                Sound.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVol");
            }
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}//EndScript