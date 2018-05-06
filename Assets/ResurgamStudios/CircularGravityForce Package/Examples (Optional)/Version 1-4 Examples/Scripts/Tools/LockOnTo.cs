/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for making the current Transform lock on to target transform.
*******************************************************************************************/
using UnityEngine;
using System.Collections;

namespace CircularGravityForce
{
    public class LockOnTo : MonoBehaviour
    {
        #region Properties
        
        [SerializeField, Tooltip("Align direction.")]
        private CGF_AlignToForce.AlignDirection alignDirection;
        public CGF_AlignToForce.AlignDirection _alignDirection
        {
            get { return alignDirection; }
            set { alignDirection = value; }
        }

        [SerializeField, Tooltip("Target to align.")]
        private Transform target;
        public Transform Target
        {
            get { return target; }
            set { target = value; }
        }

        [SerializeField, Tooltip("Rotation the rigidbody.")]
        private bool rotationRigidbody = false;
        public bool RotationRigidbody
        {
            get { return rotationRigidbody; }
            set { rotationRigidbody = value; }
        }

        [SerializeField, Tooltip("Speed of the rotation.")]
        private float slerpSpeed = 8f;
        public float SlerpSpeed
        {
            get { return slerpSpeed; }
            set { slerpSpeed = value; }
        }

        private Transform myTransform;
        private Rigidbody myRigidbody;

        #endregion

        #region Unity Functions

        // Use this for initialization
        void Start()
        {
            if (RotationRigidbody)
            {
                myRigidbody = this.GetComponent<Rigidbody>();
                myTransform = myRigidbody.transform;
            }
            else
            {
                myTransform = this.transform;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Target != null)
            {
                Vector3 up = (myTransform.position - Target.position).normalized;
                Vector3 newLocal = Vector3.zero;

                switch (_alignDirection)
                {
                    case CGF_AlignToForce.AlignDirection.Up:
                        newLocal = myTransform.up;
                        break;
                    case CGF_AlignToForce.AlignDirection.Down:
                        newLocal = -myTransform.up;
                        break;
                    case CGF_AlignToForce.AlignDirection.Left:
                        newLocal = -myTransform.right;
                        break;
                    case CGF_AlignToForce.AlignDirection.Right:
                        newLocal = myTransform.right;
                        break;
                    case CGF_AlignToForce.AlignDirection.Forward:
                        newLocal = myTransform.forward;
                        break;
                    case CGF_AlignToForce.AlignDirection.Backward:
                        newLocal = -myTransform.forward;
                        break;
                }

                var targetRotation = Quaternion.FromToRotation(newLocal, up) * myTransform.rotation;
                var newRotation = Quaternion.Slerp(myTransform.rotation, targetRotation, Time.deltaTime * SlerpSpeed);

                if (RotationRigidbody)
                    myRigidbody.MoveRotation(newRotation);
                else
                    myTransform.rotation = (newRotation);

            }
        }

        #endregion
    }
}
