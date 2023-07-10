using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Interface : MonoBehaviour
{
    private float musicVolume, sfxVolume, ResolutionIndex;
    [SerializeField] bool gamePaused = false, debugFrames = false, pauseableFrame = true, showingTutorial = false, noPopUps = true;
    private int[] ScreenWidth = {800, 1024, 1152, 1280, 1366, 1440, 1600, 1920, 2560}, ScreenHeight = {600, 768, 864, 720, 768, 900, 1200, 1080, 1440}; // [0, 8]
    [SerializeField] Slider MusicSlider, SfxSlider, ResolutionSlider;
    GameObject[] MusicComponent, SFXComponent;
    [SerializeField] GameObject HUD, Main, Setting, How2PlayScreen, TutorialTriggerObject, checkMarkSprite;
    [SerializeField] Text MusicDisplay, SFX_Display, ResolutionDisplay, FPS_Display;
    [SerializeField] Interaction playerInteraction;
    [SerializeField] Animator ScreenFader;

    [SerializeField] PlayerMovement movementScript;
    [SerializeField] mouseScript mouseControlScript;
    [SerializeField] Ingame_PopUps popUpScript;
    BuildModeHandler ModeHandler;

    [SerializeField] AppWindow[] appWindowsInGame;
    [SerializeField] GameObject[] ObjectsNotInWebBuild;

    void Awake()
    {
        Application.targetFrameRate = 65;

        ModeHandler = Object.FindObjectOfType<BuildModeHandler>();
        
        if (Setting != null)
        {
            if (PlayerPrefs.GetFloat("ScreenWidth") == 0)
            {
                // load default settings
                if (ModeHandler.isWebGLMode())
                {
                    LoadBrowserDefaults(); // 1366x768 : index 4
                } else
                    {
                        LoadDefaults();
                    }
            } else // player had set their resolution
                {
                    ApplyPlayerSettings();

                    if (ModeHandler.isWebGLMode())
                    {
                        foreach (GameObject piece in ObjectsNotInWebBuild)
                        {
                            piece.SetActive(false);
                        }
                    } else
                        {
                            foreach (GameObject piece in ObjectsNotInWebBuild)
                            {
                                piece.SetActive(true);
                            }
                        }
                }

            if (SceneManager.GetActiveScene().buildIndex > 1)
            {
                ResumeGame();
            } else
                {
                    MainMenu_UI();
                }
        }

        if (popUpScript != null)
        {
            DisablePopUpsInGame();
        }
    }

    void Start()
    {
        MusicComponent = GameObject.FindGameObjectsWithTag("Music");
        SFXComponent = GameObject.FindGameObjectsWithTag("SFX");

        StartFadingIn();
    }

    public void DisplayingTutorial(bool v)
    {
        showingTutorial = v;
    }

    void LoadDefaults()
    {
        // Debug.Log("Loaded Defaults");
        ResolutionIndex = 7;
        ResolutionSlider.value = 7;

        MusicSlider.value = 5;
        SfxSlider.value = 5;

        SetResolutionSettings();
        SetAudioSettings();

        foreach (GameObject piece in ObjectsNotInWebBuild)
        {
            piece.SetActive(true);
        }

        Debug.Log("[Default] => " + (int) PlayerPrefs.GetFloat("ScreenWidth") + " : " + (int) PlayerPrefs.GetFloat("ScreenHeight"));
    }

    void LoadBrowserDefaults()
    {
        // Debug.Log("Loaded Defaults");
        ResolutionIndex = 4;
        ResolutionSlider.value = 4;

        MusicSlider.value = 5;
        SfxSlider.value = 5;

        SetResolutionSettings();
        SetAudioSettings();

        foreach (GameObject piece in ObjectsNotInWebBuild)
        {
            piece.SetActive(false);
        }

        Debug.Log("[Default] => " + (int) PlayerPrefs.GetFloat("ScreenWidth") + " : " + (int) PlayerPrefs.GetFloat("ScreenHeight"));
    }

    public void ConfigForNewLevel()
    {
        Application.targetFrameRate = 65;
        
        if (Setting != null)
        {
            if (SceneManager.GetActiveScene().buildIndex > 1)
            {
                ResumeGame();
            } else
                {
                    MainMenu_UI();
                }
    
            // Get all objects in the scene that have a script for editing
            MusicComponent = GameObject.FindGameObjectsWithTag("Music");
            SFXComponent = GameObject.FindGameObjectsWithTag("SFX");
    
            ApplyPlayerSettings();
        }
    }

    public void PauseBuffer(bool value)
    {
        pauseableFrame = value;
        // print(pauseableFrame);
    }

    public void ExternalPause()
    {
        GoToSettings();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && playerInteraction != null)
        {
            if (!playerInteraction.GetInterface() && (Input.GetKeyDown(KeyCode. Escape) || Input.GetKeyDown(KeyCode. Tab)) && pauseableFrame) // player isnt on the desktop
            {
                if (!gamePaused)
                {
                    GoToSettings();
                } else
                    {
                        ResumeGame();
                    }
            }
        }

        if (Setting.activeSelf && Setting != null)
        {
            ResolutionIndex = ResolutionSlider.value;

            MusicDisplay.text = MusicSlider.value * 10 + " %";
            SFX_Display.text = SfxSlider.value * 10 + " %";
            ResolutionDisplay.text = ScreenWidth[(int)ResolutionIndex] + "x" + ScreenHeight[(int)ResolutionIndex];

            SetAudioSettings();
        }

        if (FPS_Display != null)
        {
            if (debugFrames)
            {
                float fpsNum = Mathf.Round(1.0f / Time.deltaTime);
                FPS_Display.text = fpsNum.ToString() + " : FPS";
            } else
                {
                    FPS_Display.text = "";
                }
        }
    }

