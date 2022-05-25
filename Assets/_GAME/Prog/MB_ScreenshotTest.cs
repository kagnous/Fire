using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_ScreenshotTest : MonoBehaviour
{
    public void ScreenShot()
    {
        Debug.Log("click");
        ScreenCapture.CaptureScreenshot("C:/Users/antoi/Desktop/TestScreenShot/monScreenshot.jpg");
    }
}