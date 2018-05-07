using System.Collections;
using System.Collections.Generic;
using FellahSpaceBattle;
using UnityEngine;

public class BossZero : MonoBehaviour
{

	public int HP = 100;
	public Transform m_Player, m_Port;
	public BossBullet[] m_bullet;
	private int bulletCount = 0;

	public float _TurnRateEase = 5f, fireTimeGap = 2f;

	private bool bCollapsed = false;
	public float dropSpeed = 10;
	public LODGroup m_LOD;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(FireForceBullet());
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{

		if (!bCollapsed)
		{
			var playerPosition = m_Player.position;

			var offset = playerPosition - transform.position;
			//var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.LookRotation(offset, transform.up);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _TurnRateEase * Time.fixedDeltaTime);
		}
		else
		{
			if ((GlobalStateManager.Instance.m_Sun.position - transform.position).sqrMagnitude > 100f)
			{
				transform.position += (GlobalStateManager.Instance.m_Sun.position - transform.position).normalized * dropSpeed *Time.fixedDeltaTime;
			}
		}
	}

	IEnumerator FireForceBullet()
	{
		while (true)
		{
			if (HP >= 60)
			{
				bulletCount++;
				BossBullet bullet =Instantiate(m_bullet[bulletCount % m_bullet.Length], m_Port.position, m_Player.rotation) as BossBullet;
				bullet.transform.SetParent(GlobalStateManager.Instance.bulletRoot);
				bullet.transform.localScale = Vector3.one * 4;
				bullet.Init(m_Player, transform);
				yield return new WaitForSeconds(fireTimeGap);
			}
			else
			{
				bulletCount++;
				BossBullet bullet =Instantiate(m_bullet[bulletCount % m_bullet.Length], m_Port.position, m_Player.rotation) as BossBullet;
				bullet.transform.SetParent(GlobalStateManager.Instance.bulletRoot);
				bullet.transform.localScale = Vector3.one * 4;
				bullet.Init(m_Player, transform);
				yield return new WaitForSeconds(fireTimeGap*2/3);
			}
			
		}
	}

	public void GetHit(int damage)
	{
		HP -= damage;
		CheckHP();
	}

	void CheckHP()
	{
		if (HP <= 0)
		{
			GlobalStateManager.Instance.SwitchGlobalGameState(GlobalGameState.Collapsed);
		}
	}

	public void Collapsed()
	{
		StopCoroutine(FireForceBullet());
		bCollapsed = true;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		transform.localScale = Vector3.one * 0.6f;
		m_LOD.ForceLOD(0);
		//GetComponent<Rigidbody>().velocity = (GlobalStateManager.Instance.m_Sun.position - transform.position).normalized;
		//GetComponent<Rigidbody>().useGravity = true;
	}
}
