using System.Collections;
using System.Collections.Generic;
using FellahSpaceBattle;
using UnityEngine;

public class SuperNovaExit : MonoBehaviour
{
	public bool inited = false;
	public Collider m_Collider;

	public void Init()
	{
		if (inited)
		{
			return;
		}

		inited = true;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.GetComponent<PlayerDrone>())
		{
			other.gameObject.GetComponent<PlayerDrone>().BRaycastingNova = false;
			m_Collider.enabled = false;
			GlobalStateManager.Instance.SwitchGlobalGameState(GlobalGameState.Escaped);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}
}
