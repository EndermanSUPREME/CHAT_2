using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class warningIntro : MonoBehaviour
{
    void Start()
    {
        Invoke("LoadMainMenuScene", 8.5f);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LoadMainMenuScene()
    {
        SceneManager.LoadScene(1);
    }
}//EndScript