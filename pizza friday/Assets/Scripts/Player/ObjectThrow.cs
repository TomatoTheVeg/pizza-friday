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
    [SerializeField]private Animator anim;
    Transform sprite;
    PlayerBehavior player;
    float defaultScale;
   // [SerializeField] AudioClip jumpSound;
   // [SerializeField] AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetComponentInChildren<Animator>();
        joy = GameObject.FindGameObjectWithTag("GameController").GetComponent<Joystick>();
        sprite = transform.Find("sprite");
        defaultScale = sprite.transform.localScale.x;
        player = GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(joy.IsInDeadZone/*"magnitude: "+Mathf.Sqrt(joy.Vertical*joy.Vertical+joy.Horizontal*joy.Horizontal)*/);
        // Debug.Log(prevJoystickPosition.x + " " + prevJoystickPosition.y);
        if (prevJoystickPosition != Vector2.zero && !joy.IsInDeadZone && joy.Vertical == 0f && player.canJump)
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
        if (prevJoystickPosition.x > 0)
        {
            sprite.localScale = new Vector3(defaultScale, sprite.localScale.y, sprite.localScale.z);
        }
        if (prevJoystickPosition.x < 0)
        {
            sprite.localScale = new Vector3(-defaultScale, sprite.localScale.y, sprite.localScale.z);
        }
        // Debug.Log(joy.Horizontal + " " + joy.Vertical);
    }

    public void Push(Vector2 direction)
    {
        rb.velocity = rb.velocity+direction * pushStrength;
        AudioManager.instance.PlaySound(AudioManager.instance.FindSound("jump"));
        anim.SetTrigger("IsJumping");
        // src.clip = jumpSound;
        //src.Play();
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        Platform p;
        if (collision.gameObject.TryGetComponent<Platform>(out p)) { 
            onGround = true;
            //anim.SetTrigger("IsLanding");
            //anim.SetBool("OnGround", true);
            standingPlatform = collision.gameObject.GetComponent<Platform>();
        }
    }*/

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
        standingPlatform = null;
        anim.SetBool("IsOnGround", false);
        //anim.SetBool("OnGround", false);
    }
    */
    public void StrengthChange(float str)
    {
        pushStrength = str;
    }
}
