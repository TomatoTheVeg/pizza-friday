using UnityEngine;

[System.Serializable]
public class Sound
{

	public string name;
	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = 0.7f;
	[Range(0.5f, 1.5f)]
	public float pitch = 1f;

	[Range(0f, 0.5f)]
	public float randomVolume = 0.1f;
	[Range(0f, 0.5f)]
	public float randomPitch = 0.1f;

	public bool loop = false, doNotDestroyOnLoad = false;

	[SerializeField]private AudioSource source;

	public void SetSource(AudioSource _source)
	{
		source = _source;
		source.clip = clip;
		source.loop = loop;
	}

	public void Play()
	{
		
		source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
		source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
		source.Play();
	}

	public void PlayUninterrupted()
    {
        if (!source.isPlaying)
        {
			source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
			source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
			source.Play();
		}
    }

	public void Stop()
	{
		source.Stop();
	}

}
