using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZero : MonoBehaviour
{

	public Transform m_Player, m_Port;
	public BossBullet[] m_bullet;
	private int bulletCount = 0;

	public float _TurnRateEase = 5f, fireTimeGap = 2f;
	// Use this for initialization
	void Start ()
	{
		StartCoroutine(FireForceBullet());
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{

		var playerPosition = m_Player.position;

		var offset = playerPosition - transform.position;
		//var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
		Quaternion rotation=Quaternion.LookRotation(offset,transform.up) ;
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _TurnRateEase*Time.fixedDeltaTime);
		//transform.LookAt(m_Player);
	}

	IEnumerator FireForceBullet()
	{
		while (true)
		{
			bulletCount++;
			BossBullet bullet=Instantiate(m_bullet[bulletCount%m_bullet.Length], m_Port.position,m_Player.rotation) as BossBullet;
			bullet.transform.localScale=Vector3.one*3;
			bullet.Init(m_Player,transform);
			//bullet.
			yield return new WaitForSeconds(fireTimeGap);
		}
	}
}
