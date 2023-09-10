using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] GameObject startPosition;
    [SerializeField] float maxSpeed, deathSpeed;
    Rigidbody2D rb;
    PizzaTemperature pizza;
    private Collider2D floor;
    private Vector2 speed;
    private float gravitySc, rightSpriteScale;
    CamBehaviour camera;
    Animator anim;
    Transform sprite;
    ContactPoint2D[] contactpoint = new ContactPoint2D[20];
    public bool canJump =false;

    private void Start()
    {
        GameMaster.player = this.gameObject;
        pizza = GetComponent<PizzaTemperature>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        gravitySc = rb.gravityScale;
        anim = transform.GetComponentInChildren<Animator>();
        sprite = transform.Find("sprite");
        rightSpriteScale = sprite.transform.localScale.x;
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
        if (rb.velocity.x > 0)
        {
            sprite.localScale = new Vector3(rightSpriteScale, sprite.localScale.y, sprite.localScale.z);
        }
        else if (rb.velocity.x < 0)
        {
            sprite.localScale = new Vector3(-rightSpriteScale, sprite.localScale.y, sprite.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.GetContacts(contactpoint);
        for (int i = 0; i < collision.GetContacts(contactpoint); i++)
        {
            if (contactpoint[i].normal == Vector2.up)
            {
                Platform platform;
                if (collision.gameObject.TryGetComponent<Platform>(out platform))
                {
                    
                    anim.SetBool("IsOnGround", true);
                    if (collision.relativeVelocity.magnitude > deathSpeed && platform.canKill)
                    {
                        TeleportToSave(GameMaster.instance.currSavePoint);
                        return;
                    }
                    //Debug.Log(platform.PlatformType);

                    switch (platform.PlatformType)
                    {
                        case PlatformType.BasicPlatform:
                            BasicPlatform p = platform.GetComponent<BasicPlatform>();
                            AudioManager.instance.PlaySound(AudioManager.instance.FindSound("fall"));
                            break;
                        case PlatformType.BouncePlatform:
                            BouncePlatform bp = platform.GetComponent<BouncePlatform>();
                            rb.velocity = bp.bounceBoost * (speed + 2 * VectorProjection(speed, collision.collider.gameObject.transform.rotation));
                            //Debug.Log("Bounce " + 2 * VectorProjection(speed, collision.collider.gameObject.transform.rotation));
                            break;
                        case PlatformType.BreakingPlatform:
                            BreakingPlatform bkp = platform.GetComponent<BreakingPlatform>();
                            bkp.OnLanding();
                            break;/*
                        case PlatformType.StickyWall:
                            StickyWall sw = platform.GetComponent<StickyWall>();
                            rb.gravityScale = gravitySc * (1 - sw.stickiness);
                            Debug.Log("Stickcheck");
                            rb.velocity = Vector2.zero;
                            break;*/
                        case PlatformType.DeadlyPlatform:
                            DeadlyPlatform dp = platform.GetComponent<DeadlyPlatform>();
                            TeleportToSave(GameMaster.instance.currSavePoint);
                            break;
                        case PlatformType.WinningPlatform:
                            GameMaster.instance.LoadScene();
                            AudioManager.instance.ChangeMusicWithAscending(AudioManager.instance.FindSound("menu master"), 2.5f);
                            break;
                    }
                    /*
                    BasicPlatform p;
                    BouncePlatform bp;
                    StickyWall sw;
                    DeadlyPlatform dp;
                    if (collision.gameObject.TryGetComponent<BasicPlatform>(out p))
                    {
                        //src.clip = landingSound;
                        //src.Play();
                    }
                    else if (collision.gameObject.TryGetComponent<BouncePlatform>(out bp))
                    {
                        rb.velocity = bp.bounceBoost * (speed + 2 * VectorProjection(speed, collision.collider.gameObject.transform.rotation));
                        Debug.Log("Bounce " + 2 * VectorProjection(speed, collision.collider.gameObject.transform.rotation));
                    }
                    else if (collision.gameObject.TryGetComponent<StickyWall>(out sw))
                    {
                        rb.gravityScale = gravitySc * (1 - sw.stickiness);
                        rb.velocity = Vector2.zero;
                    }
                    else if (collision.gameObject.TryGetComponent<DeadlyPlatform>(out dp))
                    {
                        TeleportToStart();
                    }
                    */
                }
            }
            else if (contactpoint[i].normal == Vector2.right)
            {
                StickyWall sw;
                if(collision.gameObject.TryGetComponent<StickyWall>(out sw))
                {
                    rb.gravityScale = gravitySc * (1 - sw.stickiness);
                    rb.velocity = Vector2.zero;
                }
                GameMaster.instance.joystick.state = JoystickState.Right;
            }
            else if (contactpoint[i].normal == Vector2.left)
            {
                StickyWall sw;
                if (collision.gameObject.TryGetComponent<StickyWall>(out sw))
                {
                    rb.gravityScale = gravitySc * (1 - sw.stickiness);
                    rb.velocity = Vector2.zero;
                }
                GameMaster.instance.joystick.state = JoystickState.Left;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Platform p;
        if(collision.gameObject.TryGetComponent<Platform>(out p))
        {
            if (collision.relativeVelocity.y == 0 && collision.GetContact(0).normal == Vector2.up)
            {
                if (p.isJumpable)
                {
                    canJump = true;
                }
                rb.velocity = rb.velocity / p.Roughness;
                pizza.currPizzaTemperature += p.temperatureChange * Time.deltaTime;
            }
            if (p.PlatformType == PlatformType.StickyWall)
            {
                canJump = true;
                //rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -500, 0));
            }
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
            GameMaster.instance.joystick.state = JoystickState.Up;

        }
        canJump = false;
        anim.SetBool("IsOnGround", false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        CameraProperiesChangeTrigger trig;
        if (other.gameObject.TryGetComponent<CameraProperiesChangeTrigger>(out trig))
        {
            camera.ChangeCameraProperies( trig.vertialUnmovableField, trig.horizontalUnmovableField);
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
        rb.velocity = Vector2.zero;
        AudioManager.instance.PlaySound(AudioManager.instance.FindSound("gameover"));
        //deathSrc.clip = fatalitySound;
       // musicSrc.Pause();
       // deathSrc.Play();
    }

    public void TeleportToSave(SavePoint save)
    {
        transform.position = save.transform.position;
        rb.velocity = Vector2.zero;
        AudioManager.instance.PlaySound(AudioManager.instance.FindSound("gameover"));
    }
}
