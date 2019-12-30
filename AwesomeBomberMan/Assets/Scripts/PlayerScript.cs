using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    protected float speed = 10f;
    private const float MIN_X = -2.55f;
    private const float MAX_X = 2.55f;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float h = Input.GetAxis("Horizontal");
        Vector3 currentPosition = this.transform.position;

        /* Go to right side */
        if(h > 0)
        {
            currentPosition.x += speed * Time.deltaTime;
        }

        /* Go to left side */
        if(h < 0)
        {
            currentPosition.x -= speed * Time.deltaTime;
        }

        currentPosition.x = Mathf.Clamp(currentPosition.x, MIN_X, MAX_X);

        this.transform.position = currentPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bomb"))
        {
            Time.timeScale = 0f;
        }
    }
}
