using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppWindow : MonoBehaviour
{
    [SerializeField] RectTransform MainWindow, WindowHead, WindowPivot;
    [SerializeField] float WindowWidth = 800, WindowHeight = 500, widthRef, heightRef;
    float pageX, pageY, refX, refY;
    bool refHasBeenSet = false;
    [SerializeField] Taskbar_Component taskBarIcon;

    void Update()
    {
        MainWindow.sizeDelta = new Vector2(WindowWidth, WindowHeight);
        WindowHead.sizeDelta = new Vector2(WindowWidth, WindowHead.sizeDelta.y);
    }

//================================ Window Scaling =====================================

    public void PrepareRescale()
    {
        widthRef = WindowWidth;
        heightRef = WindowHeight;

        refHasBeenSet = true;
    }

    public void PushScaleChange()
    {
        widthRef = WindowWidth;
        heightRef = WindowHeight;

        WindowWidth = widthRef;
        WindowHeight = heightRef;
    }

    public void ResizeWindow(float amountX, float amountY)
    {
        if (widthRef + amountX > 500)
        {
            WindowWidth = widthRef + amountX;
        } else
            {
                WindowWidth = 500;
            }

        if (heightRef + amountY > 300)
        {
            WindowHeight = heightRef + amountY;
        } else
            {
                WindowHeight = 300;
            }
    }

    public void SnapScale()
    {
        widthRef = PlayerPrefs.GetFloat("ScreenWidth") / 2;
        heightRef = PlayerPrefs.GetFloat("ScreenHeight") / 2;

        WindowWidth = widthRef;
        WindowHeight = heightRef;

        WindowPivot.sizeDelta = new Vector2(WindowWidth, WindowHeight);
    }

    public void ScaleHalfScreen()
    {
        widthRef = PlayerPrefs.GetFloat("ScreenWidth") / 2;
        heightRef = PlayerPrefs.GetFloat("ScreenHeight");

        WindowWidth = widthRef;
        WindowHeight = heightRef;

        WindowPivot.sizeDelta = new Vector2(WindowWidth, WindowHeight);
    }

    public bool Prepared()
    {
        return refHasBeenSet;
    }

//================================ Window Buttons =====================================

    public void MaximizePage()
    {
        // Debug.Log("Maximizing Page. . .");
        WindowWidth = PlayerPrefs.GetFloat("ScreenWidth");
        WindowHeight = PlayerPrefs.GetFloat("ScreenHeight");

        widthRef = WindowWidth;
        heightRef = WindowHeight;

        WindowPivot.sizeDelta = new Vector2(WindowWidth, WindowHeight);

        WindowPivot.position = WindowPivot.parent.position;
        // MainWindow.localPosition = new Vector3(WindowWidth/2, WindowHeight/2, 0);
    }

    public void MinimizePage()
    {
        // Window is not active and the TaskBar stays lit
        taskBarIcon.PageMinimizeStatus(true);
        taskBarIcon.SetIconColor(0);

        taskBarIcon.DisableWindow();

        // gameObject.SetActive(false);
    }

    public void ClosePage()
    {
        // Window is not active and the TaskBar goes grey
        taskBarIcon.PageMinimizeStatus(false);
        taskBarIcon.SetIconColor(1);
        taskBarIcon.GetShortcut().ClosedStatus(true);

        taskBarIcon.DisableWindow();

        // gameObject.SetActive(false);
        if (transform.name.Equals("CommandPrompt_Window"))
        {
            transform.GetComponent<terminalScript>().EndTerminalInstance();
        }
    }

//================================ Window Positioning =====================================

    public void SetPageRefPosition()
    {
        refX = WindowPivot.position.x;
        refY = WindowPivot.position.y;
    }

    public void MovePage(Vector3 currentMousePosition, float x, float y)
    {
        pageX = x;
        pageY = y;

        WindowPivot.position = new Vector3(refX - pageX, refY - pageY, 0);
    }

    public void CheckNewPosition()
    {
        float p_basedScreenPositionY = MainWindow.position.y;
        float n_basedScreenPositionY = (MainWindow.position.y - MainWindow.sizeDelta.y);

        // Debug.Log(MainWindow.position.y + " : " + MainWindow.sizeDelta.y + " => " + p_basedScreenPositionY + " | " + n_basedScreenPositionY);

        if (p_basedScreenPositionY > PlayerPrefs.GetFloat("ScreenHeight"))
        {
            Debug.Log("Too High");
            MovePageToSnapPosition(WindowPivot.localPosition.x, (PlayerPrefs.GetFloat("ScreenHeight") - WindowPivot.sizeDelta.y) / 2);
        }

        if (n_basedScreenPositionY < 0)
        {
            Debug.Log("Too Low");
            MovePageToSnapPosition(WindowPivot.localPosition.x, -((PlayerPrefs.GetFloat("ScreenHeight") - WindowPivot.sizeDelta.y) / 2));
        }
    }

    public void MovePageToSnapPosition(float x, float y)
    {
        WindowPivot.localPosition = new Vector3(x, y, 0);
    }

    public void PullWindowUpFront()
    {
        WindowPivot.SetSiblingIndex(WindowPivot.parent.childCount - 1);
    }

//================================ Window Remodulizing =====================================

    public void PlayerChangedScreenResolutions()
    {
        WindowWidth = 500; WindowHeight = 300;
        widthRef = WindowWidth; heightRef = WindowHeight;

        MainWindow.sizeDelta = new Vector2(WindowWidth, WindowHeight);
        WindowHead.sizeDelta = new Vector2(WindowWidth, WindowHead.sizeDelta.y);
        WindowPivot.sizeDelta = new Vector2(WindowWidth, WindowHeight);

        pageX = 0; pageY = 0;
        refX = pageX; refY = pageY;

        MovePageToSnapPosition(pageX, pageY);
    }

}//EndScript