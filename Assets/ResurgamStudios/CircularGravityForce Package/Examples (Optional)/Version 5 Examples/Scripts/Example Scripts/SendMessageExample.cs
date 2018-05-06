/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for showing CGF sendmessage example.
*******************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CircularGravityForce
{
    public class SendMessageExample : MonoBehaviour
    {
        public CGF compareCGF;

        public Text textField;

        private int sendmessageCounter = 0;

        //Registered event for send message
        private void OnApplyCGF(CGF cgf)
        {
            if (compareCGF != cgf)
                return;

            sendmessageCounter++;

            textField.text = string.Format("SendMessage: {0}", sendmessageCounter);
        }
    }
}