using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppShortcut : MonoBehaviour
{
    [SerializeField] GameObject WindowObject;
    bool pageClosed = true;
    [SerializeField] Taskbar_Component taskBarIcon;
    [SerializeField] AppWindow selectedAppWindow;

    void Start()
    {
        if (WindowObject != null)
        {
            WindowObject.SetActive(false);
            ClosedStatus(true);
        }
    }

    public void OpenInstance()
    {
        if (pageClosed)
        {
            WindowObject.SetActive(true);
            ClosedStatus(false);

            taskBarIcon.PageMinimizeStatus(false);
            taskBarIcon.SetIconColor(0);
        }
    }

    public AppWindow GetAppWindow()
    {
        return selectedAppWindow;
    }

    public void ClosedStatus(bool value)
    {
        pageClosed = value;
    }

    public bool isPageOpen()
    {
        return pageClosed;
    }

}//EndScirpt