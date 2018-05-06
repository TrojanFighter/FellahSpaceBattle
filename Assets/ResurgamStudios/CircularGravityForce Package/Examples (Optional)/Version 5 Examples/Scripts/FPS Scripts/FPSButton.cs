/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for creating FPS buttons.
*******************************************************************************************/
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace CircularGravityForce
{
    public class FPSButton : MonoBehaviour
    {
        public Transform targetTransform;

        [Header("Gaze Events")]
        public UnityEvent onGazeEnter;
        public UnityEvent onGazeExit;

        [Header("Input Events")]
        public UnityEvent onClickDown;
        public UnityEvent onClickUp;
        public UnityEvent onClickPress;

        private bool isFocused = false;
        private bool isPressed = false;

        //Draws gizmos
        private void OnDrawGizmosSelected()
        {
            GizmoHelper.DrawUnityEventGizmo(onGazeEnter, this.transform, Color.green);
            GizmoHelper.DrawUnityEventGizmo(onGazeExit, this.transform, Color.red);
            GizmoHelper.DrawUnityEventGizmo(onClickDown, this.transform, Color.cyan);
            GizmoHelper.DrawUnityEventGizmo(onClickUp, this.transform, Color.cyan);
            GizmoHelper.DrawUnityEventGizmo(onClickPress, this.transform, Color.cyan);
        }

        //Awake is called when the script instance is being loaded
        void Awake()
        {
            FPSPlayerInput.OnGazeEnter += FPSPlayerGaze_OnGazeEnter;
            FPSPlayerInput.OnGazeExit += FPSPlayerGaze_OnGazeExit;

            FPSPlayerInput.OnClickDown += FPSPlayerInput_OnClickDown;
            FPSPlayerInput.OnClickUp += FPSPlayerInput_OnClickUp;
            FPSPlayerInput.OnClickPress += FPSPlayerInput_OnClickPress;
        }

        //This function is called when the MonoBehaviour will be destroyed
        void OnDestroy()
        {
            FPSPlayerInput.OnGazeEnter -= FPSPlayerGaze_OnGazeEnter;
            FPSPlayerInput.OnGazeExit -= FPSPlayerGaze_OnGazeExit;

            FPSPlayerInput.OnClickDown -= FPSPlayerInput_OnClickDown;
            FPSPlayerInput.OnClickUp -= FPSPlayerInput_OnClickUp;
            FPSPlayerInput.OnClickPress -= FPSPlayerInput_OnClickPress;
        }

        //On gaze enter
        private void FPSPlayerGaze_OnGazeEnter(Transform obj)
        {
            if (obj == targetTransform)
            {
                onGazeEnter.Invoke();

                isFocused = true;
            }
        }

        //On gaze exit
        private void FPSPlayerGaze_OnGazeExit(Transform obj)
        {
            if (obj == targetTransform)
            {
                onGazeExit.Invoke();

                isFocused = false;
            }
        }

        //On click down
        private void FPSPlayerInput_OnClickDown()
        {
            if (!isFocused)
                return;

            onClickDown.Invoke();

            isPressed = true;
        }

        //On click up
        private void FPSPlayerInput_OnClickUp()
        {
            if (isPressed)
            {
                onClickUp.Invoke();
            }

            isPressed = false;
        }

        //On click press
        private void FPSPlayerInput_OnClickPress()
        {
            if (!isFocused)
                return;

            onClickPress.Invoke();

            isPressed = true;
        }
    }
}