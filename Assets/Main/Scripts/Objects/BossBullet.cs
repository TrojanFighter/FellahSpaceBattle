using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
	private bool inited = false;
	public Transform m_player;

	public Vector3 DirectionToMove;

	public float movingSpeed,applyForce=3000f;
	// Use this for initialization
	void Start () {
		
	}

	public void Init(Transform targetPlayer)
	{
		if (inited)
		{
			return;
		}
		inited = true;
		m_player = targetPlayer;

		DirectionToMove = (targetPlayer.position - transform.position).normalized;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (inited)
		{
			transform.position += DirectionToMove * Time.fixedDeltaTime*movingSpeed;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform == m_player)
		{
			m_player.GetComponent<Rigidbody>().AddForce(-DirectionToMove*applyForce,ForceMode.Impulse);
			SelfDestroy();
		}
	}

	void SelfDestroy()
	{
		Destroy(gameObject);
	}
}
