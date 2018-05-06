/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used turning Rocket Explosion prefab.
*******************************************************************************************/
using UnityEngine;
using System.Collections;

namespace CircularGravityForce
{
    public class RocketExplosion : MonoBehaviour
    {
        private CGF cgf;
        private Timer timer;

        // Use this for initialization
        void Start()
        {
            cgf = this.GetComponent<CGF>();
            timer = this.GetComponent<Timer>();
        }

        // Update is called once per frame
        void Update()
        {
            cgf.Enable = !timer.Alarm;

            if(SceneSettings.Instance != null)
            {
                cgf._drawGravityProperties.DrawGravityForce = SceneSettings.Instance.ToggleCGF;
            }
        }
    }
}
