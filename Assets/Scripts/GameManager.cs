// filepath: /FloppyBirdECS2D/FloppyBirdECS2D/Assets/Scripts/GameManager.cs
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText;


    [SerializeField] private Text maxScoreText;

    [SerializeField] private Text timeText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;

    public PlayerData playerDatalist;

    public string currentPlayerName;
    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }

        // set the max score text
        maxScoreText.text = playerDatalist.GetMaxScore().ToString();


    }

    private void Update()
    {
        timeText.text = Time.timeSinceLevelLoad.ToString("F2");
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        currentPlayerName = "Player" + playerDatalist.players.Count;
        playerDatalist.players.Add(new PlayerData.PlayerRecord(currentPlayerName));

        // set the max score text
        maxScoreText.text = playerDatalist.GetMaxScore().ToString();


    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        // save the score data to current playername
        playerDatalist.SaveScore(currentPlayerName, score);
    }
}