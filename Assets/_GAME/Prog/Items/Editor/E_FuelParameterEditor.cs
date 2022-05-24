using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SO_FuelParameters))]
public class E_FuelParameterEditor : Editor
{
    /*public override void OnInspectorGUI()
    {
        SO_FuelParameters fuelParameter = target as SO_FuelParameters;
        base.OnInspectorGUI();

        fuelParameter.ChangeColor = EditorGUILayout.Toggle("Change color", fuelParameter.ChangeColor);

        if (fuelParameter.ChangeColor)
            fuelParameter.Color = EditorGUILayout.GradientField("Colors", fuelParameter.Color);
    }*/
}