using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppWindowComponent : MonoBehaviour
{
    [SerializeField] Transform MainWindow;

    public Transform Get_MainWindow() // attaches to things like the minimize, maximize, & close buttons
    {
        return MainWindow;
    }
}//EndScript