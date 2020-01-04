using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D myBody;
    private Animator animator;
    public string stunnedAnimationName;
    public LayerMask playerLayer;

    private bool moveLeft;
    private bool canMove;
    private bool stunned;

    public Transform leftCollision, rightCollision, topCollision, downCollision;
    public Vector3 leftCollisionPosition, rightCollisionPosition;

    void Awake()
    {
        this.myBody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

        this.leftCollisionPosition = leftCollision.position;
        this.rightCollisionPosition = rightCollision.position;
    }

    void Start()
    {
        this.moveLeft = true;
        this.canMove = true;
        this.stunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.canMove)
        {
            if (this.moveLeft)
            {
                this.myBody.velocity = new Vector2(-this.moveSpeed, this.myBody.velocity.y);
            }
            else
            {
                this.myBody.velocity = new Vector2(this.moveSpeed, this.myBody.velocity.y);
            }
        }

        this.CheckCollision();
    }

    void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(this.leftCollision.position, Vector2.left, 0.1f, this.playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(this.rightCollision.position, Vector2.right, 0.1f, this.playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(this.topCollision.position, 0.3f, this.playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.CompareTag("Player"))
            {
                if (!this.stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);

                    this.myBody.velocity = Vector2.zero;
                    this.animator.Play(this.stunnedAnimationName);
                    this.canMove = false;
                    this.stunned = true;

                    if (this.gameObject.CompareTag("Beetle"))
                    {
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if (leftHit)
        {
            if (leftHit.collider.gameObject.CompareTag("Player"))
            {
                if (!this.stunned)
                {

                }
                else
                {
                    if (!this.gameObject.CompareTag("Beetle"))
                    {
                        this.myBody.velocity = new Vector2(15f, this.myBody.velocity.y);
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if (rightHit)
        {
            if (rightHit.collider.gameObject.CompareTag("Player"))
            {
                if (!this.stunned)
                {

                }
                else
                {
                    if (!this.gameObject.CompareTag("Beetle"))
                    {
                        this.myBody.velocity = new Vector2(-15f, this.myBody.velocity.y);
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if (!Physics2D.Raycast(this.downCollision.position, Vector2.down, 0.01f))
        {
            this.ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        this.moveLeft = !this.moveLeft;

        Vector3 tempScale = this.transform.localScale;

        if (this.moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            this.leftCollision.position = this.leftCollisionPosition;
            this.rightCollision.position = this.rightCollisionPosition;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
            this.leftCollision.position = this.rightCollisionPosition;
            this.rightCollision.position = this.leftCollisionPosition;
        }

        this.transform.localScale = tempScale;
    }

    IEnumerator Dead(float timeSeconds)
    {
        yield return new WaitForSeconds(timeSeconds);
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Bullet"))
        {
            if (this.CompareTag("Beetle"))
            {
                this.animator.Play(stunnedAnimationName);
                this.canMove = false;
                this.myBody.velocity = Vector2.zero;
                StartCoroutine(this.Dead(0.5f));
            }

            if (this.CompareTag("Snail"))
            {
                if (!this.stunned)
                {
                    this.animator.Play(stunnedAnimationName);
                    this.stunned = true;
                    this.canMove = false;
                    this.myBody.velocity = Vector2.zero;
                }
                else
                {
                    this.gameObject.SetActive(false);
                }

            }
        }
    }
}
