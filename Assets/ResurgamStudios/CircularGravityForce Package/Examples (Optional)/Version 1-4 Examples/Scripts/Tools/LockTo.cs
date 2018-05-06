/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for locking the current Transform.
*******************************************************************************************/
using UnityEngine;
using System.Collections;

namespace CircularGravityForce
{
	public class LockTo : MonoBehaviour
    {
        #region Properties

        [SerializeField, Tooltip("Locks the current transform.")]
	    private bool lockTransform = true;
	    public bool LockTransform
	    {
	        get { return lockTransform; }
	        set { lockTransform = value; }
	    }

	    private Quaternion initialRotation;

        #endregion

        #region Unity Functions

        // Use this for initialization
		void Start () 
	    {
	        initialRotation = transform.rotation;
		}
		
		// Update is called once per frame
		void Update () 
	    {
	        if (LockTransform)
	        {
	            transform.rotation = initialRotation;
	        }
        }

        #endregion
    }
}