/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for spawning objects.
*******************************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CircularGravityForce
{
    public class Spawn : MonoBehaviour
    {
        #region Properties

        [SerializeField, Tooltip("Prefab gameobject to spawn.")]
        private GameObject spawnObject;
        public GameObject SpawnObject
        {
            get { return spawnObject; }
            set { spawnObject = value; }
        }

        [SerializeField, Tooltip("Max gameobjects the spawner can use.")]
        private int maxObjects = 25;
        public int MaxObjects
        {
            get { return maxObjects; }
            set { maxObjects = value; }
        }

		[SerializeField, Tooltip("The last spawned prefab."), Header("Spawned Objects:")]
		private GameObject lastSpawned;
		public GameObject LastSpawned
		{
			get { return lastSpawned; }
			set { lastSpawned = value; }
		}
		private List<GameObject> spawnedObjects;
		public List<GameObject> SpawnedObjects
		{
			get { return spawnedObjects; }
			set { spawnedObjects = value; }
		}

        private GameObject spawnObjectParent;
        private int index = 0;

        #endregion

        #region Unity Functions

        //Use this for initialization
        void Awake()
        {
			SetupSpawnPool();
        }

        #endregion

        #region Functions

		public void SetupSpawnPool()
		{
			if(spawnObject != null)
			{
				if(SpawnedObjects != null)
				{
					for (int i = 0; i < SpawnedObjects.Count; i++)
					{
						Destroy(SpawnedObjects[i]);
					}
				}
				SpawnedObjects = new List<GameObject>();
				
				spawnObjectParent = new GameObject(string.Format("Spawn-Pool: {0}", spawnObject.name));
				
				for (int i = 0; i < maxObjects; i++)
				{
					GameObject spawnedObject = Instantiate(SpawnObject) as GameObject;
					
					spawnedObject.transform.SetParent(spawnObjectParent.transform, false);
					
					spawnedObject.SetActive(false);
					
					SpawnedObjects.Add(spawnedObject);
				}
			}
		}

        public void Spawning()
        {
			if (index >= SpawnedObjects.Count)
                index = 0;

			SpawnedObjects[index].SetActive(true);
			SpawnedObjects[index].gameObject.transform.position = this.transform.position;
			SpawnedObjects[index].gameObject.transform.rotation = this.transform.rotation;

			if (SpawnedObjects[index].GetComponent<Rigidbody>() != null)
            {
				SpawnedObjects[index].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
				SpawnedObjects[index].GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
			if (SpawnedObjects[index].GetComponent<Rigidbody2D>() != null)
            {
				SpawnedObjects[index].GetComponent<Rigidbody2D>().angularVelocity = 0f;
				SpawnedObjects[index].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

			LastSpawned = SpawnedObjects [index];

            index++;
        }

        #endregion
    }
}