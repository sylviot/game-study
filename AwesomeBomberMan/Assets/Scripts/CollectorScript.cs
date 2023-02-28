using UnityEngine;
using UnityEngine.UI;

public class CollectorScript : MonoBehaviour
{
    public Text scoreObject;

    void IncreaseScore()
    {
        GameState.Instance.AddScore();
        this.scoreObject.text = $"{GameState.Instance.CurrentInstanceScore} bombs";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameState.Instance.State == GameStateEnum.PLAY && collision.CompareTag("Bomb"))
        {
            this.IncreaseScore();
            Destroy(collision.gameObject);
        }
    }
}
