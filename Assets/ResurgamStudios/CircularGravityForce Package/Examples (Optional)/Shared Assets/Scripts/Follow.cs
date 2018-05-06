/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for making the current Transform move, look or scale to a target.
*******************************************************************************************/
using System;
using System.Collections;
using UnityEngine;

namespace CircularGravityForce
{
    public class Follow : MonoBehaviour
    {
        [Serializable]
        public class VectorToggle
        {
            [SerializeField]
            private bool x = false;
            public bool X
            {
                get { return x; }
                set { x = value; }
            }

            [SerializeField]
            private bool y = false;
            public bool Y
            {
                get { return y; }
                set { y = value; }
            }

            [SerializeField]
            private bool z = false;
            public bool Z
            {
                get { return z; }
                set { z = value; }
            }
        }

        //Movement translation type
        public enum TranslateType
        {
            Move,
            Rotate,
            Both,
        }

        //Track target type
        public enum TargetType
        {
            Transform,
            Vector,
            MainCamera,
        }

        //Update type
        public enum UpdateType
        {
            Transform,
            Rigidbody,
        }

        //When to update the follow
        public enum UpdateOn
        {
            Update,
            FixedUpdate,
        }

        [SerializeField, Tooltip("Enable/Disables the follow.")]
        private bool enable = true;
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        [SerializeField, Tooltip("Enables lerp/slerp.")]
        private bool interpolation = true;
        public bool Interpolation
        {
            get { return interpolation; }
            set { interpolation = value; }
        }

        [SerializeField, Tooltip("The movement translation type.")]
        private TranslateType translateType = TranslateType.Move;
        public TranslateType _translateType
        {
            get { return translateType; }
            set { translateType = value; }
        }

        [SerializeField, Tooltip("The way to track the target.")]
        private TargetType targetType = TargetType.Transform;
        public TargetType _targetType
        {
            get { return targetType; }
            set { targetType = value; }
        }

        [SerializeField, Tooltip("Targeted transform.")]
        private Transform targetTransform;
        public Transform TargetTransform
        {
            get { return targetTransform; }
            set { targetTransform = value; }
        }

        [SerializeField, Tooltip("Targeted vector.")]
        private Vector3 targetVector;
        public Vector3 TargetVector
        {
            get { return targetVector; }
            set { targetVector = value; }
        }

        [SerializeField, Tooltip("The way it updates the objects location.")]
        private UpdateType updateType = UpdateType.Transform;
        public UpdateType _updateType
        {
            get { return updateType; }
            set { updateType = value; }
        }

        [SerializeField, Tooltip("If enabled sets the velocity to zero.")]
        private bool disableVelocity = false;
        public bool DisableVelocity
        {
            get { return disableVelocity; }
            set { disableVelocity = value; }
        }

        [SerializeField, Tooltip("If enabled sets the angular velocity to zero.")]
        private bool disableAngularVelocity = false;
        public bool DisableAngularVelocity
        {
            get { return disableAngularVelocity; }
            set { disableAngularVelocity = value; }
        }

        [SerializeField, Tooltip("The way it updates the objects location.")]
        private UpdateOn updateOn = UpdateOn.Update;
        public UpdateOn _updateOn
        {
            get { return updateOn; }
            set { updateOn = value; }
        }

        [SerializeField, Tooltip("Sets a margin distace for the object.")]
        private float marginDistance = 0f;
        public float MarginDistance
        {
            get { return marginDistance; }
            set { marginDistance = value; }
        }

        [SerializeField, Tooltip("Use the local move offset of the object.")]
        private bool useLocalOffset = false;
        public bool UseLocalOffset
        {
            get { return useLocalOffset; }
            set { useLocalOffset = value; }
        }

        [SerializeField, Tooltip("Position offset value.")]
        private Vector3 positionOffset = Vector3.zero;
        public Vector3 PositionOffset
        {
            get { return positionOffset; }
            set { positionOffset = value; }
        }

        [SerializeField, Tooltip("Uses this.transform.up vector alignment.")]
        private bool upVectorAlignment = false;
        public bool UpVectorAlignment
        {
            get { return upVectorAlignment; }
            set { upVectorAlignment = value; }
        }

        [SerializeField, Tooltip("Uses the position offset when rotating.")]
        private bool usePositionOffsetWithRotate = true;
        public bool UsePositionOffsetWithRotate
        {
            get { return usePositionOffsetWithRotate; }
            set { usePositionOffsetWithRotate = value; }
        }

