using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Collision collision;
    private Grab grab;
    private Jumping jumping;

    private Rigidbody2D rigidbody;

    public float jumpForce = 5f;
    public float speed = 4f;
    public float dashPower = 40f;

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        this.collision = GetComponent<Collision>();
        this.grab = GetComponent<Grab>();
        this.jumping = GetComponent<Jumping>();

        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.jumping.enabled = false;

        bool jump = Input.GetButton("Jump");
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        bool dash = Input.GetMouseButtonDown(0);// Input.GetKeyDown(KeyCode.F);
        var direction = new Vector2(x, y);

        if (jump)
        {
            if (this.collision.onGround)
            {
                this.Jump();
            }
        }

        if (dash)
        {
            if(xRaw != 0 || yRaw != 0)
            {
                this.canMove = false;
                Debug.Log($"{xRaw}, {yRaw}");
                Camera.main.transform.DOComplete();
                Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
                FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(this.transform.position));
                FindObjectOfType<GhostTrail>().Show();
                direction = new Vector2(xRaw, yRaw);
                this.rigidbody.velocity = Vector2.zero;
                this.rigidbody.velocity += direction.normalized * dashPower;
                Debug.Log($"{direction.normalized * 40f}");
                StartCoroutine(DashWait());
            }
        }

        /* Se houve alguma aceleração */
        if (x != 0f)
        {
            this.transform.localScale = new Vector3(Mathf.Sign(x) * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }

        if (this.collision.onWall)
        {
            //var direction = (this.collision.onLeftWall ? 1 : -1);
            //this.Grab(direction);
            //this.transform.localScale = new Vector3(direction * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }

        if (this.canMove && (this.collision.onGround || this.collision.onAir))
            //if (this.canMove && (this.collision.onGround || this.collision.onAir))
        {
            
            this.Walk(direction);
        }
    }

    IEnumerator DashWait()
    {
        DOVirtual.Float(14, 0, .8f, this.RigidbodyDrag);
        var gravity = this.rigidbody.gravityScale;
        this.rigidbody.gravityScale = 0;    
        
        
        yield return new WaitForSeconds(.3f);

        this.canMove = true;
        this.rigidbody.gravityScale = gravity;
    }

    private void RigidbodyDrag(float x)
    {
        this.rigidbody.drag = x;
    }

    private void Grab(float direction)
    {
        this.rigidbody.velocity = Vector2.zero;
    }

    private void Walk(Vector2 direction)
    {
        this.rigidbody.velocity = (new Vector2(direction.x * this.speed * (this.collision.onAir ? 0.75f : 1), this.rigidbody.velocity.y));
    }

    private void Jump()
    {
        this.rigidbody.velocity = new Vector2(this.rigidbody.velocity.x, 0);
        this.rigidbody.velocity += Vector2.up * this.jumpForce;
    }

    private void JumpFromWall()
    {
        this.rigidbody.velocity = new Vector2(Vector2.right.x * this.jumpForce * 0.95f, 0);
        this.Jump();
        StartCoroutine("ReactiveMovement");
    }

    private IEnumerator ReactiveMovement()
    {
        this.canMove = false;
        yield return new WaitForSeconds(0.5f);
        this.canMove = true;
    }
}
