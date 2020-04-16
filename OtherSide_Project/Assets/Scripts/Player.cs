using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float accelaration;
    public float jumpForce;
    public float fallClamp;
    public DialogueHandler dialogueHandler;


    public bool randomDimension;

    float groundedRememberTime = .1f;
    float radius = .13f;
    float winTimer = 1f;
    public float whitePixelGap;
    InputActions input;
    Transform groundCheck;
    Transform pixelHolder;
    Rigidbody2D rb;
    float direction;
    bool canJump;
    bool isInside;
    float groundedRemember;
    float winRememberTimer;
    Transform spawnPoint;
    Transform[] whitePixels;
    int currentPixel = 2;
    private void Start()
    {
        //get some stuff
        rb = GetComponent<Rigidbody2D>();


        pixelHolder = transform.Find("PixelHolder");
        //dialogue = GetComponent<DialogueHandler>();
        groundCheck = transform.Find("GroundCheck");


        //no need to worry about it no more
        HandleInput();
        whitePixels = new Transform[3];
        SpawnPixels();


    }
    void SpawnPixels()
    {
        for (int i = 0; i < whitePixels.Length; i++)
        {
            whitePixels[i] = Instantiate(StaticStuff.i.whitePixel, (Vector2)pixelHolder.position + Vector2.right * i * whitePixelGap, Quaternion.identity, pixelHolder);
        }
    }
    private void Update()
    {
        if (randomDimension && Random.Range(0, 2) == 0)
        {
            randomDimension = false;
            DoSwap();
        }
        else
        {
            randomDimension = false;
        }
        groundedRemember -= Time.deltaTime;

        //Update canJump
        if (GroundCheck())
        {
            canJump = true;
            groundedRemember = groundedRememberTime;
        }

        //Look for difference && flip
        HandleFlip();
    }
    private void FixedUpdate()
    {
        //do it
        HandleMovement();
    }

    public void AddAndShowTextNow(Dialogue.Sentence[] dialogue)
    {
        dialogueHandler.AddAndShowTextNow(dialogue);
    }
    private void HandleMovement()
    {
        Vector2 velocity = rb.velocity;
        if (direction != 0)
        {
            velocity.x += direction * accelaration;
        }
        else
        {
            velocity.x = 0;
        }
        velocity.y = Mathf.Clamp(velocity.y, -fallClamp, Mathf.Infinity);
        velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
        rb.velocity = velocity;
    }
    private void HandleJump()
    {
        if (!PauseManager.isPaused)
        {
            if (groundedRemember > 0)
            {
                Jump();
            }
            else if (canJump)
            {
                Jump();
                canJump = false;
            }
        }
    }
    private void HandleInput()
    {
        input = new InputActions();
        input.Enable();
        input.Player.Move.performed += ctx => direction = ctx.ReadValue<float>();
        input.Player.Jump.performed += ctx => HandleJump();
        input.Player.SetSpawn.performed += ctx => TrySetSpawn();
    }
    private void HandleFlip()
    {
        if (transform.localScale.x > 0 && direction < 0)
        {
            Flip();
        }
        else if (transform.localScale.x < 0 && direction > 0)
        {
            Flip();
        }
    }
    private void TrySetSpawn()
    {
        if (!PauseManager.isPaused)
        {
            if (GroundCheck() && currentPixel + 1 > 0)
            {
                if (spawnPoint)
                {
                    Destroy(spawnPoint.gameObject);
                }
                spawnPoint = Instantiate(StaticStuff.i.spawnPointPrefab, transform.position, Quaternion.identity);
                if (transform.localScale.x < 0)
                {
                    Vector2 scale = spawnPoint.localScale;
                    scale.x *= -1;
                    spawnPoint.localScale = scale;
                }
                GameObject effect=Instantiate(StaticStuff.i.spawnPointSetEffect,transform.position,Quaternion.identity);
                Destroy(effect,3f);
                SoudManager.instance.Play("SetSpawn");
            }
        }
    }
    void DoSwap()
    {
        SwapManager.i.Swap();
        if (gameObject.layer == StaticStuff.i.playerInside)
        {
            gameObject.layer = StaticStuff.i.playerOutside;
        }
        else
        {
            gameObject.layer = StaticStuff.i.playerInside;
        }
        isInside = !isInside;
    }
    void Jump()
    {
        Vector2 velocity = rb.velocity;
        velocity.y = jumpForce;
        rb.velocity = velocity;
        SoudManager.instance.Play("Jump");
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
            return Physics2D.OverlapCircle(groundCheck.position, radius, StaticStuff.i.groundInside);
        }
        else
        {
            return Physics2D.OverlapCircle(groundCheck.position, radius, StaticStuff.i.groundOutside);
        }

    }
    void Die()
    {
        SoudManager.instance.Play("Hurt");
        GameObject explosion = Instantiate(StaticStuff.i.dieExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 3f);
        if (spawnPoint)
        {
            transform.position = spawnPoint.position;
            DoSwap();
            rb.velocity = Vector2.zero;
            Destroy(spawnPoint.gameObject);
            Destroy(whitePixels[currentPixel].gameObject);
            currentPixel--;
            direction = 0;
        }
        else
        {
            transform.Find("Sprite").gameObject.SetActive(false);
            LevelLoader.i.LoadRestartLevel(1f);
            rb.bodyType = RigidbodyType2D.Static;

            Destroy(this);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hurt"))
        {
            Die();
        }
        else if (other.CompareTag("End"))
        {
            SoudManager.instance.Play("Pickup");
            LevelLoader.i.LoadNextLevel();
        }
        else if (other.CompareTag("Buff"))
        {
            if (currentPixel < 2)
            {
                SoudManager.instance.Play("Pickup");
                currentPixel++;
                whitePixels[currentPixel] = Instantiate(StaticStuff.i.whitePixel, (Vector2)pixelHolder.position + Vector2.right * currentPixel * whitePixelGap * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), Quaternion.identity, pixelHolder);
                Destroy(other.gameObject);
            }
        }
    }
    private void OnDisable()
    {
        input?.Disable();
    }
}
