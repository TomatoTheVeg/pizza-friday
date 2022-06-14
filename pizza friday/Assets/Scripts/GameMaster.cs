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
        audioManager.PlaySound("level loop");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Win");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