        [SerializeField, Tooltip("Sets a rotation offset.")]
        private Vector3 rotateOffset = Vector3.zero;
        public Vector3 RotateOffset
        {
            get { return rotateOffset; }
            set { rotateOffset = value; }
        }

        [SerializeField, Tooltip("Locks the vector axis when moving.")]
        private VectorToggle lockPosition;
        public VectorToggle _lockPosition
        {
            get { return lockPosition; }
            set { lockPosition = value; }
        }

        [SerializeField, Tooltip("Locks the Rotation axis when moving.")]
        private VectorToggle lockRotation;
        public VectorToggle _lockRotation
        {
            get { return lockRotation; }
            set { lockRotation = value; }
        }

        [SerializeField, Tooltip("Scales the object based on the target distance.")]
        private bool scaleByDistance = false;
        public bool ScaleByDistance
        {
            get { return scaleByDistance; }
            set { scaleByDistance = value; }
        }

        [SerializeField, Tooltip("Scale factor when using scaling.")]
        private float scaleFactor = 1f;
        public float ScaleFactor
        {
            get { return scaleFactor; }
            set { scaleFactor = value; }
        }

        [SerializeField, Tooltip("Movement speed.")]
        private float moveSpeed = 8f;
        public float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }

        [SerializeField, Tooltip("Rotation speed.")]
        private float rotateSpeed = 8f;
        public float RotateSpeed
        {
            get { return rotateSpeed; }
            set { rotateSpeed = value; }
        }

        private Rigidbody rigid;

        public Follow()
        {
            this.lockPosition = new VectorToggle();
            this.lockRotation = new VectorToggle();
        }

        void Start()
        {
            if (this.GetComponent<Rigidbody>() != null)
            {
                SetRigidbody(this.GetComponent<Rigidbody>());
            }
        }

        public Rigidbody GetRigidbody()
        {
            return this.rigid;
        }

        public void SetRigidbody(Rigidbody rigid)
        {
            this.rigid = rigid;
        }

        void OnDrawGizmosSelected()
        {
            if (Enable)
            {
                bool drawFlag = false;
                switch (_targetType)
                {
                    case TargetType.Transform:

                        if (TargetTransform != null)
                            drawFlag = true;
                        break;
                    case TargetType.Vector:
                        drawFlag = true;
                        break;
                    case TargetType.MainCamera:
                        drawFlag = true;
                        break;
                }

                switch (_translateType)
                {
                    case TranslateType.Move:
                        Gizmos.color = Color.cyan;
                        break;
                    case TranslateType.Rotate:
                        Gizmos.color = Color.red;
                        break;
                    case TranslateType.Both:
                        Gizmos.color = Color.cyan;
                        break;
                }

                if (drawFlag)
                {
                    if (_translateType == TranslateType.Move || _translateType == TranslateType.Both)
                    {
                        Gizmos.DrawLine(this.transform.position, CalculateMargin(this.transform.position, GetTargetVector(), MarginDistance));
                        Gizmos.DrawWireSphere(CalculateMargin(this.transform.position, GetTargetVector(), MarginDistance), .035f);
                        Gizmos.color = Color.white;
                        Gizmos.DrawLine(CalculateMargin(this.transform.position, GetTargetVector(), MarginDistance), GetTargetVector());

                        if (_translateType == TranslateType.Both && !UsePositionOffsetWithRotate)
                        {
                            Gizmos.color = Color.red;
                            Gizmos.DrawLine(this.transform.position, GetTargetVector(false));
                            Gizmos.DrawWireSphere(GetTargetVector(false), .035f);
                        }
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(this.transform.position, GetTargetVector(UsePositionOffsetWithRotate));
                        Gizmos.DrawWireSphere(GetTargetVector(), .035f);
                    }
                }
            }
        }

        void Update()
        {
            switch (_updateOn)
            {
                case UpdateOn.Update:
                    FollowAlongUpdate();
                    break;
            }
        }

        void FixedUpdate()
        {
            switch (_updateOn)
            {
                case UpdateOn.FixedUpdate:
                    FollowAlongUpdate();
                    break;
            }
        }

