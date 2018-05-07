using System.Collections;
using System.Collections.Generic;
using FellahSpaceBattle;
using UnityEditor;
using UnityEngine;

public class PowerUpSphere : MonoBehaviour
{

	public bool bEnabled = true, bInvertEffect = true;

	public float lastInvertTime, InvertGapTime = 3f;

	public LayerMask affectedLayer;

	public MeshRenderer _renderer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!_renderer.enabled && Time.time - lastInvertTime > InvertGapTime)
		{
			SwitchOn(true);
		}
	}

	void SwitchOn(bool switchOn)
	{
		_renderer.enabled = switchOn;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!_renderer.enabled)
		{
			return;
		}

		//if ((other.gameObject.layer & affectedLayer) > 0)
		{
			if (other.gameObject.GetComponent<PlayerDrone>())
			{
				if (Time.time - lastInvertTime > InvertGapTime)
				{
					//other.gameObject.GetComponent<PlayerDrone>().SwitchP47Mode(true);
					GlobalStateManager.Instance.SwitchArmedState(true);
					if (bInvertEffect)
					{
						other.transform.forward = -other.transform.forward;
					}
					lastInvertTime = Time.time;
					SwitchOn(false);
				}
			}
		}
	}
}
