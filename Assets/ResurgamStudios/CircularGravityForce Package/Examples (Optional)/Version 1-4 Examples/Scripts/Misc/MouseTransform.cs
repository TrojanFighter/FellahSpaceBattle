/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Moves this transform to mouse hit location.
*******************************************************************************************/
using UnityEngine;
using System.Collections;

namespace CircularGravityForce
{
    public class MouseTransform : MonoBehaviour
    {
        [SerializeField]
        private LayerMask layerMask;
        public LayerMask _layerMask
        {
            get { return layerMask; }
            set { layerMask = value; }
        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, _layerMask))
            {
                this.transform.position = hit.point;
            }
        }
    }
}