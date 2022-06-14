using UnityEngine;


public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	[SerializeField]
	Sound[] sounds;
	GameObject undestructableAudioSource; 

	void Awake()
	{
		if (instance != null)
		{
			if (instance != this)
			{
				Destroy(this.gameObject);
			}
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
			if (sounds[i].withLowPassFilter)
			{
				sounds[i].SetLowPassFilter(_go.AddComponent<AudioLowPassFilter>());
			}

		}
	}

	void Start()
	{

	}

	public void PlaySound(string _name)
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sounds[i].name == _name)
			{
				sounds[i].Play();
				return;
			}
		}

		// no sound with _name
		Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
	}

	public void PlayUninterruptedSound(string _name)
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sounds[i].name == _name)
			{
				sounds[i].PlayUninterrupted();
				return;
			}

			// no sound with _name
			
		}
		Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
	}
	public void StopSound(string _name)
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sounds[i].name == _name)
			{
				sounds[i].Stop();
				return;
			}
		}

		// no sound with _name
		Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
	}

	public Sound FindSound(string _name)
    {
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sounds[i].name == _name)
			{
				sounds[i].PlayUninterrupted();
				return sounds[i];
			}

			// no sound with _name
		}
		Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
		return null;
	}

	public void ChangeSoundWithscending(Sound currentSound, Sound newSound, float transitionTime)
    {
		currentSound.coroutine =StartCoroutine(currentSound.Fade(transitionTime));
		newSound.coroutine =StartCoroutine(newSound.Ascend(transitionTime, newSound.defaultVolume, 22000));
    }

	public void ChangeSound(Sound currentSound, Sound newSound, float transitionTime)
	{
		currentSound.coroutine = StartCoroutine(currentSound.Fade(transitionTime));
		newSound.Play();
	}
}
