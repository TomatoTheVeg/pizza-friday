using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    private AudioManager audioManager;
    public Joystick joystick;
    public SavePoint currSavePoint = null;
    public List<SavePoint> savePoints = new List<SavePoint>();
    public static GameObject player = null;

    private void Awake()
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
        savePoints = new List<SavePoint>();
        joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
    }
    /*
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
        savePoints = new List<SavePoint>();
    }*/

    public void LoadScene()
    {
        SceneManager.LoadScene("Win");
        currSavePoint = null;
    }

    public void MenuMusic()
    {
        audioManager.ChangeMusicWithAscending(audioManager.FindSound("menu master"), 1.2f);
    }

    public void LevelMusic()
    {
        audioManager.ChangeMusicWithAscending(audioManager.FindSound("level master"), 1.2f);
    }

    public void AddSavePoint(SavePoint save)
    {
        if(savePoints.Count==0)
        {
            savePoints.Add(save);
            return;
        }
        for(int i = 0; i < savePoints.Count; i++)
        {
            if (savePoints[i].getSavenum > save.getSavenum)
            {
                savePoints.Insert(i, save);
                return;
            }
        }
        savePoints.Add(save);
    }

    public void ResetLevel()
    {
        currSavePoint = savePoints[0];
        player.GetComponent<PlayerBehavior>().TeleportToStart();
    }
}

