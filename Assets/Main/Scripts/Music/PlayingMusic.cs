using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingMusic : MonoBehaviour
{
	public InMusicGroup m_Music;
	private void OnEnable()
	{
		InAudio.Music.Play(m_Music);
	}

	private void OnDisable()
	{
		InAudio.Music.Stop(m_Music);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
