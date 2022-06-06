using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    Rigidbody2D rb;
    Joystick joy;
    [SerializeField] public float pushStrength;
    private Vector2 prevJoystickPosition = Vector2.zero;
    private bool onGround = false;
    private Platform standingPlatform;
    [SerializeField] Animator anim;
   // [SerializeField] AudioClip jumpSound;
   // [SerializeField] AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joy = GameObject.FindGameObjectWithTag("GameController").GetComponent<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(prevJoystickPosition.x + " " + prevJoystickPosition.y);
        if (prevJoystickPosition != Vector2.zero && joy.Horizontal == 0f && joy.Vertical == 0f && onGround&&standingPlatform.isJumpable)
        {
            Push(prevJoystickPosition);
            BreakingPlatform p;
            if (standingPlatform != null&& standingPlatform.TryGetComponent<BreakingPlatform>(out p))
            {
                p.Break();
            }
        }
        prevJoystickPosition.x = joy.Horizontal;
        prevJoystickPosition.y = joy.Vertical;
        // Debug.Log(joy.Horizontal + " " + joy.Vertical);
    }

    public void Push(Vector2 direction)
    {
        rb.velocity = rb.velocity+direction * pushStrength;
       // src.clip = jumpSound;
        //src.Play();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Platform p;
        if (collision.gameObject.TryGetComponent<Platform>(out p)) { 
            onGround = true;
            anim.SetBool("OnGround", true);
            standingPlatform = collision.gameObject.GetComponent<Platform>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
        standingPlatform = null;
        anim.SetBool("OnGround", false);
    }

    public void StrengthChange(float str)
    {
        pushStrength = str;
    }
}
