using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Ender.Computer;

public class Interaction : MonoBehaviour
{
    [SerializeField] GameObject interactionAffordance, ComputerGUI, HUD;
    [SerializeField] Canvas computerGUICanvas;
    Vector3[] resScales = { new Vector3(0.01124f,0.017f,1), new Vector3(0.0088f,0.0132f,1), new Vector3(0.0078f,0.0118f,1),
                            new Vector3(0.007f,0.014f,1), new Vector3(0.00657f,0.0131f,1), new Vector3(0.00624f,0.0112f,1),
                            new Vector3(0.0056f,0.00835f,1), new Vector3(0.00464f,0.00932f,1), new Vector3(0.0035f,0.007f,1)};

    [SerializeField] Transform CameraTransitionPoint, CameraTransitionLookPoint, ComputerGUIDisplayPoint, Computer;
    bool usingComputer = false, moving = false, resettingPlayer = false, inTutorial = false;
    [SerializeField] float camMovementSpeed;
    [SerializeField] MonoBehaviour[] PlayerScripts;
//==================================================================================================================================//
//==================================================================================================================================//
    [SerializeField] Transform ComputerCursor;
    public GraphicRaycaster m_Raycaster;
    public PointerEventData m_PointerEventData;
    public EventSystem m_EventSystem;

    bool GUI_Selected = false;
    protected GameObject Selected_GUIObject;

    [SerializeField] Animator upperLeftCorner, upperRightCorner, lowerLeftCorner, lowerRightCorner, fullFillAnim, leftFillAnim, rightFillAnim;
    [SerializeField] UI_Interface basicUI_Script;
    [SerializeField] CanvasScaler ComputerScreen_Canvas;

    bool fill_LL_Corner, fill_LU_Corner, fill_RL_Corner, fill_RU_Corner, fullFull, leftFill, rightFill, snapBack;
    [SerializeField] shortcutScaler shortcutResScaleScript;

    [SerializeField] Image screenCursor;
    public Sprite regularPointer, fingerPointer;

    [SerializeField] SoundBoardScript soundScript;
    [SerializeField] Rigidbody playerRb;

    void AllowGamePause()
    {
        basicUI_Script.PauseBuffer(true);
    }

    public void InGameTutorialModifier(bool value)
    {
        inTutorial = value;
    }

    void Awake()
    {
        shortcutResScaleScript.enabled = true;

        Invoke("PrepareComputer", 1f);
    }

    void PrepareComputer()
    {
        interactionAffordance.SetActive(false);
        ResetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (usingComputer)
        {
            if (!inTutorial)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            } else
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }

