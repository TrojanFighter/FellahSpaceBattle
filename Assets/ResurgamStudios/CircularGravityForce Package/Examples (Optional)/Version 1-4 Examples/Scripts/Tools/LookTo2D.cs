﻿/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for making the current Transform move to a target transform in 2D.
*******************************************************************************************/
using UnityEngine;
using System.Collections;

namespace CircularGravityForce
{
	public class LookTo2D : MonoBehaviour
    {
        #region Properties
        
        [SerializeField, Tooltip("Looks at the current target.")]
	    private Transform target;
	    public Transform Target
	    {
	        get { return target; }
	        set { target = value; }
	    }
		
	    [SerializeField, Tooltip("Rotates the rigidbody.")]
	    private float slerpSpeed = 8f;
	    public float SlerpSpeed
	    {
	        get { return slerpSpeed; }
	        set { slerpSpeed = value; }
	    }

	    [SerializeField, Tooltip("Look to direction.")]
	    private Vector3 direction = Vector3.back;
	    public Vector3 Direction
	    {
	        get { return direction; }
	        set { direction = value; }
	    }

        #endregion

        #region Unity Functions

        // Update is called once per frame
	    void FixedUpdate()
	    {
	        var newRotation = Quaternion.LookRotation(transform.position - Target.position, direction);

	        newRotation.x = 0.0f;
	        newRotation.y = 0.0f;

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * SlerpSpeed);
        }

        #endregion
    }
}