using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D myBody;

    private Vector3 moveDirection = Vector3.down;
    private string changeMovementCoRoutineName = "ChangeMovement";

    private float time = 1.4f;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
        this.myBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeMovementCoRoutineName);
    }

    // Update is called once per frame
    void Update()
    {
        this.MoveSpider();
    }

    void MoveSpider()
    {
        this.transform.Translate(this.moveDirection * Time.smoothDeltaTime);
    }

    IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(this.time);

        if(this.moveDirection == Vector3.down)
        {
            this.moveDirection = Vector3.up;
        }
        else
        {
            this.moveDirection = Vector3.down;
        }

        StartCoroutine(changeMovementCoRoutineName);
    }

    IEnumerator SpiderDead()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Bullet"))
        {
            this.animator.Play("SpiderDead");

            this.myBody.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<BoxCollider2D>().isTrigger = true;

            StopCoroutine(changeMovementCoRoutineName);
            StartCoroutine(SpiderDead());
        }
    }
}