            UseGUIRaycast();
        } else
            {
                if (!inTutorial)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                } else
                    {
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
                    }

                UsePlayerRaycast();
            }

        if (moving)
        {
            transform.LookAt(CameraTransitionLookPoint.position);

            transform.position = Vector3.MoveTowards(transform.position, CameraTransitionPoint.position, camMovementSpeed * Time.deltaTime);

            if (transform.position == CameraTransitionPoint.position)
            {
                moving = false;

                // ComputerGUI.SetActive(true);
                computerGUICanvas.renderMode = RenderMode.ScreenSpaceOverlay;
                ComputerGUI.transform.SetParent(transform);
                HUD.SetActive(false);
                shortcutResScaleScript.enabled = true;
                usingComputer = true;
            }
        }

        if (resettingPlayer)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 0.6f, 0), camMovementSpeed * Time.deltaTime);

            if (transform.localPosition == new Vector3(0, 0.6f, 0))
            {
                for (int i = 0; i < PlayerScripts.Length; i++)
                {
                    PlayerScripts[i].enabled = true;
                }

                resettingPlayer = false;
            }
        }
    }

    void MoveCamera()
    {
        moving = true;
    }

    void ResetCamera()
    {
        // ComputerGUI.SetActive(false);
        usingComputer = false;
        shortcutResScaleScript.enabled = false;

        computerGUICanvas.renderMode = RenderMode.WorldSpace;
        ComputerGUI.transform.SetParent(Computer);
        ComputerGUI.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(PlayerPrefs.GetFloat("ScreenWidth"), PlayerPrefs.GetFloat("ScreenHeight"));
        
        ComputerGUI.transform.localPosition = ComputerGUIDisplayPoint.localPosition;
        ComputerGUI.transform.localScale = resScales[(int) PlayerPrefs.GetFloat("ResSlideVal")];
        ComputerGUI.transform.localRotation = new Quaternion(0.5f,0.5f,0.5f,0.5f);

        HUD.SetActive(true);

        resettingPlayer = true;

        // transform.localPosition = new Vector3(0, 0.6f, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void ChangeInterface(bool value)
    {
        if (value)
        {
            playerRb.velocity = Vector3.zero;
            basicUI_Script.PauseBuffer(false);
            playerRb.velocity = Vector3.zero;

            MoveCamera();
        
            for (int i = 0; i < PlayerScripts.Length; i++)
            {
                PlayerScripts[i].enabled = false;
            }
        } else
            {
                ResetCamera();
            }
    }

    public bool GetInterface()
    {
        return usingComputer;
    }

    void UsePlayerRaycast()
    {
        RaycastHit playerRay;

        if (Physics.Raycast(transform.position, transform.forward, out playerRay, 3.25f))
        {
            if (playerRay.transform.tag == "Desktop")
            {
                interactionAffordance.SetActive(true);

                if (Input.GetButtonDown("Fire2")) ChangeInterface(true);
            } else
                {
                    interactionAffordance.SetActive(false);
                }
        } else
            {
                interactionAffordance.SetActive(false);
            }
    }

    void UseGUIRaycast()
    {
        if (Input.GetKeyDown(KeyCode. Escape) || Input.GetKeyDown(KeyCode. Tab)) { ChangeInterface(false); Invoke("AllowGamePause", 0.5f); };

        ComputerScreen_Canvas.referenceResolution = new Vector2(PlayerPrefs.GetFloat("ScreenWidth"), PlayerPrefs.GetFloat("ScreenHeight"));

        interactionAffordance.SetActive(false);

        ComputerScript.MoveMouse(ComputerCursor, Input.mousePosition);

        if (!GUI_Selected)
        {
            GetGUI_Data();
        } else
            {
                ClickAndDrag(Selected_GUIObject);
            }
    }

    void GetGUI_Data()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results != null)
        {
            if (results.Count > 0)
            {
                // Debug.Log("Hovering Over : " + results[0].gameObject.name);
                ClickAndDrag(results[0].gameObject);

                // Debug.Log(results[0].gameObject.layer);

                if (results[0].gameObject.layer == 3)
                {
                    screenCursor.sprite = fingerPointer;
                } else
                    {
                        screenCursor.sprite = regularPointer;
                    }
            } else
                {
                    screenCursor.sprite = regularPointer;
                }
        }
    }

    void ClickAndDrag(GameObject guiObject)
    {
        if (Input.GetButtonDown("Fire1")) // single click
        {
            soundScript.PlayMouseSound();

            string objTag = guiObject.transform.tag;

            Selected_GUIObject = guiObject;

            GUI_Selected = true;

            // Debug.Log("Tag : " + objTag);

            switch (objTag)
            {
                case "windowScaler":
                    ComputerScript.SetRefPoint(Input.mousePosition);
                    ComputerScript.isRescaling(true);

                    Selected_GUIObject.transform.parent.GetComponent<AppWindow>().PullWindowUpFront();

                    ComputerScript.setModification(true);
                break;
                case "windowHead":
                    ComputerScript.SetRefPoint(Input.mousePosition);
                    ComputerScript.prepareWindowRepositioning(true);
                    ComputerScript.setPagePositionReferences(Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow());
                    ComputerScript.MoveAppPage(Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow(), Input.mousePosition);

                    Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow().GetComponent<AppWindow>().PullWindowUpFront();

                    ComputerScript.setModification(true);
                break;
                case "minimize":
                    Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow().GetComponent<AppWindow>().MinimizePage();

                    Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow().GetComponent<AppWindow>().PullWindowUpFront();
                    GUI_Selected = false;
                break;
                case "maximize":
                    Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow().GetComponent<AppWindow>().MaximizePage();
                    
                    Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow().GetComponent<AppWindow>().PullWindowUpFront();
                    GUI_Selected = false;
                break;
                case "close":
                    if (Selected_GUIObject.GetComponent<AppWindowComponent>() != null)
                    {
                        Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow().GetComponent<AppWindow>().ClosePage();
                    } else if (Selected_GUIObject.GetComponent<minigame_buttons>() != null)
                        {
                            Selected_GUIObject.GetComponent<minigame_buttons>().CloseExecutable();
                        } else if (Selected_GUIObject.GetComponent<virusWindow>() != null)
                            {
                                Selected_GUIObject.GetComponent<virusWindow>().ClickingOffVirusWindow();
                            }
                    GUI_Selected = false;
                break;
                case "taskBarIcon":
                    // Debug.Log("Unminimize Page");

                    if (Selected_GUIObject.GetComponent<Taskbar_Component>() != null)
                    {
                        Selected_GUIObject.GetComponent<Taskbar_Component>().UnMinimizePage();
                        Selected_GUIObject.GetComponent<Taskbar_Component>().GetAppWindow().PullWindowUpFront();
                    } else if (Selected_GUIObject.GetComponent<minigame_taskbar_icon>() != null) // minigame_handler
                        {
                            Selected_GUIObject.GetComponent<minigame_taskbar_icon>().FocusOnMinigame();
                        }
                    GUI_Selected = false;
                break;
                case "shortcut":
                    // Debug.Log("Spawn new page");

                    Selected_GUIObject.GetComponent<AppShortcut>().OpenInstance();
                    Selected_GUIObject.GetComponent<AppShortcut>().GetAppWindow().PullWindowUpFront();
                    GUI_Selected = false;
                break;
                case "executable":
                    Selected_GUIObject.GetComponent<minigame_handler>().StartExecutable();

                    GUI_Selected = false;
                break;
                case "window":
                    Selected_GUIObject.GetComponent<AppWindow>().PullWindowUpFront();
                    GUI_Selected = false;
                break;
                case "windowChunk":
                    if (Selected_GUIObject.transform.parent.tag == "window")
                    {
                        Selected_GUIObject.transform.parent.GetComponent<AppWindow>().PullWindowUpFront();
                    } else if (Selected_GUIObject.transform.parent.transform.parent.tag == "window")
                        {
                            Selected_GUIObject.transform.parent.transform.parent.GetComponent<AppWindow>().PullWindowUpFront();
                        }
                        
                    GUI_Selected = false;
                break;
                
                default:
                    // Debug.Log("Bad Tag Aquired. . .");

                    ComputerScript.isRescaling(false);
                    ComputerScript.prepareWindowRepositioning(false); // sets the page as stationary
                    ComputerScript.setModification(false);
                    GUI_Selected = false;
                break;
            }
        }

        if (Input.GetButton("Fire1")) // click and hold
        {
            // Debug.Log("Holding : " + Selected_GUIObject.name);
            // Debug.Log(Input.mousePosition);

            if (ComputerScript.GetRescaleStatus())
            {
                // Debug.Log("Rescaling On Mouse Hold");

                ComputerScript.RescalingWindow(Selected_GUIObject.transform.parent.GetComponent<RectTransform>(), Input.mousePosition);
                ComputerScript.setModification(true);
            }
            
            if (ComputerScript.isMovingPage())
            {
                // Debug.Log("Repositioning On Mouse Hold");

                ComputerScript.MoveAppPage(Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow(), Input.mousePosition);
                ComputerScript.setModification(true);

                float mX = Input.mousePosition.x;
                float mY = Input.mousePosition.y;

                // Debug.Log(mX + " : " + mY);

                if (mX < (PlayerPrefs.GetFloat("ScreenWidth") / 8) && mY < (PlayerPrefs.GetFloat("ScreenHeight") / 8)) // corners
                {
                    lowerLeftCorner.Play("cornerGrow");
                    lowerRightCorner.Play("shrunk");
                    upperLeftCorner.Play("shrunk");
                    upperRightCorner.Play("shrunk");
                    fullFillAnim.Play("shrunk");
                    leftFillAnim.Play("shrunk");
                    rightFillAnim.Play("shrunk");

                    fill_LL_Corner = true;
                    fill_LU_Corner = false;
                    fill_RL_Corner = false;
                    fill_RU_Corner = false;
                    fullFull = false;
                    leftFill = false;
                    rightFill = false;
                    snapBack = false;

                    ShrinkCornerBoxes();
                } else if (mX > PlayerPrefs.GetFloat("ScreenWidth") - (PlayerPrefs.GetFloat("ScreenWidth") / 8) && mY < (PlayerPrefs.GetFloat("ScreenHeight") / 8)) // corners
                    {
                        lowerLeftCorner.Play("shrunk");
                        lowerRightCorner.Play("cornerGrow");
                        upperLeftCorner.Play("shrunk");
                        upperRightCorner.Play("shrunk");
                        fullFillAnim.Play("shrunk");
                        leftFillAnim.Play("shrunk");
                        rightFillAnim.Play("shrunk");

                        fill_LL_Corner = false;
                        fill_LU_Corner = true;
                        fill_RL_Corner = false;
                        fill_RU_Corner = false;
                        fullFull = false;
                        leftFill = false;
                        rightFill = false;
                        snapBack = false;

                        ShrinkCornerBoxes();
                    } else if (mX < (PlayerPrefs.GetFloat("ScreenWidth") / 8) && mY > PlayerPrefs.GetFloat("ScreenHeight") - (PlayerPrefs.GetFloat("ScreenHeight") / 8)) // corners
                        {
                            lowerLeftCorner.Play("shrunk");
                            lowerRightCorner.Play("shrunk");
                            upperLeftCorner.Play("cornerGrow");
                            upperRightCorner.Play("shrunk");
                            fullFillAnim.Play("shrunk");
                            leftFillAnim.Play("shrunk");
                            rightFillAnim.Play("shrunk");

                            fill_LL_Corner = false;
                            fill_LU_Corner = false;
                            fill_RL_Corner = true;
                            fill_RU_Corner = false;
                            fullFull = false;
                            leftFill = false;
                            rightFill = false;
                            snapBack = false;

                            ShrinkCornerBoxes();
                        } else if (mX > PlayerPrefs.GetFloat("ScreenWidth") - (PlayerPrefs.GetFloat("ScreenWidth") / 8) && mY > PlayerPrefs.GetFloat("ScreenHeight") - (PlayerPrefs.GetFloat("ScreenHeight") / 8)) // corners
                            {
                                lowerLeftCorner.Play("shrunk");
                                lowerRightCorner.Play("shrunk");
                                upperLeftCorner.Play("shrunk");
                                upperRightCorner.Play("cornerGrow");
                                fullFillAnim.Play("shrunk");
                                leftFillAnim.Play("shrunk");
                                rightFillAnim.Play("shrunk");

                                fill_LL_Corner = false;
                                fill_LU_Corner = false;
                                fill_RL_Corner = false;
                                fill_RU_Corner = true;
                                fullFull = false;
                                leftFill = false;
                                rightFill = false;
                                snapBack = false;

                                ShrinkCornerBoxes();
                            } else if (mX > ((PlayerPrefs.GetFloat("ScreenWidth") / 2) - (PlayerPrefs.GetFloat("ScreenWidth") / 8)) && mX < ((PlayerPrefs.GetFloat("ScreenWidth") / 2) + (PlayerPrefs.GetFloat("ScreenWidth") / 8)) && mY > PlayerPrefs.GetFloat("ScreenHeight") - (PlayerPrefs.GetFloat("ScreenHeight") / 8)) // fullscreen
                                {
                                    lowerLeftCorner.Play("shrunk");
                                    lowerRightCorner.Play("shrunk");
                                    upperLeftCorner.Play("shrunk");
                                    upperRightCorner.Play("shrunk");
                                    fullFillAnim.Play("sideFillGrow");
                                    leftFillAnim.Play("shrunk");
                                    rightFillAnim.Play("shrunk");

                                    fill_LL_Corner = false;
                                    fill_LU_Corner = false;
                                    fill_RL_Corner = false;
                                    fill_RU_Corner = false;
                                    fullFull = true;
                                    leftFill = false;
                                    rightFill = false;
                                    snapBack = false;

                                    ShrinkCornerBoxes();
                                } else if (mY > ((PlayerPrefs.GetFloat("ScreenHeight") / 2) - (PlayerPrefs.GetFloat("ScreenHeight") / 8)) && mY < ((PlayerPrefs.GetFloat("ScreenHeight") / 2) + (PlayerPrefs.GetFloat("ScreenHeight") / 8)) && mX < (PlayerPrefs.GetFloat("ScreenWidth") / 8)) // left fill
                                    {
                                        lowerLeftCorner.Play("shrunk");
                                        lowerRightCorner.Play("shrunk");
                                        upperLeftCorner.Play("shrunk");
                                        upperRightCorner.Play("shrunk");
                                        fullFillAnim.Play("shrunk");
                                        leftFillAnim.Play("sideHalfFillGrow");
                                        rightFillAnim.Play("shrunk");

                                        fill_LL_Corner = false;
                                        fill_LU_Corner = false;
                                        fill_RL_Corner = false;
                                        fill_RU_Corner = false;
                                        fullFull = false;
                                        leftFill = true;
                                        rightFill = false;
                                        snapBack = false;

                                        ShrinkCornerBoxes();
                                    } else if (mY > ((PlayerPrefs.GetFloat("ScreenHeight") / 2) - (PlayerPrefs.GetFloat("ScreenHeight") / 8)) && mY < ((PlayerPrefs.GetFloat("ScreenHeight") / 2) + (PlayerPrefs.GetFloat("ScreenHeight") / 8)) && mX > PlayerPrefs.GetFloat("ScreenWidth") - (PlayerPrefs.GetFloat("ScreenWidth") / 8)) // right fill
                                        {
                                            lowerLeftCorner.Play("shrunk");
                                            lowerRightCorner.Play("shrunk");
                                            upperLeftCorner.Play("shrunk");
                                            upperRightCorner.Play("shrunk");
                                            fullFillAnim.Play("shrunk");
                                            leftFillAnim.Play("shrunk");
                                            rightFillAnim.Play("sideHalfFillGrow");

                                            fill_LL_Corner = false;
                                            fill_LU_Corner = false;
                                            fill_RL_Corner = false;
                                            fill_RU_Corner = false;
                                            fullFull = false;
                                            leftFill = false;
                                            rightFill = true;
                                            snapBack = false;

                                            ShrinkCornerBoxes();
                                        } else
                                            {
                                                fill_LL_Corner = false;
                                                fill_LU_Corner = false;
                                                fill_RL_Corner = false;
                                                fill_RU_Corner = false;
                                                fullFull = false;
                                                leftFill = false;
                                                rightFill = false;
                                                snapBack = false;

                                                ClearCornerBoxes();
                                            }
            }
            
        } else // no mouse clicks
            {
                if (ComputerScript.isPageBeingModified())
                {
                    // Debug.Log("Mouse Released After Action. . .");

                    ComputerScript.isRescaling(false);

                    lowerLeftCorner.Play("shrunk");
                    lowerRightCorner.Play("shrunk");
                    upperLeftCorner.Play("shrunk");
                    upperRightCorner.Play("shrunk");
                    fullFillAnim.Play("shrunk");
                    leftFillAnim.Play("shrunk");
                    rightFillAnim.Play("shrunk");

                    if (fill_LL_Corner)
                    {
                        ComputerScript.MoveToSnapPosition(Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow(), -(PlayerPrefs.GetFloat("ScreenWidth") / 4), -(PlayerPrefs.GetFloat("ScreenHeight") / 4));
                    } else if (fill_LU_Corner)
                        {
                            ComputerScript.MoveToSnapPosition(Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow(), (PlayerPrefs.GetFloat("ScreenWidth") / 4), -(PlayerPrefs.GetFloat("ScreenHeight") / 4));
                        } else if (fill_RL_Corner)
                            {
                                ComputerScript.MoveToSnapPosition(Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow(), -(PlayerPrefs.GetFloat("ScreenWidth") / 4), (PlayerPrefs.GetFloat("ScreenHeight") / 4));
                            } else if (fill_RU_Corner)
                                {
                                    ComputerScript.MoveToSnapPosition(Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow(), (PlayerPrefs.GetFloat("ScreenWidth") / 4), (PlayerPrefs.GetFloat("ScreenHeight") / 4));
                                } else if (fullFull)
                                    {
                                        Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow().GetComponent<AppWindow>().MaximizePage();
                                    } else if (leftFill)
                                        {
                                            ComputerScript.FillHalfScreen(Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow(), -(PlayerPrefs.GetFloat("ScreenWidth") / 4), 0);
                                        } else if (rightFill)
                                            {
                                                ComputerScript.FillHalfScreen(Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow(), (PlayerPrefs.GetFloat("ScreenWidth") / 4), 0);
                                            } else if (snapBack)
                                                {

                                                };

                    if (ComputerScript.isMovingPage())
                    {
                        Selected_GUIObject.GetComponent<AppWindowComponent>().Get_MainWindow().GetComponent<AppWindow>().CheckNewPosition();
                    }

                    ComputerScript.prepareWindowRepositioning(false); // sets the page as stationary
                    
                    ComputerScript.SetNewAppScale(); // saves scale modification

                    ComputerScript.setModification(false);

                    fill_LL_Corner = false;
                    fill_LU_Corner = false;
                    fill_RL_Corner = false;
                    fill_RU_Corner = false;
                    fullFull = false;
                    leftFill = false;
                    rightFill = false;
                    snapBack = false;

                    GUI_Selected = false;
                }
            }
    }

    void ShrinkCornerBoxes()
    {
        lowerLeftCorner.SetBool("shrink", false);
        lowerRightCorner.SetBool("shrink", false);
        upperLeftCorner.SetBool("shrink", false);
        upperRightCorner.SetBool("shrink", false);
        fullFillAnim.SetBool("shrink", false);
        leftFillAnim.SetBool("shrink", false);
        rightFillAnim.SetBool("shrink", false);
    }

    void ClearCornerBoxes()
    {
        lowerLeftCorner.SetBool("shrink", true);
        lowerRightCorner.SetBool("shrink", true);
        upperLeftCorner.SetBool("shrink", true);
        upperRightCorner.SetBool("shrink", true);
        fullFillAnim.SetBool("shrink", true);
        leftFillAnim.SetBool("shrink", true);
        rightFillAnim.SetBool("shrink", true);
    }

}//EndScript