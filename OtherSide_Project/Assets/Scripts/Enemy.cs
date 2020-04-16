using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public bool isInside;
    public float distance = 1f;
    public float waitTime;
    Transform groundCheck;
    Rigidbody2D rb;
    float direction;
    bool walking = true;
    float time;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        direction = transform.localScale.x / Mathf.Abs(transform.localScale.x);
    }
    private void Update()
    {
        if (walking)
        {
            if (!GroundCheck())
            {
                walking = false;
                direction = 0;
                time = waitTime;
            }
        }
        else
        {
            if (time > 0)
            {
                time -= Time.fixedDeltaTime;
                if (time <= 0)
                {

                    direction = transform.localScale.x / Mathf.Abs(transform.localScale.x) * -1;
                    Flip();
                    walking = true;
                }
            }
        }
        if (transform.localScale.x > 0 && direction < 0)
        {
            Flip();
        }
        else if (transform.localScale.x < 0 && direction > 0)
        {
            Flip();
        }
    }
    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = direction * speed;
        rb.velocity = velocity;
    }
    void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    bool GroundCheck()
    {
        if (isInside)
        {
            if (!Physics2D.Raycast(groundCheck.position, Vector2.down, distance, StaticStuff.i.groundInside) || Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x / Mathf.Abs(transform.localScale.x), distance / 3, StaticStuff.i.groundInside))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if(!Physics2D.Raycast(groundCheck.position, Vector2.down, distance, StaticStuff.i.groundOutside)|| Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x / Mathf.Abs(transform.localScale.x), distance / 3, StaticStuff.i.groundOutside))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
