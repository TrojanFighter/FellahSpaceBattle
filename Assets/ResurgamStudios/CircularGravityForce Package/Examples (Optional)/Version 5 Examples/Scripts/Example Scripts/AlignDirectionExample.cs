/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for showing CGF align to force example.
*******************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CircularGravityForce
{
    public class AlignDirectionExample : MonoBehaviour
    {
        public CGF_AlignToForce cgf_AlignToForce;

        //Aligns to down
        public void SetCGFAlignmentDown()
        {
            cgf_AlignToForce._alignDirection = CGF_AlignToForce.AlignDirection.Down;
        }

        //Aligns to up
        public void SetCGFAlignmentUp()
        {
            cgf_AlignToForce._alignDirection = CGF_AlignToForce.AlignDirection.Up;
        }

        //Aligns to left
        public void SetCGFAlignmentLeft()
        {
            cgf_AlignToForce._alignDirection = CGF_AlignToForce.AlignDirection.Left;
        }

        //Aligns to right
        public void SetCGFAlignmentRight()
        {
            cgf_AlignToForce._alignDirection = CGF_AlignToForce.AlignDirection.Right;
        }
    }
}