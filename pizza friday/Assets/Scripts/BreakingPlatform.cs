using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : Platform
{
    [SerializeField] private float breakingTime, restoringTime;
    private float timer;
    private Renderer rend;
    private GameObject player;
    private Collider2D coll;
    void Start()
    {
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider2D>();
    }

    public void Break()
    {
        rend.enabled = false;
        coll.enabled = false;
        Invoke("Restore", restoringTime);
    }

    public void Restore()
    {
        rend.enabled = true;
        coll.enabled = true;
    }

    public void OnLanding()
    {
        Invoke("Break", breakingTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerBehavior p;
        if (collision.gameObject.TryGetComponent<PlayerBehavior>(out p))
        {
            OnLanding();
        }
    }

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerBehavior p;
        if (collision.gameObject.TryGetComponent<PlayerBehavior>(out p))
        {
            Break();
        }
    }*/
}
