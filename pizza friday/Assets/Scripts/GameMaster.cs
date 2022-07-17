using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    private AudioManager audioManager;
    void Start()
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
        audioManager = AudioManager.instance;
        audioManager.SwitchMusic(audioManager.FindSound("level loop"));
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Win");
    }

    public void MenuMusic()
    {
        audioManager.ChangeMusicWithAscending(audioManager.FindSound("menu master"), 1.2f);
    }

    public void LevelMusic()
    {
        audioManager.ChangeMusicWithAscending(audioManager.FindSound("level master"), 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
