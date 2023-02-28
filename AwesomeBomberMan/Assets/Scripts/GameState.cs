using UnityEngine;

public enum GameStateEnum
{
    IDLE,
    PLAY,
    GAME_OVER,
    MOVING_TO_PLAY,
    MOVING_TO_IDLE
}

public class GameState
{
    private static GameState _instance;
    public static GameState Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameState();
            }

            return _instance;
        }
    }

    public GameStateEnum State { get; private set; } = GameStateEnum.IDLE;

    public const string BEST_SCORE = "best_score";

    public const float MIN_X = -2.55f; // 0
    public const float MAX_X = 2.55f; // 400 ?!?
    public const float InitialTime = 1.5f;
    public const float FinalTime = 0.7f;

    // Instance
    public int CurrentInstanceScore { get; private set; }
    public float CurrentInstanceTime { get; private set; }

    private GameState() { }

    public void AddScore() => CurrentInstanceScore++;
    public int BestScore() => PlayerPrefs.GetInt(BEST_SCORE);

    public void MovingTo(GameStateEnum movingTo)
    {
        if (movingTo == GameStateEnum.MOVING_TO_IDLE || movingTo == GameStateEnum.MOVING_TO_PLAY)
            State = movingTo;
    }

    public void StartIdle()
    {
        State = GameStateEnum.IDLE;
    }

    public void StartGame()
    {
        State = GameStateEnum.PLAY;
        CurrentInstanceScore = 0;
        CurrentInstanceTime = InitialTime;
    }

    public void EndGame()
    {
        State = GameStateEnum.GAME_OVER;
        int bestScore = BestScore();

        if (CurrentInstanceScore > bestScore)
        {
            PlayerPrefs.SetInt(BEST_SCORE, CurrentInstanceScore);
        }
    }

    public Vector3 NewSpawn()
    {
        return new Vector3(0, 0, 0);
    }
}
