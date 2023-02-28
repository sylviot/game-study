using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.Instance.State == GameStateEnum.GAME_OVER)
        {
            GameState.Instance.MovingTo(GameStateEnum.MOVING_TO_IDLE);
            Debug.Log("Game over");

            Invoke(nameof(ChangeScene), 3f);

            Debug.Log("/Game over");
        }
    }

    void ChangeScene()
    {
        Debug.Log("ChangeScene");
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
