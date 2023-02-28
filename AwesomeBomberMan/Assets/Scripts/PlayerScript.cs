using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Image bg_your_score;
    public TextMeshProUGUI txt_your_score;
    public TextMeshProUGUI label_your_score;

    protected float speed = 10f;
    private const float MIN_X = -2.55f;
    private const float MAX_X = 2.55f;
    //// Start is called before the first frame update
    void Start()
    {
        bg_your_score.enabled = txt_your_score.enabled = label_your_score.enabled = false;
        txt_your_score.text = "";
    }

    void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.Instance.State == GameStateEnum.PLAY)
        {

            MovePlayer();
        }
    }

    void MovePlayer()
    {
        float h = Input.GetAxis("Horizontal");
        Vector3 currentPosition = this.transform.position;

        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved && touch.deltaPosition.x > 0)
            {
                MoveRight(ref currentPosition);
                Debug.Log($"TOUCH {touch.deltaPosition}");
            }
            else if(touch.phase == TouchPhase.Moved && touch.deltaPosition.x < 0)
            {
                MoveLeft(ref currentPosition);
            }
        }

        /* Go to right side */
        if (h > 0)
        {
            MoveRight(ref currentPosition);
        }

        /* Go to left side */
        if (h < 0)
        {
            MoveLeft(ref currentPosition);
        }

        currentPosition.x = Mathf.Clamp(currentPosition.x, MIN_X, MAX_X);

        this.transform.position = currentPosition;
    }

    private void MoveLeft(ref Vector3 currentPosition)
    {
        currentPosition.x -= speed * Time.deltaTime;
    }

    private void MoveRight(ref Vector3 currentPosition)
    {
        currentPosition.x += speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            //Time.timeScale = 0f;
            GameState.Instance.EndGame();

            bg_your_score.enabled = txt_your_score.enabled = label_your_score.enabled = true;
            txt_your_score.text = GameState.Instance.CurrentInstanceScore.ToString("000");
        }
    }

    void ChangeScene2()
    {
        Debug.Log("ChangeScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1,LoadSceneMode.Single);
    }
}
