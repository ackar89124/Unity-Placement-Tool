using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;


/// <summary>
/// 
///     This is a basic tool to quickly move/rotate a selected object by a set amout with a single click of a button.
/// 
///     This tool may be used in commercial, educational or non-commercial usage without request or fees.
///     All copyright to this code belongs to its creator © Jordan Wright and if edited, must clearly be display which parts 
///     have been edited and the copyright notice must be included with all versions.
///     
///     Personal Contact details: ackar89124@hotmail.co.uk
/// 
/// /// </summary>

public class PositionHelperPro : EditorWindow
{
    int posSlider;
    int RotSlider;
    float pos;
    float rot;
    float posChange;
    float rotChange;

    GameObject selected;
    List<float> disLis = new List<float>() { 0.1f, 0.2f, 0.25f, 0.5f, 1, 2, 5, 10, 20, 100 };   // List of distance variables
    List<float> rotLis = new List<float>() { 1, 5, 10, 25, 33.3f, 45, 90, 135, 180, 270 };      // List of rotation variables
    static List<float> disLisReset = new List<float>() { 0.1f, 0.2f, 0.25f, 0.5f, 1, 2, 5, 10, 20, 100 };   // List of distance variables
    static List<float> rotLisReset = new List<float>() { 1, 5, 10, 25, 33.3f, 45, 90, 135, 180, 270 };      // List of rotation variables

    [MenuItem("Tools/Position Helper Pro")]
    public static void ShowWindow()
    {
        GetWindow(typeof(PositionHelperPro));
    }

    private void OnGUI()
    {
        if (disLis.Count < 1)
        {
            disLis.Add(0);
        }
        if (rotLis.Count < 1)
        {
            rotLis.Add(0);
        }

        GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };

        selected = Selection.activeGameObject;      // Object currently selected

        GUILayout.Label("Position", EditorStyles.boldLabel);

        posSlider = EditorGUILayout.IntSlider("Translate Rate", posSlider, 0, disLis.Count - 1);    // Position slider
        FindPos();
        EditorGUILayout.LabelField(pos.ToString(), style, GUILayout.ExpandWidth(true));

