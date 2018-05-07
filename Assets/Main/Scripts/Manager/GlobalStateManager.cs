using System.Collections;
using System.Collections.Generic;
using InAudioLeanTween;
using UnityEngine;

namespace FellahSpaceBattle
{
	public class GlobalStateManager : MonoBehaviourEX<GlobalStateManager>
	{

		public GlobalGameState m_gameState;

		public GameMusicState m_MusicState;
		public bool armedState = true;

		public PlayerDrone m_player;

		public BossZero m_Boss;
		
		public Transform bulletRoot,npcRoot,ForceField1;

		public Transform m_Sun,m_Nova,m_Meadow;

		public void SwitchMusicState(GameMusicState musicState)
		{
			/*if (m_MusicState == musicState)
			{
				return;
			}*/

			if (m_CurrentMusic == m_Music[(int) musicState])
			{
				return;
			}

			InAudio.Music.Pause(m_CurrentMusic);
			m_CurrentMusic = m_Music[(int)musicState];
			//InAudio.Music.Play(m_CurrentMusic);
			InAudio.Music.PlayWithFadeIn(m_CurrentMusic,0.3f,LeanTweenType.easeInSine);
		}

		public InMusicGroup[] m_Music;
		public InMusicGroup m_CurrentMusic;
		private void OnEnable()
		{
			InAudio.Music.Play(m_CurrentMusic);
		}

		private void OnDisable()
		{
			InAudio.Music.Stop(m_CurrentMusic);
		}

		public void SwitchGlobalGameState(GlobalGameState gameState)
		{
			m_gameState = gameState;
			switch (gameState)
			{
			    case GlobalGameState.Trapped:
				    SwitchMusicState(GameMusicState.Sorrow);
				    break;
				case GlobalGameState.Collapsed:
					SwitchMusicState(GameMusicState.Creation);
					m_Boss.Collapsed();
					bulletRoot.gameObject.SetActive(false);
					npcRoot.gameObject.SetActive(true);
					ForceField1.gameObject.SetActive(false);
					m_player.BRaycastingNova = true;
					break;
				case GlobalGameState.Escaped:
					SwitchMusicState(GameMusicState.Happy);
					m_player.Escaped();
					break;
			}
		}

		public void SwitchArmedState(bool armed)
		{
			if (m_gameState == GlobalGameState.Trapped)
			{
				Debug.Log("Armed: "+armed);
				m_player.SwitchP47Mode(armed);
				if (armed)
				{
					SwitchMusicState(GameMusicState.Stellaris);
				}
				else
				{
					SwitchMusicState(GameMusicState.Sorrow);
				}
			}
		}

		void Update()
		{
			
		}
	}
}