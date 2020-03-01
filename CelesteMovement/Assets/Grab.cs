using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float fallMultiplier = 7;

    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        this.rigidbody.velocity += Vector2.up * Physics2D.gravity.y * this.fallMultiplier * Time.deltaTime;
    }
}
