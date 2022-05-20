using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] GameObject deadFloor;
    [SerializeField] GameObject startPosition;
    [SerializeField] float maxSpeed;
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
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity * maxSpeed / rb.velocity.magnitude;
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
        StickyWall w;
        if (collision.gameObject.TryGetComponent<BouncePlatform>(out p))
        {
            rb.velocity = p.bounceBoost*(speed + 2*VectorProjection(speed, collision.collider.gameObject.transform.rotation));
            Debug.Log("Bounce "+ 2 * VectorProjection(speed, collision.collider.gameObject.transform.rotation));
        }
        else if (collision.gameObject.TryGetComponent<StickyWall>(out w))
        {
            rb.gravityScale = gravitySc * (1 - w.stickiness);
            rb.velocity = Vector2.zero;
            Debug.Log(rb.gravityScale);

        }
    }
/*
    private void OnCollision2D(Collision2D collision)
    {
        StickyWall w;
        if (collision.gameObject.TryGetComponent<StickyWall>(out w))
        {
            rb.gravityScale = gravitySc* (1 - w.stickiness);
            rb.velocity = Vector2.up*rb.velocity.x;
            Debug.Log(rb.gravityScale);

        }
    }
*/
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
        Vector2 norm =new Vector2(-Mathf.Sin(transform.eulerAngles.z*3.14f/180), Mathf.Cos(transform.eulerAngles.z*3.14f/180));
        return norm*Mathf.Abs((norm*speed).x+(norm*speed).y);
    }

    public void TeleportToStart()
    {
        transform.position = startPosition.transform.position;
    }
}
