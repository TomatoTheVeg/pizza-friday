using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] GameObject deadFloor;
    [SerializeField] GameObject startPosition;
    Rigidbody2D rb;
    private Collider2D floor;

    private void Start()
    {
        floor = deadFloor.GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == floor)
        {
            TeleportToStart();
        }
        BouncePlatform bp;
        if (collision.gameObject.TryGetComponent<BouncePlatform>(out bp))
        {
            //rb.velocity
            //collision.gameObject.transform.rotation.eulerAngles.z;
        }
    }

    public void TeleportToStart()
    {
        transform.position = startPosition.transform.position;
    }
}