//========================== GAME SETTINGS =====================================
    public void ApplyPlayerSettings()
    {
        // Screen.SetResolution((int) PlayerPrefs.GetFloat("ScreenWidth"), (int) PlayerPrefs.GetFloat("ScreenHeight"), true);

        MusicSlider.value = PlayerPrefs.GetFloat("MusicVol") * 10;
        SfxSlider.value = PlayerPrefs.GetFloat("SFXVol") * 10;
        ResolutionSlider.value = PlayerPrefs.GetFloat("ResSlideVal");
        ResolutionIndex = PlayerPrefs.GetFloat("ResSlideVal");

        SetResolutionSettings();
        
        SetAudioSettings();

        Debug.Log("[Apply] => " + (int) PlayerPrefs.GetFloat("ScreenWidth") + " : " + (int) PlayerPrefs.GetFloat("ScreenHeight"));
    }

    public void SetResolutionSettings()
    {
        Screen.SetResolution(ScreenWidth[(int)ResolutionIndex], ScreenHeight[(int)ResolutionIndex], true);

        PlayerPrefs.SetFloat("ScreenWidth", ScreenWidth[(int)ResolutionIndex]);
        PlayerPrefs.SetFloat("ScreenHeight", ScreenHeight[(int)ResolutionIndex]);
        PlayerPrefs.SetFloat("ResSlideVal", ResolutionIndex);

        if (appWindowsInGame != null)
        {
            foreach (AppWindow window in appWindowsInGame)
            {
                window.PlayerChangedScreenResolutions();
            }
        }
    }

    private void SetAudioSettings()
    {
        MusicComponent = GameObject.FindGameObjectsWithTag("Music");
        SFXComponent = GameObject.FindGameObjectsWithTag("SFX");

        musicVolume = MusicSlider.value / 10;
        sfxVolume = SfxSlider.value / 10;

        PlayerPrefs.SetFloat("MusicVol", musicVolume);
        PlayerPrefs.SetFloat("SFXVol", sfxVolume);

        PlayerPrefs.SetFloat("MusicSlideVal", MusicSlider.value);
        PlayerPrefs.SetFloat("SFXSlideVal", SfxSlider.value);

        if (MusicComponent != null && SFXComponent != null)
        {
            foreach (GameObject Sound in MusicComponent)
            {
                Sound.GetComponent<AudioSource>().volume = musicVolume;
            }
    
            foreach (GameObject Sound in SFXComponent)
            {
                Sound.GetComponent<AudioSource>().volume = sfxVolume;
            }
        }
    }

    public void StartFadingIn()
    {
        if (MusicComponent != null && SFXComponent != null)
        {
            foreach (GameObject Sound in MusicComponent)
            {
                StartCoroutine(FadeIn(Sound.GetComponent<AudioSource>(), 0.25f));
            }
    
            foreach (GameObject Sound in SFXComponent)
            {
                StartCoroutine(FadeIn(Sound.GetComponent<AudioSource>(), 0.25f));
            }
        }
    }

    public void StartFadingOut()
    {
        if (MusicComponent != null && SFXComponent != null)
        {
            foreach (GameObject Sound in MusicComponent)
            {
                StartCoroutine(FadeOut(Sound.GetComponent<AudioSource>(), 0.25f));
            }
    
            foreach (GameObject Sound in SFXComponent)
            {
                StartCoroutine(FadeOut(Sound.GetComponent<AudioSource>(), 0.25f));
            }
        }
    }

    IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = PlayerPrefs.GetFloat("MusicVol");
 
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }

        audioSource.volume = 0;
    }
 
    IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 0.1f;
 
        audioSource.volume = 0;
 
        while (audioSource.volume < PlayerPrefs.GetFloat("MusicVol"))
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.volume = PlayerPrefs.GetFloat("MusicVol");
    }

    public void ShowFPS()
    {
        if (!debugFrames)
        {
            debugFrames = true;
        } else
            {
                debugFrames = false;
            }
    }
    
