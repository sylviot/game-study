using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator animator;

    private Vector3 moveDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;

    public GameObject birdEgg;
    public LayerMask playerLayer;
    
    private float speed = 3f;
    private bool attacked;
    private bool canMove;

    void Awake()
    {
        this.myBody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.originPosition = transform.position;
        this.originPosition.x += 6f;

        this.movePosition = transform.position;
        this.movePosition.x -= 6;

        this.canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.MoveTheBird();
        this.DropTheEgg();
    }

    void MoveTheBird()
    {
        if (this.canMove)
        {
            this.transform.Translate(this.moveDirection * this.speed * Time.smoothDeltaTime);
            if(this.transform.position.x >= this.originPosition.x)
            {
                this.moveDirection = Vector3.left;
                this.ChangeDirection(Mathf.Abs(this.transform.localScale.x));
            }
            else if(this.transform.position.x <= this.movePosition.x)
            {
                this.moveDirection = Vector3.right;
                this.ChangeDirection(-Mathf.Abs(this.transform.localScale.x));
            }
        }
    }

    void ChangeDirection(float direction)
    {
        Vector3 tempScale = this.transform.localScale;
        tempScale.x = direction;
        this.transform.localScale = tempScale;
    }

    void DropTheEgg()
    {
        if (!this.attacked)
        {
            if(Physics2D.Raycast(this.transform.position, Vector2.down, Mathf.Infinity, this.playerLayer))
            {
                Instantiate(this.birdEgg, new Vector3(this.transform.position.x, this.transform.position.y - 1f, this.transform.position.z), Quaternion.identity);
                this.attacked = true;
            }
        }
    }

    IEnumerator BirdDead()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Bullet"))
        {
            this.animator.Play("BirdDead");
            GetComponent<BoxCollider2D>().isTrigger = true;
            this.myBody.bodyType = RigidbodyType2D.Dynamic;
            this.canMove = false;

            StartCoroutine(this.BirdDead());
        }
    }
}
