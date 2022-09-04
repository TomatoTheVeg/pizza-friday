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
    void Awake()
    {
        pt = PlatformType.BreakingPlatform;
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider2D>();
    }

    public void Break()
    {

        rend.enabled = false;
        coll.enabled = false;
        CancelInvoke();
        Invoke("Restore", restoringTime);
        isBroken = true;
    }

    public void Restore()
    {
        rend.enabled = true;
        coll.enabled = true;
        isBroken = false;
        CancelInvoke();
    }

    public void OnLanding()
    {
        Invoke("Break", breakingTime);
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
