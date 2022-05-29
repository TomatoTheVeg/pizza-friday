using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehaviour : MonoBehaviour
{
    [SerializeField] GameObject player;
    public float xMargin = 1f;
    public float zMargin = 1f;
    public float xSmooth = 8f;
    public float zSmooth = 8f;
    public Vector3 maxXAndZ;
    public Vector3 minXAndZ;


   // private Transform player;


    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) > xMargin;
    }


    bool CheckZMargin()
    {
        return Mathf.Abs(transform.position.z - player.transform.position.z) > zMargin;
    }


    void FixedUpdate()
    {
        //TrackPlayer();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }


    void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetZ = transform.position.z;
        if (CheckXMargin())
            targetX = Mathf.Lerp(transform.position.x, player.transform.position.x, xSmooth * Time.deltaTime);
        if (CheckZMargin())
            targetZ = Mathf.Lerp(transform.position.z, player.transform.position.z, zSmooth * Time.deltaTime);
        //targetX = Mathf.Clamp(targetX, minXAndZ.x, maxXAndZ.x);
        //targetZ = Mathf.Clamp(targetZ, minXAndZ.z, maxXAndZ.z);
        transform.position = new Vector3(targetX, transform.position.y, targetZ);
    }
}
