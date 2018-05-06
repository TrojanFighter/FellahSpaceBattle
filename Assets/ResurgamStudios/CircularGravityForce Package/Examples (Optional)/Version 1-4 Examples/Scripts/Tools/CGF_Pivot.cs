/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for cgf mod, creates a pivot effect using the cgf.
*******************************************************************************************/
using UnityEngine;
using System.Collections;
using System;

namespace CircularGravityForce
{
    [RequireComponent(typeof(Follow))]
    public class CGF_Pivot : MonoBehaviour
    {
        [SerializeField]
        private CGF cgf;
        public CGF _cgf
        {
            get { return cgf; }
            set { cgf = value; }
        }

        [Serializable]
        public class CustomAnimationCurveObject
        {
            [SerializeField]
            private AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            public AnimationCurve _animationCurve
            {
                get { return animationCurve; }
                set { animationCurve = value; }
            }

            [SerializeField]
            private float minTime = 0f;
            public float MinTime
            {
                get { return minTime; }
                set { minTime = value; }
            }

            [SerializeField]
            private float maxTime = 1f;
            public float MaxTime
            {
                get { return maxTime; }
                set { maxTime = value; }
            }

            [SerializeField]
            private float minValue = 0f;
            public float MinValue
            {
                get { return minValue; }
                set { minValue = value; }
            }

            [SerializeField]
            private float maxValue = 1f;
            public float MaxValue
            {
                get { return maxValue; }
                set { maxValue = value; }
            }
        }

        [SerializeField]
        private CustomAnimationCurveObject forceByDistance;
        public CustomAnimationCurveObject ForceByDistance
        {
            get { return forceByDistance; }
            set { forceByDistance = value; }
        }

        private Follow follow;

        private float distance = 0f;
        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        void Awake()
        {
        }

        // Use this for initialization
        void Start()
        {
            follow = this.GetComponent<Follow>();
        }

        // Update is called once per frame
        void Update()
        {
            switch (follow._targetType)
            {
                case Follow.TargetType.Transform:
                    if (follow.TargetTransform != null)
                        distance = Vector3.Distance(this.transform.position, follow.TargetTransform.position);
                    break;
                case Follow.TargetType.Vector:
                    distance = Vector3.Distance(this.transform.position, follow.TargetVector);
                    break;
                case Follow.TargetType.MainCamera:
                    distance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
                    break;
            }

            if (ForceByDistance._animationCurve.length > 0)
            {
                _cgf.ForcePower = ForceByDistance._animationCurve.Evaluate(distance);
            }
        }
    }
}