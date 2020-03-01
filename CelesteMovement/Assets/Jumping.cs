using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    private Rigidbody2D rigidbidy;
    public bool jumpUp;
    public bool jumpDown;

    void Start()
    {
        this.jumpUp = this.jumpDown = false;
        this.rigidbidy = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log("Jumpping");
        if(this.rigidbidy.velocity.y < 0)
        {
            this.jumpUp = true;
            this.jumpDown = false;
            this.rigidbidy.velocity += Vector2.up * Physics2D.gravity.y * 2.5f * Time.deltaTime;
        }
        else if(this.rigidbidy.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            this.jumpUp = false;
            this.jumpDown = true;
            this.rigidbidy.velocity += Vector2.up * Physics2D.gravity.y * 7 * Time.deltaTime;
        }
        else
        {
            this.jumpUp = false;
            this.jumpDown = false;
        }
    }
}