        void FollowAlongUpdate()
        {
            if (Enable)
            {
                switch (_translateType)
                {
                    case TranslateType.Move:
                        MoveObject();
                        break;
                    case TranslateType.Rotate:
                        RotateObject();
                        break;
                    case TranslateType.Both:
                        MoveObject();
                        RotateObject();
                        break;
                }

                if (ScaleByDistance)
                {
                    float distance = (transform.position - GetTargetVector()).magnitude;
                    this.transform.localScale = Vector3.one * ScaleFactor * distance;
                }

                if (DisableVelocity)
                {
                    rigid.velocity = Vector3.zero;
                }
                if (DisableAngularVelocity)
                {
                    rigid.angularVelocity = Vector3.zero;
                }
            }
        }

        private void MoveObject()
        {
            Vector3 startPoint = this.transform.position;
            Vector3 targetPoint = CalculateMargin(startPoint, GetTargetVector(), MarginDistance);

            if (_lockPosition.X)
                targetPoint.x = transform.position.x;

            if (_lockPosition.Y)
                targetPoint.y = transform.position.y;

            if (_lockPosition.Z)
                targetPoint.z = transform.position.z;

            Vector3 moveTo = Vector3.zero;
            if (Interpolation)
                moveTo = Vector3.Lerp(startPoint, targetPoint, Time.deltaTime * MoveSpeed);
            else
                moveTo = targetPoint;

            switch (_updateType)
            {
                case UpdateType.Transform:
                    transform.position = moveTo;
                    break;
                case UpdateType.Rigidbody:
                    if (rigid != null)
                        rigid.MovePosition(moveTo);
                    else
                        Debug.LogWarning("GameObject needs Rigidbody!");
                    break;
            }
        }

        private void RotateObject()
        {
            Quaternion startRotation = transform.rotation;
            Vector3 targetPoint = GetTargetVector(usePositionOffsetWithRotate);

            var flatVectorToTarget = transform.position - targetPoint;

            if (_lockRotation.X)
            {
                flatVectorToTarget.x = 0;
            }
            if (_lockRotation.Y)
            {
                flatVectorToTarget.y = 0;
            }
            if (_lockRotation.Z)
            {
                flatVectorToTarget.z = 0;
            }

            var lookRotation = Quaternion.identity;

            if (flatVectorToTarget != Vector3.zero)
            {
                if (UpVectorAlignment)
                    lookRotation = Quaternion.LookRotation(flatVectorToTarget, this.transform.up) * Quaternion.Euler(RotateOffset);
                else
                    lookRotation = Quaternion.LookRotation(flatVectorToTarget) * Quaternion.Euler(RotateOffset);
            }

            var newRotation = Quaternion.identity;
            if (Interpolation)
                newRotation = Quaternion.Slerp(startRotation, lookRotation, Time.deltaTime * RotateSpeed);
            else
                newRotation = lookRotation;

            switch (_updateType)
            {
                case UpdateType.Transform:
                    transform.rotation = newRotation;
                    break;
                case UpdateType.Rigidbody:
                    if (rigid != null)
                        rigid.MoveRotation(newRotation);
                    else
                        Debug.LogWarning("GameObject needs Rigidbody!");
                    break;
            }
        }

        public Vector3 GetTargetVector(bool useOffset = true)
        {
            Vector3 targetPoint = Vector3.zero;

            switch (_targetType)
            {
                case TargetType.Transform:

                    if (TargetTransform != null)
                    {
                        if (useOffset)
                        {
                            if (useLocalOffset)
                            {
                                targetPoint = TargetTransform.position + TargetTransform.TransformDirection(PositionOffset);
                            }
                            else
                            {
                                targetPoint = TargetTransform.position + PositionOffset;
                            }
                        }
                        else
                        {
                            targetPoint = TargetTransform.position;
                        }
                    }

                    break;
                case TargetType.Vector:

                    if (useOffset)
                    {
                        targetPoint = new Vector3(TargetVector.x + PositionOffset.x, TargetVector.y + PositionOffset.y, TargetVector.z + PositionOffset.z);
                    }
                    else
                    {
                        targetPoint = TargetVector;
                    }

                    break;
                case TargetType.MainCamera:

                    if (useOffset)
                    {
                        targetPoint = Camera.main.transform.position + Camera.main.transform.TransformDirection(PositionOffset);
                    }
                    else
                    {
                        targetPoint = Camera.main.transform.position;
                    }

                    break;
            }

            return targetPoint;
        }

        public Vector3 CalculateMargin(Vector3 startPoint, Vector3 targetPoint, float distance)
        {
            if (Vector3.Distance(startPoint, targetPoint) > distance)
                return targetPoint + ((startPoint - targetPoint).normalized * distance);
            else
                return startPoint;
        }
    }
}