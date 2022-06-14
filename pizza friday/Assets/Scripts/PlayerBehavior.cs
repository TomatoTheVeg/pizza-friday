using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] GameObject startPosition;
    [SerializeField] float maxSpeed, deathSpeed;
    Rigidbody2D rb;
    PizzaTemperature pizza;
    private Collider2D floor;
    private Vector2 speed;
    private float gravitySc;
    CamBehaviour camera;

    private void Start()
    {
        pizza = GetComponent<PizzaTemperature>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamBehaviour>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Platform platform;
        if (collision.gameObject.TryGetComponent<Platform>(out platform))
        {
            if (collision.relativeVelocity.magnitude > deathSpeed && platform.canKill)
            {
                TeleportToStart();
                return;
            }
            Debug.Log(platform.PlatformType);
            
            switch (platform.PlatformType)
            {
                case PlatformType.BasicPlatform:
                    BasicPlatform p = platform.GetComponent<BasicPlatform>();
                    AudioManager.instance.PlaySound("fall");
                    break;
                case PlatformType.BouncePlatform:
                    BouncePlatform bp = platform.GetComponent<BouncePlatform>();
                    rb.velocity = bp.bounceBoost * (speed + 2 * VectorProjection(speed, collision.collider.gameObject.transform.rotation));
                    Debug.Log("Bounce " + 2 * VectorProjection(speed, collision.collider.gameObject.transform.rotation));
                    break;
                case PlatformType.BreakingPlatform:
                    BreakingPlatform bkp = platform.GetComponent<BreakingPlatform>();
                    break;     
                case PlatformType.StickyWall:
                    StickyWall sw = platform.GetComponent<StickyWall>();
                    rb.gravityScale = gravitySc * (1 - sw.stickiness);
                    rb.velocity = Vector2.zero;
                    break;
                case PlatformType.DeadlyPlatform:
                    DeadlyPlatform dp = platform.GetComponent<DeadlyPlatform>();
                    TeleportToStart();
                    break;
                case PlatformType.WinningPlatform:
                    GameMaster.instance.LoadScene();
                    AudioManager.instance.ChangeSoundWithscending(AudioManager.instance.FindSound("level loop"), AudioManager.instance.FindSound("menu master"), 5);

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        Platform p;
        if(collision.gameObject.TryGetComponent<Platform>(out p)&&collision.relativeVelocity.y==0)
        {
            rb.velocity = rb.velocity / p.Roughness;
            pizza.currPizzaTemperature += p.temperatureChange * Time.deltaTime;
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
        AudioManager.instance.PlaySound("gameover");
        //deathSrc.clip = fatalitySound;
       // musicSrc.Pause();
       // deathSrc.Play();
    }
}
