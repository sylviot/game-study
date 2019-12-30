using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectorScript : MonoBehaviour
{
    public Text scoreObject;
    private int score;

    private void Start()
    {
        this.score = 0;
    }
    void IncreaseScore()
    {
        this.score++;
        this.scoreObject.text = $"{score} bombs";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bomb"))
        {
            this.IncreaseScore();
        }
    }
}
