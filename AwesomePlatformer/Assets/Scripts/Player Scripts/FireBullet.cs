using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float speed = 10f;
    public float Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }

    private Animator animator;
    private bool canMove;
    void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    void Start()
    {
        this.canMove = true;
        StartCoroutine(DisableBullet(5f));
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
    }

    void Move()
    {
        if (this.canMove)
        {
            Vector3 tempPosition = this.transform.position;
            tempPosition.x += this.speed * Time.deltaTime;
            this.transform.position = tempPosition;
        }
    }

    IEnumerator DisableBullet(float timeSeconds)
    {
        yield return new WaitForSeconds(timeSeconds);
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Snail") || target.gameObject.CompareTag("Beetle") || target.gameObject.CompareTag("Spider"))
        {
            this.canMove = false;
            this.animator.Play("BulletExplode");
            StartCoroutine(DisableBullet(0.15f));
        }
    }
}