using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Joystick joy;
    [SerializeField] public float pushStrength;
    [SerializeField] public Vector2 pushDirection;
    private Vector2 prevJoystickPosition = Vector2.zero;
    private bool onGround = false;
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (prevJoystickPosition != Vector2.zero && joy.Horizontal == 0f && joy.Vertical == 0f && onGround)
        {
            Push(prevJoystickPosition);
        }
        prevJoystickPosition.x = joy.Horizontal;
        prevJoystickPosition.y = joy.Vertical;
        Debug.Log(joy.Horizontal + " " + joy.Vertical);
    }

    public void Push(Vector2 direction)
    {
        rb.velocity = direction * pushStrength;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        onGround = true;
        anim.SetBool("OnGround", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
        anim.SetBool("OnGround", false);
    }

    public void StrengthChange(float str)
    {
        pushStrength = str;
    }
}
