using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBackZone : MonoBehaviour
{

    public LayerMask affectedLayer;//=1<<LayerMask.NameToLayer("Player");
    public Transform m_DragTargetPoint;
    public float m_DragForceStrength=600f;
    public ForceMode m_ForceMode;

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name+" "+other.gameObject.layer);
        if ((affectedLayer& other.gameObject.layer )!=0)
        {
            Vector3 forceDirection = (m_DragTargetPoint.position - other.gameObject.transform.position).normalized;
            other.attachedRigidbody.AddForce(forceDirection*m_DragForceStrength,m_ForceMode);
            Debug.Log("Pulled: "+other.gameObject.name);
        }
    }
}
