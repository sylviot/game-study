using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    public bool onGround, onWall, onLeftWall, onRightWall, onBottomWall, onAir;

    [Header("Offsets")]
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, leftOffset, rightOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        this.onGround = Physics2D.OverlapCircle((Vector2)this.transform.position + this.bottomOffset, this.collisionRadius, this.groundLayer);
        this.onLeftWall = Physics2D.OverlapCircle((Vector2)this.transform.position + this.leftOffset, this.collisionRadius, this.wallLayer);
        this.onRightWall = Physics2D.OverlapCircle((Vector2)this.transform.position + this.rightOffset, this.collisionRadius, this.wallLayer);
        this.onBottomWall = Physics2D.OverlapCircle((Vector2)this.transform.position + this.bottomOffset, this.collisionRadius, this.wallLayer);

        this.onWall = !this.onGround && (this.onLeftWall || this.onRightWall);

        this.onAir = !this.onGround && !this.onWall;
    }

    /* */
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var offset in new Vector2[] { bottomOffset, leftOffset, rightOffset })
        {
            Gizmos.DrawWireSphere((Vector2)this.transform.position + offset, this.collisionRadius);
        }
    }
}
