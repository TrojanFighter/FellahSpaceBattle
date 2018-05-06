/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for quickly hooking events to the keyboard.
*******************************************************************************************/
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace CircularGravityForce
{
    public class InputEventTrigger : MonoBehaviour
    {
        [Serializable]
        public class ControlGroup
        {
            //Input type
            public enum PressType
            {
                Down,
                Up,
                Press
            }

            [Serializable]
            public class InputEvent
            {
                public KeyCode keycode;

                public UnityEvent keyEvent;

                public PressType pressType;

                [HideInInspector]
                public bool pressState;
            }

            //Event the ControlGroup is idle
            public UnityEvent idleEvent;

            //InputEvent groups
            public InputEvent[] keyboardEvents;

            [HideInInspector]
            public bool idleState = true;
        }

        public ControlGroup[] controlGroups;

        //Draws event gizmo lines
        private void OnDrawGizmosSelected()
        {
            if (controlGroups != null)
            {
                foreach (var controlGroup in controlGroups)
                {
                    GizmoHelper.DrawUnityEventGizmo(controlGroup.idleEvent, this.transform, Color.yellow);

                    if (controlGroup.keyboardEvents != null)
                    {
                        foreach (var keyboardEvent in controlGroup.keyboardEvents)
                        {
                            GizmoHelper.DrawUnityEventGizmo(keyboardEvent.keyEvent, this.transform, Color.cyan);
                        }
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var controlGroup in controlGroups)
            {
                foreach (var keyboardEvent in controlGroup.keyboardEvents)
                {
                    if (keyboardEvent.pressType == ControlGroup.PressType.Down)
                    {
                        if (Input.GetKeyDown(keyboardEvent.keycode))
                        {
                            keyboardEvent.pressState = true;
                            controlGroup.idleState = false;
                        }
                    }

                    if (keyboardEvent.pressType == ControlGroup.PressType.Up)
                    {
                        if (Input.GetKeyUp(keyboardEvent.keycode))
                        {
                            keyboardEvent.pressState = true;
                            controlGroup.idleState = true;
                        }
                    }

                    if (keyboardEvent.pressType == ControlGroup.PressType.Press)
                    {
                        if (Input.GetKey(keyboardEvent.keycode))
                        {
                            keyboardEvent.pressState = true;
                            controlGroup.idleState = false;
                        }
                    }
                }
            }

            foreach (var controlGroup in controlGroups)
            {
                foreach (var keyboardEvent in controlGroup.keyboardEvents)
                {
                    if (keyboardEvent.pressState)
                    {
                        keyboardEvent.keyEvent.Invoke();
                    }
                }

                if (controlGroup.idleState == true)
                {
                    controlGroup.idleEvent.Invoke();
                }
            }
        }

        void LateUpdate()
        {
            foreach (var controlGroup in controlGroups)
            {
                foreach (var keyboardEvent in controlGroup.keyboardEvents)
                {
                    keyboardEvent.pressState = false;
                }

                controlGroup.idleState = true;
            }
        }
    }
}