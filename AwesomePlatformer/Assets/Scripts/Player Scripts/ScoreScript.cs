using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private Text coinsText;
    private AudioSource audioSource;
    private int score = 0;

    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        this.coinsText = GameObject.Find("CoinsText").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Coin"))
        {
            target.gameObject.SetActive(false);
            this.audioSource.Play();
            this.score++;
            this.coinsText.text = this.score.ToString("x#");
        }
    }
}
