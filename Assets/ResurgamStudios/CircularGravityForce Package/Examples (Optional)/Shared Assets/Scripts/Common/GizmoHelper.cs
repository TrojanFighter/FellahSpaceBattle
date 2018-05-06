/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for drawing gizmo helpers.
*******************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CircularGravityForce
{
    public class GizmoHelper
    {
        public static void DrawUnityEventGizmo(UnityEvent unityEvent, Transform target, Color gizmoColor)
        {
            if (unityEvent == null)
                return;

            Gizmos.color = gizmoColor;
            var countOnEvent = unityEvent.GetPersistentEventCount();
            for (int i = 0; i < countOnEvent; i++)
            {
                var mb = unityEvent.GetPersistentTarget(i) as MonoBehaviour;
                if (mb != null)
                {
                    Gizmos.DrawWireSphere(target.position, .05f);
                    Gizmos.DrawLine(target.position, mb.gameObject.transform.position);
                    Gizmos.DrawWireSphere(mb.gameObject.transform.position, .1f);
                }
                var go = unityEvent.GetPersistentTarget(i) as GameObject;
                if (go != null)
                {
                    Gizmos.DrawWireSphere(target.position, .05f);
                    Gizmos.DrawLine(target.position, go.gameObject.transform.position);
                    Gizmos.DrawWireSphere(go.gameObject.transform.position, .1f);
                }
            }
        }
    }
}