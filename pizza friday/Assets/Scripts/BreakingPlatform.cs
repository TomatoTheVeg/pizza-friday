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
    private bool isBroken = false;
    void Start()
    {
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider2D>();
    }

    public void Break()
    {
        if (!isBroken)
        {
            rend.enabled = false;
            coll.enabled = false;
            Invoke("Restore", restoringTime);
            isBroken = true;
        }
    }

    public void Restore()
    {
        rend.enabled = true;
        coll.enabled = true;
        isBroken = false;
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
