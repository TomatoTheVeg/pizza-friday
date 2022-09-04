using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavePoint : MonoBehaviour
{
    [SerializeField] private int saveNum;
    public bool isSaved = false;
    public int getSavenum { get { return saveNum; } }

    void Start()
    {
        gameObject.name = "Save zone " + saveNum;
        GameMaster.instance.AddSavePoint(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player"&& !isSaved)
        {
            isSaved = true;
            GameMaster.instance.currSavePoint = this;
            //Debug.Log("Check point Num " + saveNum + " saved");
        }
    }
}
