using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	[SerializeField]
	Sound[] sounds;
	[SerializeField] float volume;
	public Sound currentMusic;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		for (int i = 0; i < sounds.Length; i++)
		{
			GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
			_go.transform.SetParent(this.transform);
			sounds[i].SetSource(_go.AddComponent<AudioSource>());
			sounds[i].SetVolume(volume);
			if (sounds[i].withLowPassFilter)
			{
				sounds[i].SetLowPassFilter(_go.AddComponent<AudioLowPassFilter>());
			}

		}
		currentMusic = FindSound("silence");
	}

	public void PlaySound(Sound sound)
	{
		sound.Play();
	}

	public void SwitchMusic(Sound sound)
    {
		sound.Play();
		if (sound.isMusic)
		{

			currentMusic.Stop();
			currentMusic = sound;
			return;
		}
	}

	public void PlayUninterruptedSound(Sound sound)
	{
		sound.PlayUninterrupted();
		return;
	}
	public void StopSound(Sound sound)
	{
		sound.Stop();
        if (sound == currentMusic)
        {
			currentMusic = null;
        }
		return;
	}

	public Sound FindSound(string _name)
    {
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sounds[i].name == _name)
			{
				return sounds[i];
			}

			// no sound with _name
		}
		Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
		return null;
	}

	public void ChangeMusicWithAscending(Sound newSound, float transitionTime)
    {
		if (currentMusic != null)
		{
			currentMusic.coroutine = StartCoroutine(currentMusic.Fade(transitionTime));
		}
		PlaySound(newSound);
		newSound.coroutine = StartCoroutine(newSound.Ascend(transitionTime, newSound.defaultVolume, 22000));
		currentMusic = newSound;
	}

	public void ChangeMusic(Sound newSound, float transitionTime)
	{
		if (currentMusic != null)
		{
			currentMusic.coroutine = StartCoroutine(currentMusic.Fade(transitionTime));
		}
		PlaySound(newSound);
		currentMusic = newSound;
	}

	public void StopAllSounds()
    {
		foreach(Sound sound in sounds)
        {
			sound.Stop();
        }
    }

	public void StopAllMusic()
    {
		foreach (Sound sound in sounds)
		{
			if (sound.isMusic)
			{
				sound.Stop();
				StopAllCoroutines();
			}
		}
	}

	public void ChangeAllSoundsVolume(float newVolume)
    {
		foreach (Sound sound in sounds)
		{
			sound.SetVolume(newVolume);
		}
	}

	public void ChangeSoundEffectVolume(float newVolume)
	{
		foreach (Sound sound in sounds)
		{
			if (!sound.isMusic)
			{
				sound.SetVolume(newVolume);
			}
		}
	}

	public void ChangeMusicVolume(float newVolume)
	{
		foreach (Sound sound in sounds)
		{
			if (sound.isMusic)
			{
				sound.SetVolume(newVolume);
			}
		}
	}

	public void ChangeAllSoundsCutOffFrequency(float newFrequency)
	{
		foreach (Sound sound in sounds)
		{
			sound.SetCutOffFrequency(newFrequency);
		}
	}

	public IEnumerator MusicListPlayer(List<Sound> soundsList)
    {
		foreach(Sound sound in soundsList)
        {
			sound.Play();
			yield return new WaitForSeconds(sound.clip.length);
        }
    }
}
