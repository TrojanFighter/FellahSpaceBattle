/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for Rocket prefab.
*******************************************************************************************/
using UnityEngine;
using System.Collections;

namespace CircularGravityForce
{
    public class Rocket : MonoBehaviour
    {
        public enum RocketState
        {
            None = 0,
            LockOn = 1,
        }

        [SerializeField, Tooltip("The rocket state.")]
        private RocketState rocketState = RocketState.None;
        public RocketState _rocketState
        {
            get { return rocketState; }
            set { rocketState = value; }
        }

        [SerializeField, Tooltip("Explode distance.")]
        private float explodeDistance = 2f;
        public float ExplodeDistance
        {
            get { return explodeDistance; }
            set { explodeDistance = value; }
        }

        [SerializeField, Tooltip("Rocket target transform.")]
        private Transform target;
        public Transform Target
        {
            get { return target; }
            set { target = value; }
        }

        [SerializeField, Tooltip("CGF gameobject that controls the trust."), Header("Required Objects:")]
        private CGF cgfThrust;
        public CGF CgfThrust
        {
            get { return cgfThrust; }
            set { cgfThrust = value; }
        }

        [SerializeField, Tooltip("Rocket explodtion gameobject prefab.")]
        private GameObject rocketExplosion;
        public GameObject RocketExplosion
        {
            get { return rocketExplosion; }
            set { rocketExplosion = value; }
        }

        private LockOnTo lockOnTo;
        private Rigidbody rocketRigidbody;
        private GameObject explosion;
        private Timer timer;

        void Awake()
        {
            lockOnTo = this.gameObject.AddComponent<LockOnTo>();
            rocketRigidbody = this.gameObject.GetComponent<Rigidbody>();
            explosion = Instantiate(RocketExplosion) as GameObject;
            timer = explosion.GetComponent<Timer>();
        }

        void OnEnable()
        {
            ResetRocket();
        }

        void OnDisable()
        {
            if (explosion != null)
            {
                explosion.transform.position = this.transform.position;
                explosion.gameObject.SetActive(true);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (_rocketState == RocketState.LockOn)
            {
                timer.StartTimer = true;
                this.gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            switch (_rocketState)
            {
                case RocketState.None:
                    break;
                case RocketState.LockOn:
                    if (Target != null)
                    {
                        CgfThrust.ForcePower = 20f;

                        lockOnTo.Target = Target;

                        if (Vector3.Distance(this.transform.position, Target.transform.position) < ExplodeDistance)
                        {
                            timer.StartTimer = true;
                            this.gameObject.SetActive(false);
                        }
                    }
                    break;
            }
        }

        void ResetRocket()
        {
            rocketState = RocketState.None;

            CgfThrust.ForcePower = 0f;

            lockOnTo._alignDirection = CGF_AlignToForce.AlignDirection.Down;
            lockOnTo.RotationRigidbody = true;
            lockOnTo.SlerpSpeed = 10f;

            rocketRigidbody.velocity = Vector3.zero;
            rocketRigidbody.angularVelocity = Vector3.zero;

            explosion.transform.position = Vector3.zero;
            explosion.gameObject.SetActive(false);

            timer.StartTimer = false;
            timer.Alarm = false;
        }

        public void Fire()
        {
            _rocketState = RocketState.LockOn;
        }
    }
}