        GUILayout.BeginHorizontal();    // Position buttons
        if (GUILayout.Button("X+"))
        {
            MoveObject(0);
        }
        if (GUILayout.Button("X-"))
        {
            MoveObject(1);
        }
        if (GUILayout.Button("Y+"))
        {
            MoveObject(2);
        }
        if (GUILayout.Button("Y-"))
        {
            MoveObject(3);
        }
        if (GUILayout.Button("Z+"))
        {
            MoveObject(4);
        }
        if (GUILayout.Button("Z-"))
        {
            MoveObject(5);
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Rotation", EditorStyles.boldLabel);

        RotSlider = EditorGUILayout.IntSlider("Rotation Rate", RotSlider, 0, rotLis.Count - 1);     // Rotation slider
        FindRot();
        EditorGUILayout.LabelField(rot.ToString(), style, GUILayout.ExpandWidth(true));

        GUILayout.BeginHorizontal();    // Rotation buttons
        if (GUILayout.Button("X+"))
        {
            RotateObject(0);
        }
        if (GUILayout.Button("X-"))
        {
            RotateObject(1);
        }
        if (GUILayout.Button("Y+"))
        {
            RotateObject(2);
        }
        if (GUILayout.Button("Y-"))
        {
            RotateObject(3);
        }
        if (GUILayout.Button("Z+"))
        {
            RotateObject(4);
        }
        if (GUILayout.Button("Z-"))
        {
            RotateObject(5);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Move to nearest", EditorStyles.label);
        if (GUILayout.Button("n.00"))
        {
            MoveToNearest(0);
        }
        if (GUILayout.Button("0.n0"))
        {
            MoveToNearest(1);
        }
        if (GUILayout.Button("0.0n"))
        {
            MoveToNearest(2);
        }
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        posChange = EditorGUILayout.FloatField("Change Selected Position", posChange);
        if (GUILayout.Button("Update"))
        {
            disLis[posSlider] = posChange;
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        rotChange = EditorGUILayout.FloatField("Change Selected Rotation", rotChange);
        if (GUILayout.Button("Update"))
        {
            rotLis[RotSlider] = rotChange;
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset Positions"))
        {
            disLis = disLisReset;
        }
        if (GUILayout.Button("Reset Rotations"))
        {
            rotLis = rotLisReset;
        }
        GUILayout.EndHorizontal();
    }

    private void FindPos()     // Sets movement rate based on scale
    {
        pos = disLis[posSlider];
    }

    private void FindRot()     // Sets rotation rate based on scale
    {
        rot = rotLis[RotSlider];
    }

    private void MoveToNearest(int dec)
    {
        if (dec == 0)
        {
            selected.transform.localPosition = new Vector3(Mathf.Round(selected.transform.localPosition.x),
                Mathf.Round(selected.transform.localPosition.y),
                Mathf.Round(selected.transform.localPosition.z));
        }
        else if (dec == 1)
        {
            selected.transform.localPosition = new Vector3((float)Math.Round(selected.transform.localPosition.x, 1),
                (float)Math.Round(selected.transform.localPosition.y, 1),
                (float)Math.Round(selected.transform.localPosition.z, 1));
        }
        else if (dec == 2)
        {
            selected.transform.localPosition = new Vector3((float)Math.Round(selected.transform.localPosition.x, 2),
                (float)Math.Round(selected.transform.localPosition.y, 2),
                (float)Math.Round(selected.transform.localPosition.z, 2));
        }
    }

    private void MoveObject(int dir)    // Changes postion based on request direction and scale
    {
        if (selected != null)
        {
            switch (dir)
            {
                case 0: 
                    {
                        selected.transform.localPosition = new Vector3(
                            selected.transform.localPosition.x + pos,
                            selected.transform.localPosition.y,
                            selected.transform.localPosition.z);
                        break;
                    }
                case 1:
                    {
                        selected.transform.localPosition = new Vector3(
                            selected.transform.localPosition.x - pos,
                            selected.transform.localPosition.y,
                            selected.transform.localPosition.z);
                        break;
                    }
                case 2:
                    {
                        selected.transform.localPosition = new Vector3(
                            selected.transform.localPosition.x,
                            selected.transform.localPosition.y + pos,
                            selected.transform.localPosition.z);
                        break;
                    }
                case 3:
                    {
                        selected.transform.localPosition = new Vector3(
                            selected.transform.localPosition.x,
                            selected.transform.localPosition.y - pos,
                            selected.transform.localPosition.z);
                        break;
                    }
                case 4:
                    {
                        selected.transform.localPosition = new Vector3(
                            selected.transform.localPosition.x,
                            selected.transform.localPosition.y,
                            selected.transform.localPosition.z + pos);
                        break;
                    }
                case 5:
                    {
                        selected.transform.localPosition = new Vector3(
                            selected.transform.localPosition.x,
                            selected.transform.localPosition.y,
                            selected.transform.localPosition.z - pos);
                        break;
                    }
                default:
                    {

                        break;
                    }
            }

        }

    }

    private void RotateObject(int dir)    // Changes rotation based on request direction and scale
    {
        if (selected != null)
        {
            switch (dir)
            {
                case 0:
                    {
                        selected.transform.localEulerAngles = new Vector3(
                            selected.transform.localEulerAngles.x + rot,
                            selected.transform.localEulerAngles.y,
                            selected.transform.localEulerAngles.z);
                        break;
                    }
                case 1:
                    {
                        selected.transform.localEulerAngles = new Vector3(
                            selected.transform.localEulerAngles.x - rot,
                            selected.transform.localEulerAngles.y,
                            selected.transform.localEulerAngles.z);
                        break;
                    }
                case 2:
                    {
                        selected.transform.localEulerAngles = new Vector3(
                            selected.transform.localEulerAngles.x,
                            selected.transform.localEulerAngles.y + rot,
                            selected.transform.localEulerAngles.z);
                        break;
                    }
                case 3:
                    {
                        selected.transform.localEulerAngles = new Vector3(
                            selected.transform.localEulerAngles.x,
                            selected.transform.localEulerAngles.y - rot,
                            selected.transform.localEulerAngles.z);
                        break;
                    }
                case 4:
                    {
                        selected.transform.localEulerAngles = new Vector3(
                            selected.transform.localEulerAngles.x,
                            selected.transform.localEulerAngles.y,
                            selected.transform.localEulerAngles.z + rot);
                        break;
                    }
                case 5:
                    {
                        selected.transform.localEulerAngles = new Vector3(
                            selected.transform.localEulerAngles.x,
                            selected.transform.localEulerAngles.y,
                            selected.transform.localEulerAngles.z - rot);
                        break;
                    }
                default:
                    {

                        break;
                    }
            }

        }

    }
}
