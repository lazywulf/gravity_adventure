using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { Start, Playing, Paused, GameOver, Win }
    public GameState CurrentState { get; private set; }

    [Header("References")]
    public GameObject playerApple;
    //public UIManager uiManager;
    //public EventManager eventManager;

    [Header("Grav Settings")]
    public bool gravPackUnlocked = false;
    public float globalGravMultiplier = 1f;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SetGameState(GameState.Start);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (CurrentState == GameState.Playing && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void SetGameState(GameState newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case GameState.Start:
                //uiManager.ShowStartScreen();
                break;
            case GameState.Playing:
                Time.timeScale = 1f;
                //uiManager.HideAllScreens();
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                //uiManager.ShowPauseScreen();
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                //uiManager.ShowGameOverScreen();
                break;
            case GameState.Win:
                Time.timeScale = 0f;
                //uiManager.ShowWinScreen();
                break;
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.Playing);
        //eventManager.StartLevel();
    }

    public void PauseGame()
    {
        SetGameState(GameState.Paused);
    }

    public void ResumeGame()
    {
        SetGameState(GameState.Playing);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
    }

    public void WinGame()
    {
        SetGameState(GameState.Win);
    }

}
