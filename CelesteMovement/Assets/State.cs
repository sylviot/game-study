using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    protected Animator animator;
    protected Collision collision;
    protected Movement movement;
    protected Rigidbody2D rigidbody;

    public bool walking = false;
    public bool idle = false;
    public bool jumping = false;
    public bool falling = false;
    public bool grabing = false;

    public int ActualState = 0;

    void Start()
    {
        this.animator = GetComponentInChildren<Animator>();
        this.collision = GetComponent<Collision>();
        this.movement = GetComponent<Movement>();
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        this.walking = this.collision.onGround && this.rigidbody.velocity.x != 0f;
        this.idle = this.collision.onGround;
        this.jumping = this.collision.onAir && this.rigidbody.velocity.y > 0f;
        this.falling = this.collision.onAir && this.rigidbody.velocity.y < 0f;
        this.grabing = this.collision.onWall;

        if (this.idle)
        {
            this.ActualState = 0;
        }

        if (this.walking)
        {
            this.ActualState = 1;
        }

        if (this.jumping)
        {
            this.ActualState = 2;
        }

        if(this.falling)
        {
            this.ActualState = 3;
        }

        if (this.grabing)
        {
            this.ActualState = 4;
        }


        this.animator.SetInteger("state", this.ActualState);
    }
}
