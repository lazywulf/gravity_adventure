using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // events
    [SerializeField] private EventListener gameOver = new();
    [SerializeField] private EventListener blackhole = new();
    [SerializeField] private EventListener win = new();
    [SerializeField] private EventListener pause = new();


    public enum GameState { Start, Playing, Paused, GameOver, Win }
    public GameState CurrentState { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        gameOver.Subscribe(GameOver);
        blackhole.Subscribe(Blackhole);
        win.Subscribe(WinGame);
        pause.Subscribe(PauseGame);
        CurrentState = GameState.Start;
    }

	void OnDisable() {
        gameOver.Unsubscribe();
        blackhole.Unsubscribe();
        win.Unsubscribe();
        pause.Unsubscribe();
    }

    public void StartGame() {
        Debug.Log("Game starting.");
    }

    // TODO: FINISH THIS ONCE THE INPUT SYSTEM IS MERGED
    public void PauseGame() { }

    public void ResumeGame() { }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver() {

        Debug.Log("Blasted by a supernova.");
    }

    public void Blackhole() {
        Debug.Log("Eaten by a black hole.");
    }

    public void WinGame() {
        Debug.Log("You win!");
    }

}
