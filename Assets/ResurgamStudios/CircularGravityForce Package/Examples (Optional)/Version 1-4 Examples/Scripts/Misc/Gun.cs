/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: FPS gun logic.
*******************************************************************************************/
using UnityEngine;
using System.Collections;

namespace CircularGravityForce
{
    public class Gun : MonoBehaviour
    {
        #region Enumes

        //Gun Types
        public enum GunType
        {
            GravityGunForce,
            LauncherForce,
			GravityGunTorque,
			LauncherTorque,
        }

        #endregion

        #region Properties

		[SerializeField, Tooltip("Gun force power")]
		private float cgfForcePower = 30f;
		public float CgfForcePower
		{
			get { return cgfForcePower; }
			set { cgfForcePower = value; }
		}

        [SerializeField, Tooltip("Gun CGF gameobject.")]
		private GameObject cgfGameObject;
		public GameObject CgfGameObject
        {
			get { return cgfGameObject; }
			set { cgfGameObject = value; }
        }

		[SerializeField, Tooltip("Gun lazer gameobject.")]
		private GameObject gunLazerObject;
		public GameObject GunLazerObject
		{
			get { return gunLazerObject; }
			set { gunLazerObject = value; }
		}

        [SerializeField, Tooltip("Spawner for the positive bullets.")]
        private Spawn spawnPosBullet;
        public Spawn SpawnPosBullet
        {
            get { return spawnPosBullet; }
            set { spawnPosBullet = value; }
        }
        [SerializeField, Tooltip("Spawner for the negative bullets.")]
        private Spawn spawnNegBullet;
        public Spawn SpawnNegBullet
        {
            get { return spawnNegBullet; }
            set { spawnNegBullet = value; }
        }
		[SerializeField, Tooltip("Spawner for the positive bullets with torque.")]
		private Spawn spawnPosBulletTor;
		public Spawn SpawnPosBulletTor
		{
			get { return spawnPosBulletTor; }
			set { spawnPosBulletTor = value; }
		}
		[SerializeField, Tooltip("Spawner for the negitive bullets with torque.")]
		private Spawn spawnNegBulletTor;
		public Spawn SpawnNegBulletTor
		{
			get { return spawnNegBulletTor; }
			set { spawnNegBulletTor = value; }
		}

        //Gun mode
        [SerializeField, Tooltip("Gun type used for the diffrent modes.")]
        private GunType gunType;
        public GunType _gunType
        {
            get { return gunType; }
            set { gunType = value; }
        }

        private Animator animator;
		private CGF_SizeByRaycast sizeByRaycast;
		private GameObject gunLazer;

        #endregion

        #region Unity Functions

        // Use this for initialization
        void Start()
        {
            animator = this.GetComponent<Animator>();
			sizeByRaycast = cgfGameObject.GetComponent<CGF_SizeByRaycast> ();
			gunLazer = Instantiate(GunLazerObject) as GameObject;
        }

        // Update is called once per frame
        void Update()
        {
			SyncGunSelection();
        }

		void LateUpdate()
		{
			gunLazer.transform.position = sizeByRaycast.HitPoint;
		}

        #endregion

        #region Functions

        void SyncGunSelection()
		{
			CGF cgf = cgfGameObject.GetComponent<CGF> ();

			switch (_gunType)
			{
			case GunType.GravityGunForce:
				cgf._forceType = CGF.ForceType.Force;
				cgf._forceMode = ForceMode.Force;
				cgf._shape = CGF.Shape.Raycast;
				sizeByRaycast.enabled = true;
				
				if (Input.GetButton("Fire1"))
				{
					cgf.ForcePower = cgfForcePower;
				}
				else if (Input.GetButton("Fire2"))
				{
					cgf.ForcePower = -cgfForcePower;
				}
				else
				{
					cgf.ForcePower = 0f;
				}
				animator.SetBool("isShooting", cgf.ForcePower != 0f);
				break;
			case GunType.LauncherForce:

				cgf._forceType = CGF.ForceType.Force;
				cgf._forceMode = ForceMode.Impulse;
				cgf._shape = CGF.Shape.Raycast;
				sizeByRaycast.enabled = false;
				sizeByRaycast.HitPoint = Vector3.zero;
				cgf.Size = 1f;
				cgf.ForcePower = 25f;
				
				if (Input.GetButtonDown("Fire1"))
				{
					spawnPosBullet.Spawning();
					spawnPosBullet.LastSpawned.GetComponent<CGF>().ForcePower = cgfForcePower;
                    if (SceneSettings.Instance != null)
                        spawnPosBullet.LastSpawned.GetComponent<CGF>()._drawGravityProperties.DrawGravityForce = SceneSettings.Instance.ToggleCGF;
					animator.SetBool("isShooting", true);
				}
				else if (Input.GetButtonDown("Fire2"))
				{
					spawnNegBullet.Spawning();
					spawnNegBullet.LastSpawned.GetComponent<CGF>().ForcePower = -cgfForcePower;
                    if (SceneSettings.Instance != null)
                        spawnNegBullet.LastSpawned.GetComponent<CGF>()._drawGravityProperties.DrawGravityForce = SceneSettings.Instance.ToggleCGF;
					animator.SetBool("isShooting", true);
				}
				else
				{
					animator.SetBool("isShooting", false);
				}
				break;
			case GunType.GravityGunTorque:
				cgf._forceType = CGF.ForceType.Torque;
				cgf._forceMode = ForceMode.Force;
				cgf._shape = CGF.Shape.Raycast;
				sizeByRaycast.enabled = true;
				
				if (Input.GetButton("Fire1"))
				{
					cgf.ForcePower = cgfForcePower;
				}
				else if (Input.GetButton("Fire2"))
				{
					cgf.ForcePower = -cgfForcePower;
				}
				else
				{
					cgf.ForcePower = 0f;
				}
				animator.SetBool("isShooting", cgf.ForcePower != 0f);
				break;
			case GunType.LauncherTorque:
				cgf._forceType = CGF.ForceType.Force;
				cgf._forceMode = ForceMode.Impulse;
				sizeByRaycast.enabled = false;
				sizeByRaycast.HitPoint = Vector3.zero;
				cgf.Size = 1f;
				cgf.ForcePower = 30f;

				if (Input.GetButtonDown("Fire1"))
				{
					spawnPosBulletTor.Spawning();
					spawnPosBulletTor.LastSpawned.GetComponent<CGF>().ForcePower = cgfForcePower;
                    if (SceneSettings.Instance != null)
                        spawnPosBulletTor.LastSpawned.GetComponent<CGF>()._drawGravityProperties.DrawGravityForce = SceneSettings.Instance.ToggleCGF;
					animator.SetBool("isShooting", true);
				}
				else if (Input.GetButtonDown("Fire2"))
				{
					spawnNegBulletTor.Spawning();
					spawnNegBulletTor.LastSpawned.GetComponent<CGF>().ForcePower = -cgfForcePower;
                    if (SceneSettings.Instance != null)
                        spawnNegBulletTor.LastSpawned.GetComponent<CGF>()._drawGravityProperties.DrawGravityForce = SceneSettings.Instance.ToggleCGF;
					animator.SetBool("isShooting", true);
				}
				else
				{
					animator.SetBool("isShooting", false);
				}

				break;
			}
        }

        #endregion
    }
}
