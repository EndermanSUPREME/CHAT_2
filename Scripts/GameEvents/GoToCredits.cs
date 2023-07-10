using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GoToCredits : MonoBehaviour
{
    void Start()
    {
        // Invoke("LoadCredits", (float)GetComponent<VideoPlayer>().length + 0.5f);

        if (GetComponent<VideoPlayer>().url.Contains("Jacolyn"))
        {
            Invoke("LoadCredits", 20 + 0.5f);
        } else if (GetComponent<VideoPlayer>().url.Contains("Joseph"))
            {
                Invoke("LoadCredits", 14 + 0.5f);
            } else if (GetComponent<VideoPlayer>().url.Contains("Forced"))
                {
                    Invoke("LoadCredits", 6 + 0.5f);
                } else if (GetComponent<VideoPlayer>().url.Contains("Open"))
                    {
                        Invoke("LoadCredits", 11 + 0.5f);
                    }
    }

    void LoadCredits()
    {
        SceneManager.LoadScene(3);
    }
}//EndScript