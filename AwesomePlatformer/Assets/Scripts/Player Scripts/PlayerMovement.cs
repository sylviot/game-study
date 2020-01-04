using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;

    private float jumpPower = 12f;

    void Awake()
    {
        this.myBody = this.GetComponent<Rigidbody2D>();
        this.anim = this.GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckIfGrounded();
        this.PlayerJump();
    }

    void FixedUpdate()
    {
        this.PlayerWalk();    
    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if(h > 0)
        {
            this.myBody.velocity = new Vector2(speed, this.myBody.velocity.y);
            this.PlayerChangeDirection(1);
        }
        else if (h < 0)
        {
            this.myBody.velocity = new Vector2(-speed, this.myBody.velocity.y);
            this.PlayerChangeDirection(-1);
        }
        else
        {
            this.myBody.velocity = new Vector2(0f, this.myBody.velocity.y);
        }

        this.anim.SetInteger("Speed", (int)Mathf.Abs(this.myBody.velocity.x));
    }

    void PlayerChangeDirection(int direction)
    {
        Vector3 tempScale = this.transform.localScale;
        tempScale.x = direction;
        this.transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(this.groundCheckPosition.position, Vector2.down, 0.1f, this.groundLayer);

        if(this.isGrounded)
        {
            if(this.jumped)
            {
                this.jumped = false;

                this.anim.SetBool("Jump", false);
            }
        }
    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                this.myBody.velocity = new Vector2(this.myBody.velocity.x, this.jumpPower);

                this.anim.SetBool("Jump", true);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Ground"))
        {
            print("Collision");
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        
    }
}
