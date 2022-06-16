using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WindInstancier))]
public class WindEditor : Editor
{
    private WindInstancier.BuiltInConfig _presetConfigState = WindInstancier.BuiltInConfig.FrequentLoop;
    private WindInstancier.BuiltInConfig _loopState = WindInstancier.BuiltInConfig.NormalLength;
    private WindInstancier.BuiltInConfig _widthState = WindInstancier.BuiltInConfig.FrequentLoop;
    private WindInstancier.BuiltInConfig _lengthState = WindInstancier.BuiltInConfig.FrequentLoop;

    public override void OnInspectorGUI()
    {
        WindInstancier myTarget = target as WindInstancier;

        EditorGUILayout.BeginVertical("helpBox");
        {
            EditorGUILayout.LabelField("PRESETS", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            {
                if (_presetConfigState != WindInstancier.BuiltInConfig.LowWind)
                {
                    if (GUILayout.Button("Low Wind"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.LowWind);
                        _presetConfigState = WindInstancier.BuiltInConfig.LowWind;
                    } 
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("Low Wind"))
                    {

                    }
                    GUI.enabled = true;
                }

                if (_presetConfigState != WindInstancier.BuiltInConfig.MediumWind)
                {
                    if (GUILayout.Button("Medium Wind"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.MediumWind);
                        _presetConfigState = WindInstancier.BuiltInConfig.MediumWind;
                    }
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("Medium Wind"))
                    {

                    }
                    GUI.enabled = true;
                }

                if (_presetConfigState != WindInstancier.BuiltInConfig.HighWind)
                {
                    if (GUILayout.Button("High Wind"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.HighWind);
                        _presetConfigState = WindInstancier.BuiltInConfig.HighWind;
                    }
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("High Wind"))
                    {

                    }
                    GUI.enabled = true;
                }

                
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Debug Presets");

            EditorGUILayout.BeginHorizontal();
            {
                if (_presetConfigState != WindInstancier.BuiltInConfig.EveryFrameDebug)
                {
                    if (GUILayout.Button("Every Frame Debug"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.EveryFrameDebug);
                        _presetConfigState = WindInstancier.BuiltInConfig.EveryFrameDebug;
                    } 
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("Every Frame Debug"))
                    {

                    }
                    GUI.enabled = true;
                }

                if (_presetConfigState != WindInstancier.BuiltInConfig.LoopDebug)
                {
                    if (GUILayout.Button("Loop Debug"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.LoopDebug);
                        _presetConfigState = WindInstancier.BuiltInConfig.LoopDebug;
                    }
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("Loop Debug"))
                    {

                    }
                    GUI.enabled = true;
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("preset blend");

            EditorGUILayout.BeginHorizontal();
            {
                if (_loopState != WindInstancier.BuiltInConfig.NoLoop)
                {
                    if (GUILayout.Button("No Loop"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.NoLoop);
                        _loopState = WindInstancier.BuiltInConfig.NoLoop;
                    } 
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("No Loop"))
                    {

                    }
                    GUI.enabled = true;
                }

                if (_loopState != WindInstancier.BuiltInConfig.NormalLoop)
                {
                    if (GUILayout.Button("Normal Loop"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.NormalLoop);
                        _loopState = WindInstancier.BuiltInConfig.NormalLoop;
                    } 
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("Normal Loop"))
                    {

                    }
                    GUI.enabled = true;
                }

                if (_loopState != WindInstancier.BuiltInConfig.FrequentLoop)
                {
                    if (GUILayout.Button("Frequent Loop"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.FrequentLoop);
                        _loopState = WindInstancier.BuiltInConfig.FrequentLoop;
                    } 
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("Frequent Loop"))
                    {

                    }
                    GUI.enabled = true;
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                if (_widthState != WindInstancier.BuiltInConfig.SmallWidth)
                {
                    if (GUILayout.Button("Small Width"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.SmallWidth);
                        _widthState = WindInstancier.BuiltInConfig.SmallWidth;
                    } 
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("Small Width"))
                    {

                    }
                    GUI.enabled = true;
                }


                if (_widthState != WindInstancier.BuiltInConfig.NormalWidth)
                {
                    if (GUILayout.Button("Normal Width"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.NormalWidth);
                        _widthState = WindInstancier.BuiltInConfig.NormalWidth;
                    } 
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("Normal Width"))
                    {

                    }
                    GUI.enabled = true;
                }

                if (_widthState != WindInstancier.BuiltInConfig.LargeWidth)
                {
                    if (GUILayout.Button("Large Width"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.LargeWidth);
                        _widthState = WindInstancier.BuiltInConfig.LargeWidth;
                    } 
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("Large Width"))
                    {

                    }
                    GUI.enabled = true;
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                if (_lengthState != WindInstancier.BuiltInConfig.SmallLength)
                {
                    if (GUILayout.Button("Small Length"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.SmallLength);
                        _lengthState = WindInstancier.BuiltInConfig.SmallLength;
                    } 
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("Small Length"))
                    {

                    }
                    GUI.enabled = true;
                }

                if (_lengthState != WindInstancier.BuiltInConfig.NormalLength)
                {
                    if (GUILayout.Button("Normal Length"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.NormalLength);
                        _lengthState = WindInstancier.BuiltInConfig.NormalLength;
                    } 
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("Normal Length"))
                    {

                    }
                    GUI.enabled = true;
                }

                if (_lengthState != WindInstancier.BuiltInConfig.HighLength)
                {
                    if (GUILayout.Button("High Length"))
                    {
                        myTarget.SetBuiltInConfig(WindInstancier.BuiltInConfig.HighLength);
                        _lengthState = WindInstancier.BuiltInConfig.HighLength;
                    } 
                }
                else
                {
                    GUI.enabled = false;
                    if (GUILayout.Button("High Length"))
                    {

                    }
                    GUI.enabled = true;
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(20);

        EditorGUILayout.BeginVertical("helpBox");
        {
            EditorGUILayout.LabelField("ACTIVE CONFIG", EditorStyles.boldLabel);
            myTarget.WindConfig = (WindAsset)EditorGUILayout.ObjectField("Wind Config", myTarget.WindConfig, typeof(WindAsset), true);
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("___________________________________________________________________________________________________________________________________________________________________________________________");
        EditorGUILayout.Space(10);

        if (GUILayout.Button("\nClear All\n"))
            myTarget.ClearConfig();
    }
}

