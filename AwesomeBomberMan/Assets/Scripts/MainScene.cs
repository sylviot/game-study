using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public TextMeshProUGUI label_best_score;
    public TextMeshProUGUI txt_best_score;
    
    public TextMeshProUGUI label_awesome;
    public TextMeshProUGUI label_press_any_key;

    // Start is called before the first frame update
    void Start()
    {
        GameState.Instance.StartIdle();

        int best_score = GameState.Instance.BestScore();

        //label_best_score.enabled = txt_best_score.enabled = false;
        var tweenParams = new TweenParams().SetLoops(-1).SetEase(Ease.InOutFlash);

        if (best_score > 0)
        {
            label_best_score.enabled = txt_best_score.enabled = true;
            txt_best_score.text = best_score.ToString("000");

        }

        label_awesome.transform
            .DOScale(new Vector3(1.05f, 1.05f, 1), 2f)
            .SetAs(tweenParams);

        label_press_any_key.transform
            .DOScale(new Vector3(1.05f, 1.05f, 1), 2f)
            .SetAs(tweenParams);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.Instance.State == GameStateEnum.IDLE && (Input.anyKeyDown || Input.touchCount > 0))
        {
            GameState.Instance.StartGame();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
    }
}
