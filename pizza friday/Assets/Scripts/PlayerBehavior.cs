using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] GameObject deadFloor;
    [SerializeField] GameObject startPosition;
    private Collider2D floor;

    private void Start()
    {
        floor = deadFloor.GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == floor)
        {
            TeleportToStart();
        }
    }

    public void TeleportToStart()
    {
        transform.position = startPosition.transform.position;
    }
}
