/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used overwriting the Inspector GUI, and Scene GUI
*******************************************************************************************/
using UnityEditor;
using UnityEngine;
using System.Collections;

namespace CircularGravityForce
{
    [CustomEditor(typeof(Follow)), CanEditMultipleObjects()]
    public class Follow_Editor : Editor
    {
        private SerializedProperty enable_property;
        private SerializedProperty interpolation_property;
        private SerializedProperty translateType_property;
        private SerializedProperty targetType_property;
        private SerializedProperty targetTransform_property;
        private SerializedProperty targetVector_property;
        private SerializedProperty updateType_property;
        private SerializedProperty disableVelocity_property;
        private SerializedProperty disableAngularVelocity_property;
        private SerializedProperty updateOn_property;
        private SerializedProperty marginDistance_property;
        private SerializedProperty useLocalOffset_property;
        private SerializedProperty positionOffset_property;
        private SerializedProperty upVectorAlignment_property;
        private SerializedProperty usePositionOffsetWithRotate_property;
        private SerializedProperty rotateOffset_property;
        private SerializedProperty lockPosition_property;
        private SerializedProperty lockRotation_property;
        private SerializedProperty scaleByDistance_property;
        private SerializedProperty scaleFactor_property;
        private SerializedProperty moveSpeed_property;
        private SerializedProperty rotateSpeed_property;

        private bool change = false;

        public void OnEnable()
        {
            enable_property = serializedObject.FindProperty("enable");
            interpolation_property = serializedObject.FindProperty("interpolation");
            translateType_property = serializedObject.FindProperty("translateType");
            targetType_property = serializedObject.FindProperty("targetType");
            targetTransform_property = serializedObject.FindProperty("targetTransform");
            targetVector_property = serializedObject.FindProperty("targetVector");
            updateType_property = serializedObject.FindProperty("updateType");
            disableVelocity_property = serializedObject.FindProperty("disableVelocity");
            disableAngularVelocity_property = serializedObject.FindProperty("disableAngularVelocity");
            updateOn_property = serializedObject.FindProperty("updateOn");
            marginDistance_property = serializedObject.FindProperty("marginDistance");
            useLocalOffset_property = serializedObject.FindProperty("useLocalOffset");
            positionOffset_property = serializedObject.FindProperty("positionOffset");
            upVectorAlignment_property = serializedObject.FindProperty("upVectorAlignment");
            usePositionOffsetWithRotate_property = serializedObject.FindProperty("usePositionOffsetWithRotate");
            rotateOffset_property = serializedObject.FindProperty("rotateOffset");
            lockPosition_property = serializedObject.FindProperty("lockPosition");
            lockRotation_property = serializedObject.FindProperty("lockRotation");
            scaleByDistance_property = serializedObject.FindProperty("scaleByDistance");
            scaleFactor_property = serializedObject.FindProperty("scaleFactor");
            moveSpeed_property = serializedObject.FindProperty("moveSpeed");
            rotateSpeed_property = serializedObject.FindProperty("rotateSpeed");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            int translateTypeSelected = -1;
            switch (translateType_property.enumValueIndex)
            {
                case (int)Follow.TranslateType.Move:
                    translateTypeSelected = 0;
                    break;
                case (int)Follow.TranslateType.Rotate:
                    translateTypeSelected = 1;
                    break;
                case (int)Follow.TranslateType.Both:
                    translateTypeSelected = 2;
                    break;
            }

            /*<-------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(enable_property, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                change = true;
            }
            /*------------------------------------------------------------------------------------------------->*/

            /*<-------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(interpolation_property, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                change = true;
            }
            /*------------------------------------------------------------------------------------------------->*/


            /*<-------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(translateType_property, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                change = true;
            }
            /*------------------------------------------------------------------------------------------------->*/

            /*<-------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(targetType_property, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                change = true;
            }
            /*------------------------------------------------------------------------------------------------->*/

            switch (targetType_property.enumValueIndex)
            {
                case (int)Follow.TargetType.Transform:
                    /*<-------------------------------------------------------------------------------------------------*/
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(targetTransform_property, true);
                    if (EditorGUI.EndChangeCheck())
                    {
                        serializedObject.ApplyModifiedProperties();
                        change = true;
                    }
                    /*------------------------------------------------------------------------------------------------->*/
                    break;
                case (int)Follow.TargetType.Vector:
                    /*<-------------------------------------------------------------------------------------------------*/
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(targetVector_property, true);
                    if (EditorGUI.EndChangeCheck())
                    {
                        serializedObject.ApplyModifiedProperties();
                        change = true;
                    }
                    /*------------------------------------------------------------------------------------------------->*/
                    break;
            }

            /*<-------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(updateType_property, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                change = true;
            }
            /*------------------------------------------------------------------------------------------------->*/

            /*<-------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(disableVelocity_property, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                change = true;
            }
            /*------------------------------------------------------------------------------------------------->*/

            /*<-------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(disableAngularVelocity_property, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                change = true;
            }
            /*------------------------------------------------------------------------------------------------->*/

            /*<-------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(updateOn_property, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                change = true;
            }
            /*------------------------------------------------------------------------------------------------->*/

            if (translateTypeSelected == 0 || translateTypeSelected == 2)
            {
                /*<-------------------------------------------------------------------------------------------------*/
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(marginDistance_property, true);
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    change = true;
                }
                /*------------------------------------------------------------------------------------------------->*/
            }

