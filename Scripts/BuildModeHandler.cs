using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeHandler : MonoBehaviour
{
    [SerializeField] bool WebGL_Mode;

    public bool isWebGLMode()
    {
        return WebGL_Mode;
    }
}//EndScript