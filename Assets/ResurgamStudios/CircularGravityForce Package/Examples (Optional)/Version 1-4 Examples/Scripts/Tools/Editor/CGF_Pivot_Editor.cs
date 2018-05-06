/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: 
*******************************************************************************************/
using UnityEditor;
using UnityEngine;
using System;
using System.Collections;

namespace CircularGravityForce
{
    [CustomEditor(typeof(CGF_Pivot)), CanEditMultipleObjects]
    public class CGF_Pivot_Editor : Editor
    {
        private int curveHight = 64;

        private GUIStyle panelStyle;

        private SerializedProperty cgf_property;

        private SerializedProperty minValue_property;
        private SerializedProperty maxValue_property;
        private SerializedProperty animationCurve_property;
        private SerializedProperty minTime_property;
        private SerializedProperty maxTime_property;

        private bool change = false;

        public void OnEnable()
        {
            cgf_property = serializedObject.FindProperty("cgf");
            animationCurve_property = serializedObject.FindProperty("forceByDistance.animationCurve");
            minTime_property = serializedObject.FindProperty("forceByDistance.minTime");
            maxTime_property = serializedObject.FindProperty("forceByDistance.maxTime");
            minValue_property = serializedObject.FindProperty("forceByDistance.minValue");
            maxValue_property = serializedObject.FindProperty("forceByDistance.maxValue");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            /*<----------------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(cgf_property, new GUIContent("CGF"), GUILayout.ExpandWidth(true));
            if (EditorGUI.EndChangeCheck())
            {
                change = true;
            }
            /*<----------------------------------------------------------------------------------------------------------*/

            EditorGUIUtility.labelWidth = 80;

            panelStyle = new GUIStyle(GUI.skin.box);
            panelStyle.padding = new RectOffset(panelStyle.padding.left + 10, panelStyle.padding.right, panelStyle.padding.top, panelStyle.padding.bottom);
            EditorGUILayout.BeginVertical(panelStyle);

            EditorGUILayout.LabelField("Force by Distance", EditorStyles.boldLabel);

            /*<----------------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            GUILayout.BeginHorizontal();
            var animationCurveRect = EditorGUILayout.BeginVertical();
            animationCurve_property.animationCurveValue = EditorGUI.CurveField(animationCurveRect, "", EditorGUILayout.CurveField(animationCurve_property.animationCurveValue), Color.cyan, new Rect());
            GUILayout.Space(curveHight);
            EditorGUILayout.EndVertical();
            GUILayout.EndHorizontal();
            if (EditorGUI.EndChangeCheck())
            {
                int length = animationCurve_property.animationCurveValue.length;
                if (length > 1)
                {
                    minTime_property.floatValue = animationCurve_property.animationCurveValue.keys[0].time;
                    maxTime_property.floatValue = animationCurve_property.animationCurveValue.keys[animationCurve_property.animationCurveValue.length - 1].time;
                    minValue_property.floatValue = animationCurve_property.animationCurveValue.keys[0].value;
                    maxValue_property.floatValue = animationCurve_property.animationCurveValue.keys[animationCurve_property.animationCurveValue.length - 1].value;
                }
                else
                {
                    animationCurve_property.animationCurveValue = AnimationCurve.Linear(0f, 0f, 1f, 1f);
                }

                change = true;
            }
            /*<----------------------------------------------------------------------------------------------------------*/

            GUILayout.BeginHorizontal();
            /*<----------------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(minTime_property, new GUIContent("Min Distance"), GUILayout.ExpandWidth(true));
            if (EditorGUI.EndChangeCheck())
            {
                animationCurve_property.animationCurveValue = UpdateTimeCurve(animationCurve_property.animationCurveValue, true);

                change = true;
            }
            /*<----------------------------------------------------------------------------------------------------------*/
            /*<----------------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(maxTime_property, new GUIContent("Max Distance"), GUILayout.ExpandWidth(true));
            if (EditorGUI.EndChangeCheck())
            {
                animationCurve_property.animationCurveValue = UpdateTimeCurve(animationCurve_property.animationCurveValue, false);

                change = true;
            }
            /*<----------------------------------------------------------------------------------------------------------*/
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            /*<----------------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(minValue_property, new GUIContent("Min Force"), GUILayout.ExpandWidth(true));
            if (EditorGUI.EndChangeCheck())
            {
                animationCurve_property.animationCurveValue = UpdateValueCurve(animationCurve_property.animationCurveValue, true);

                change = true;
            }
            /*<----------------------------------------------------------------------------------------------------------*/
            /*<----------------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(maxValue_property, new GUIContent("Max Force"), GUILayout.ExpandWidth(true));
            if (EditorGUI.EndChangeCheck())
            {
                animationCurve_property.animationCurveValue = UpdateValueCurve(animationCurve_property.animationCurveValue, false);

                change = true;
            }
            /*<----------------------------------------------------------------------------------------------------------*/
            GUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            if (change)
            {
                int length = animationCurve_property.animationCurveValue.length;
                if (length > 1)
                {
                }
                else
                {
                    animationCurve_property.animationCurveValue = AnimationCurve.Linear(0f, 0f, 1f, 1f);
                }

                change = false;
            }

            serializedObject.ApplyModifiedProperties();
        }

        void OnSceneGUI()
        {
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }

        private AnimationCurve UpdateTimeCurve(AnimationCurve animationCurve, bool isStart)
        {
            int length = animationCurve.length;

            if (length > 1)
            {
                var updatedCurve = new AnimationCurve();

                for (int i = 0; i < length; i++)
                {
                    if (isStart)
                    {
                        if (i == 0)
                            updatedCurve.AddKey(new Keyframe(minTime_property.floatValue, animationCurve.keys[i].value));
                        else
                            updatedCurve.AddKey(animationCurve.keys[i]);
                    }
                    else
                    {
                        if (i == length - 1)
                            updatedCurve.AddKey(new Keyframe(maxTime_property.floatValue, animationCurve.keys[i].value));
                        else
                            updatedCurve.AddKey(animationCurve.keys[i]);
                    }
                }

                return updatedCurve;
            }

            return AnimationCurve.Linear(0f, 0f, 1f, 1f);
        }

        private AnimationCurve UpdateValueCurve(AnimationCurve animationCurve, bool isStart)
        {
            int length = animationCurve.length;

            if (length > 1)
            {
                var updatedCurve = new AnimationCurve();

                for (int i = 0; i < length; i++)
                {
                    if (isStart)
                    {
                        if (i == 0)
                            updatedCurve.AddKey(new Keyframe(animationCurve.keys[i].time, minValue_property.floatValue));
                        else
                            updatedCurve.AddKey(animationCurve.keys[i]);
                    }
                    else
                    {
                        if (i == length - 1)
                            updatedCurve.AddKey(new Keyframe(animationCurve.keys[i].time, maxValue_property.floatValue));
                        else
                            updatedCurve.AddKey(animationCurve.keys[i]);
                    }
                }

                return updatedCurve;
            }

            return AnimationCurve.Linear(0f, 0f, 1f, 1f);
        }
    }
}