            /*<-------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(useLocalOffset_property, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                change = true;
            }
            /*------------------------------------------------------------------------------------------------->*/

            /*<-------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(positionOffset_property, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                change = true;
            }
            /*------------------------------------------------------------------------------------------------->*/

            if (translateTypeSelected == 1 || translateTypeSelected == 2)
            {
                /*<-------------------------------------------------------------------------------------------------*/
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(upVectorAlignment_property, true);
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    change = true;
                }
                /*------------------------------------------------------------------------------------------------->*/

                /*<-------------------------------------------------------------------------------------------------*/
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(usePositionOffsetWithRotate_property, true);
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    change = true;
                }
                /*------------------------------------------------------------------------------------------------->*/

                /*<-------------------------------------------------------------------------------------------------*/
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(rotateOffset_property, true);
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    change = true;
                }
                /*------------------------------------------------------------------------------------------------->*/
            }

            if (translateTypeSelected == 0 || translateTypeSelected == 2)
            {
                /*<-------------------------------------------------------------------------------------------------*/
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(lockPosition_property, true);
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    change = true;
                }
                /*------------------------------------------------------------------------------------------------->*/
            }

            if (translateTypeSelected == 1 || translateTypeSelected == 2)
            {
                /*<-------------------------------------------------------------------------------------------------*/
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(lockRotation_property, true);
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    change = true;
                }
                /*------------------------------------------------------------------------------------------------->*/
            }

            /*<-------------------------------------------------------------------------------------------------*/
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(scaleByDistance_property, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                change = true;
            }
            /*------------------------------------------------------------------------------------------------->*/

            if (scaleByDistance_property.boolValue)
            {
                /*<-------------------------------------------------------------------------------------------------*/
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(scaleFactor_property, true);
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    change = true;
                }
                /*------------------------------------------------------------------------------------------------->*/
            }
            if (interpolation_property.boolValue)
            {
                if (translateTypeSelected == 0 || translateTypeSelected == 2)
                {
                    /*<-------------------------------------------------------------------------------------------------*/
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(moveSpeed_property, true);
                    if (EditorGUI.EndChangeCheck())
                    {
                        serializedObject.ApplyModifiedProperties();
                        change = true;
                    }
                    /*------------------------------------------------------------------------------------------------->*/
                }

                if (translateTypeSelected == 1 || translateTypeSelected == 2)
                {
                    /*<-------------------------------------------------------------------------------------------------*/
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(rotateSpeed_property, true);
                    if (EditorGUI.EndChangeCheck())
                    {
                        serializedObject.ApplyModifiedProperties();
                        change = true;
                    }
                    /*------------------------------------------------------------------------------------------------->*/
                }
            }

            if (change)
            {
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
    }
}