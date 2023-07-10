using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Taskbar_Component : MonoBehaviour
{
    [SerializeField] Color greyed, full;
    [SerializeField] GameObject WindowObject;
    bool pageIsMinimized = false;
    [SerializeField] AppShortcut shortcut;
    [SerializeField] AppWindow selectedAppWindow;

    void Start()
    {
        if (WindowObject != null)
        {
            WindowObject.SetActive(false);
            SetIconColor(1);
        }
    }

    public void UnMinimizePage()
    {
        // Debug.Log("PageClosed => " + shortcut.isPageOpen());

        if (!shortcut.isPageOpen())
        {
            WindowObject.SetActive(true);
            PageMinimizeStatus(false);
            SetIconColor(0);
        }
    }

    public void DisableWindow()
    {
        WindowObject.SetActive(false);
    }

    public AppShortcut GetShortcut()
    {
        return shortcut;
    }

    public AppWindow GetAppWindow()
    {
        return selectedAppWindow;
    }

    public void SetIconColor(int i)
    {
        if (i == 0) GetComponent<Image>().color = full; else GetComponent<Image>().color = greyed;
    }

    public void PageMinimizeStatus(bool value)
    {
        pageIsMinimized = value;
    }

    public bool isPageMinimized()
    {
        return pageIsMinimized;
    }
}//EndScirpt