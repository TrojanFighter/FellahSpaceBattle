/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for showing CGF event example.
*******************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CircularGravityForce
{
    public class EventExample : MonoBehaviour
    {
        public Text textField;

        private int eventCounter = 0;
        private Rigidbody rigid;

        //Awake is called when the script instance is being loaded
        void Awake()
        {
            CGF.OnApplyCGFEvent += CGF_OnApplyCGFEvent;
        }

        //This function is called when the MonoBehaviour will be destroyed
        void OnDestroy()
        {
            CGF.OnApplyCGFEvent -= CGF_OnApplyCGFEvent;
        }

        //Use this for initialization
        void Start()
        {
            rigid = this.GetComponent<Rigidbody>();
        }

        private void CGF_OnApplyCGFEvent(CGF cgf, Rigidbody rigid, Collider coll)
        {
            if (this.rigid != rigid)
                return;

            eventCounter++;

            textField.text = string.Format("Event: {0}", eventCounter);
        }
    }
}