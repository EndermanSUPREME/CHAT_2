using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Ender.Computer
{
    public class ComputerScript : MonoBehaviour
    {
        static Vector3 refPoint, newPoint;
        static bool beingModified = false, rescaling = false, movingAppWindow = false;
        static AppWindow selectedAppWindow;

        public static void MoveMouse(Transform computerCursor, Vector3 pos)
        {
            computerCursor.position = pos;
        }

        public static void SetRefPoint(Vector3 v3)
        {
            refPoint = v3;
        }

        public static void setModification(bool value)
        {
            beingModified = value;
        }

        public static bool isPageBeingModified()
        {
            return beingModified;
        }

//================================ Window Scaling =====================================

        public static void isRescaling(bool value)
        {
            rescaling = value;
        }

        public static bool GetRescaleStatus()
        {
            return rescaling;
        }

        public static void RescalingWindow(RectTransform mainWindow, Vector3 nPos)
        {
            selectedAppWindow = mainWindow.transform.GetComponent<AppWindow>();

            if (!selectedAppWindow.Prepared())
            {
                selectedAppWindow.PrepareRescale();
            }

            newPoint = nPos;

            float x = refPoint.x - newPoint.x;
            float y = refPoint.y - newPoint.y;

            selectedAppWindow.ResizeWindow(x, y);
        }

        public static void SetAppWindow()
        {
            selectedAppWindow.PrepareRescale();
        }

        public static void SetNewAppScale()
        {
            if (selectedAppWindow != null)
            {
                selectedAppWindow.PushScaleChange();
            }
        }

//================================ Window Positioning =====================================

        public static void prepareWindowRepositioning(bool value)
        {
            movingAppWindow = value;
        }

        public static void setPagePositionReferences(Transform mainWindow)
        {
            mainWindow.GetComponent<AppWindow>().SetPageRefPosition();
        }

        public static bool isMovingPage()
        {
            return movingAppWindow;
        }

        public static void MoveAppPage(Transform mainWindow, Vector3 newPoint)
        {
            float x = refPoint.x - newPoint.x;
            float y = refPoint.y - newPoint.y;

            mainWindow.GetComponent<AppWindow>().MovePage(Input.mousePosition, x, y);
        }

        public static void MoveToSnapPosition(Transform mainWindow, float x, float y)
        {
            mainWindow.GetComponent<AppWindow>().MovePageToSnapPosition(x, y);
            mainWindow.GetComponent<AppWindow>().SnapScale();
        }

        public static void FillHalfScreen(Transform mainWindow, float x, float y)
        {
            mainWindow.GetComponent<AppWindow>().MovePageToSnapPosition(x, y);
            mainWindow.GetComponent<AppWindow>().ScaleHalfScreen();
        }
    }//EndScript
}