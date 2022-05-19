using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] GameObject deadFloor;
    [SerializeField] GameObject startPosition;
    Rigidbody2D rb;
    private Collider2D floor;
    private Vector2 speed;
    private float gravitySc;

    private void Start()
    {
        floor = deadFloor.GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        gravitySc = rb.gravityScale;
    }
    void Update()
    {
        if(rb.velocity!= Vector2.zero)
        {
            speed = rb.velocity;
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BouncePlatform p;
        if (collision.gameObject.TryGetComponent<BouncePlatform>(out p))
        {
            rb.velocity = speed + 2*VectorProjection(speed, collision.collider.gameObject.transform.rotation);
            Debug.Log("Bounce "+ 2 * VectorProjection(speed, collision.collider.gameObject.transform.rotation));
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        StickyWall w;
        if (collision.gameObject.TryGetComponent<StickyWall>(out w))
        {
            rb.gravityScale = rb.gravityScale * (1 - w.stickiness);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StickyWall w;
        if (collision.gameObject.TryGetComponent<StickyWall>(out w))
        {
            rb.gravityScale = gravitySc;
        }
    }

    private Vector2 VectorProjection(Vector2 speed, Quaternion transform)
    {
        Vector2 norm =new Vector2(Mathf.Sin(transform.eulerAngles.z), Mathf.Cos(transform.eulerAngles.z));
        return norm*Mathf.Abs((norm*speed).x+(norm*speed).y);
    }

    public void TeleportToStart()
    {
        transform.position = startPosition.transform.position;
    }
}
