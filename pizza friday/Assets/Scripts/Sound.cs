using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Sound
{

	public string name;
	public AudioClip clip;

	[Range(0f, 1f)]
	public float defaultVolume = 1f;
	[Range(0.5f, 1.5f)]
	public float defaultPitch = 1f;

	[Range(0f, 0.5f)]
	public float randomVolume = 0f;
	[Range(0f, 0.5f)]
	public float randomPitch = 0f;

	public bool loop = false, withLowPassFilter= false;

	[SerializeField]private AudioSource source;
	[SerializeField]private AudioLowPassFilter lowPassFilter;

	[HideInInspector]public Coroutine coroutine;

	public void SetLowPassFilter(AudioLowPassFilter filter)
    {
		lowPassFilter = filter;
		lowPassFilter.cutoffFrequency = 22000;
    }

	public void SetSource(AudioSource _source)
	{
		source = _source;
		source.clip = clip;
		source.loop = loop;
		source.volume = defaultVolume;
		source.pitch = defaultPitch;
	}

	public void Play()
	{
		
		source.volume = defaultVolume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
		source.pitch = defaultPitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
		source.Play();
	}

	public void PlayUninterrupted()
    {
        if (!source.isPlaying)
        {
			source.volume = defaultVolume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
			source.pitch = defaultPitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
			source.Play();
		}
    }

	public void Stop()
	{
		source.Stop();
	}

	public void SetToDefault()
    {
		source.volume = defaultVolume;
		source.pitch = defaultPitch;
    }

	public IEnumerator Fade(float fadeTime)
    {

		float fadingVolumeSpeed = source.volume / fadeTime;
		float fadingFilterSpeed = lowPassFilter.cutoffFrequency / fadeTime;
		while (source.volume > 0)
        {
			source.volume -= fadingVolumeSpeed * Time.deltaTime;
			lowPassFilter.cutoffFrequency -= fadingFilterSpeed * Time.deltaTime;
			yield return null;
        }
    }

	public IEnumerator Ascend(float ascendingTime, float volume, float cutOffFreq)
    {
		float fadingVolumeSpeed = volume / ascendingTime;
		float fadingFilterSpeed = cutOffFreq / ascendingTime;
		while (source.volume<volume)
		{
			source.volume += fadingVolumeSpeed * Time.deltaTime;
			Mathf.Clamp(source.volume, 0, volume);
			lowPassFilter.cutoffFrequency += fadingFilterSpeed * Time.deltaTime;
			Mathf.Clamp(lowPassFilter.cutoffFrequency, 0, 22000);
			yield return null;
		}
	}
}
