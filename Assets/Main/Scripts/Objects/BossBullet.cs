using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
	private bool inited = false;
	public Transform m_player,m_boss;

	public Vector3 DirectionToMove;

	public float movingSpeed,applyForce=2000f;
	// Use this for initialization
	void Start () {
		
	}

	public void Init(Transform targetPlayer,Transform boss)
	{
		if (inited)
		{
			return;
		}
		inited = true;
		m_player = targetPlayer;
		m_boss = boss;

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
			m_player.GetComponent<Rigidbody>().AddForce((m_boss.position-m_player.position).normalized*applyForce,ForceMode.Acceleration);
			m_player.GetComponent<PlayerDrone>().SwitchP47Mode(false);
			SelfDestroy();
		}
	}

	void SelfDestroy()
	{
		Destroy(gameObject);
	}
}
