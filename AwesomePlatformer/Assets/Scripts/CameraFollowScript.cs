using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public float resetSpeed = 0.5f;
    public float cameraSpeed = 0.3f;

    public Bounds cameraBounds;

    private Transform target;
    private float offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 currentVelocity;

    private bool followsPlayer;

    void Awake()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
        this.cameraBounds = boxCollider.bounds;
    }

    void Start()
    {
        this.target = GameObject.FindGameObjectWithTag("Player").transform;
        this.lastTargetPosition = this.target.position;
        this.offsetZ = (this.transform.position - this.target.position).z;
        this.followsPlayer = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.followsPlayer)
        {
            Vector3 aheadTargetPosition = this.target.position + Vector3.forward * offsetZ;

            if(aheadTargetPosition.x >= this.transform.position.x)
            {
                Vector3 newCameraPosition = Vector3.SmoothDamp(this.transform.position, aheadTargetPosition, ref currentVelocity, cameraSpeed);
                this.transform.position = new Vector3(newCameraPosition.x, transform.position.y, newCameraPosition.z);

                this.lastTargetPosition = target.position;
            }
        }
    }
}
