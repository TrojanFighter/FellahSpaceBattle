/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for managing FPS gaze inputs.
*******************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CircularGravityForce
{
    public class FPSPlayerInput : MonoBehaviour
    {
        //FPSPlayerInput Singleton
        static public FPSPlayerInput Instance { get { return instance; } }
        static protected FPSPlayerInput instance;

        //Gaze Events
        static public event Action<Transform> OnGazeEnter;
        static public event Action<Transform> OnGazeExit;
        
        //Input Events
        static public event Action OnClickDown;
        static public event Action OnClickUp;
        static public event Action OnClickPress;

        public LayerMask gazeLayerMask = -1;
        public float maxGazeDistance = 1.5f;

        public Transform cursorTransform;
        public Transform cursorModel;

        public Transform gazeTarget;
        public Transform lastGazeTarget;

        private Vector3 gazePosition;
        private Vector3 gazeOrigin;
        private Vector3 gazeDirection;

        //Awake is called when the script instance is being loaded
        void Awake()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (OnClickDown != null)
                    OnClickDown();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (OnClickUp != null)
                    OnClickUp();
            }

            if(Input.GetMouseButton(0))
            {
                if (OnClickPress != null)
                    OnClickPress();
            }

            SyncGaze();
        }

        //LateUpdate is called every frame, if the Behaviour is enabled
        void LateUpdate()
        {
            cursorTransform.gameObject.SetActive(gazeTarget != null);
            cursorModel.gameObject.SetActive(gazeTarget != null);

            if (gazeTarget == null)
            {
                cursorModel.position = gazePosition;

                return;
            }

            cursorTransform.position = gazePosition;
        }

        //Syncs the players gaze input
        void SyncGaze()
        {
            RaycastHit hitInfo;

            if (Camera.main == null)
                return;

            gazeOrigin = Camera.main.transform.position;
            gazeDirection = Camera.main.transform.forward;

            bool hit = false;

            hit = Physics.Raycast(gazeOrigin, gazeDirection, out hitInfo, maxGazeDistance, gazeLayerMask);

            if (hit)
            {
                if (hitInfo.distance < maxGazeDistance)
                {
                    if (gazeTarget != hitInfo.transform)
                    {
                        if (gazeTarget != null)
                        {
                            lastGazeTarget = gazeTarget;

                            if (OnGazeExit != null)
                                OnGazeExit(gazeTarget);
                        }

                        gazeTarget = hitInfo.transform;

                        if (OnGazeEnter != null)
                            OnGazeEnter(gazeTarget);
                    }

                    gazePosition = hitInfo.point;
                }
            }
            else
            {
                if (gazeTarget != null)
                {
                    lastGazeTarget = gazeTarget;

                    if (OnGazeExit != null)
                        OnGazeExit(gazeTarget);
                }

                gazeTarget = null;
                gazePosition = gazeOrigin + (gazeDirection * maxGazeDistance);
            }
        }
    }
}