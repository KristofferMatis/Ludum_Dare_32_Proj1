using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerSoundState
{
	e_Stab,
	e_Slash,
	e_Bash,
	e_Smash,
	e_Run,
	e_Sprint,
	e_PickUp,
	e_RandomLine
}

[System.Serializable]
public class PlayerSound
{
	public PlayerSoundState m_State;

	public List<AudioSource> m_Sources;

	public void PlayState()
	{
		foreach(AudioSource source in m_Sources)
		{
			source.Play();
		}
	}

	public void StopState()
	{
		foreach(AudioSource source in m_Sources)
		{
			source.Stop();
		}
	}
}

public class SoundManager : MonoBehaviour
{
	public List<PlayerSound> m_PlayerSounds;

	public void PlaySoundState(PlayerSoundState state)
	{
		foreach(PlayerSound playerSound in m_PlayerSounds)
		{
			if(playerSound.m_State == state)
			{
				playerSound.PlayState();
			}
		}
	}

	public void StopSoundState(PlayerSoundState state)
	{
		foreach(PlayerSound playerSound in m_PlayerSounds)
		{
			if(playerSound.m_State == state)
			{
				playerSound.StopState();
			}
		}
	}
}