//======================= SCENE CHANGES ==========================
    public void StartTheGame() // transtion
    {
        ScreenFader.Play("FadeIn");

        Invoke("LoadGameScene", 1.65f);
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene(2);
    }

    public void ReturnToMainMenuScene() // transition
    {
        ResumeGame();
        PauseBuffer(false);
        ScreenFader.Play("FadeIn");

        Invoke("GoToMain", 1f);
    }

    void GoToMain() // Main Menu Scene
    {
        SceneManager.LoadScene(1);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

//======================= SCREEN CHANGES ==========================
    public void ShowFrameRate()
    {
        if (!debugFrames)
        {
            debugFrames = true;
        } else
            {
                debugFrames = false;
            }
    }

    public void GoToPauseMenu() // InGame Scenes
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        GoToSettings();
    }

    public void GoToSettings()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        HUD.SetActive(false);
        Main.SetActive(false);
        Setting.SetActive(true);
        How2PlayScreen.SetActive(false);

        gamePaused = true;

        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            Time.timeScale = 0;
        }

        if (playerInteraction != null)
        {
            movementScript.StopPlayer();
            movementScript.enabled = false;
            mouseControlScript.enabled = false;
            playerInteraction.enabled = false;
        }
    }

    public void HowToPlay()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        HUD.SetActive(false);
        Main.SetActive(false);
        Setting.SetActive(false);
        How2PlayScreen.SetActive(true);
    }

    public void MainMenu_UI()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        Main.SetActive(true);
        Setting.SetActive(false);
        How2PlayScreen.SetActive(false);
    }

    public void ResumeGame() // InGame Scenes
    {
        if (!showingTutorial)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }

        HUD.SetActive(true);
        Setting.SetActive(false);
        How2PlayScreen.SetActive(false);

        gamePaused = false;

        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            Time.timeScale = 1;
        }

        if (playerInteraction != null)
        {
            movementScript.AllowMovement();
            movementScript.enabled = true;
            mouseControlScript.enabled = true;
            playerInteraction.enabled = true;
        }
    }

    public void DisablePopUpsInGame()
    {
        if (noPopUps) // runs when true
        {
            noPopUps = false;

            popUpScript.enabled = false;
            TutorialTriggerObject.SetActive(false);
            checkMarkSprite.SetActive(false);
        } else // runs when false
            {
                noPopUps = true;

                popUpScript.enabled = true;
                TutorialTriggerObject.SetActive(true);
                checkMarkSprite.SetActive(true);
            }
    }

}//EndScript