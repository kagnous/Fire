using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MB_ScreenshotTest))]
public class E_ScreenshotTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();    

        MB_ScreenshotTest screenshotTest = target as MB_ScreenshotTest;
        EditorGUILayout.Space();


        if (GUILayout.Button("save"))
        {
            screenshotTest.ScreenShot();
        }
    }
}