using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textLives;
    public TextMeshProUGUI textTime;
    public GameObject PausePanel;
    public GameObject GameOverPanel;
    public GameObject WinPanel;

    [SerializeField] private Animation damageAnimator;
    [SerializeField] private int lives = 3;
    [SerializeField] private int score;

    private float timer;
    private int remainingBlocks;

    private void Start()
    {
        textLives.text = lives.ToString();

        remainingBlocks = FindObjectsOfType<Block>().Length;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        textTime.text = ((int)timer).ToString();
    }

    public void BlockDestroyed()
    {
        remainingBlocks--;

        if (remainingBlocks <= 0)
        {
            WinGame();
        }
    }

    public void LoseLife()
    {
        lives--;
        textLives.text = lives.ToString();

        damageAnimator.Play("Damage Animation");

        if (lives <= 0) GameOver();
    }

    public void AddLife()
    {
        lives++;
        textLives.text = lives.ToString();
    }

    public void AddScore()
    {
        score++;
        textScore.text = score.ToString();
    }

    private void WinGame()
    {
        Time.timeScale = 0f;
        WinPanel.SetActive(true);
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        GameOverPanel.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
    }

    public void TurnOffPause()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }

    public void RestartLevl(string LevlName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(LevlName);
    }
}