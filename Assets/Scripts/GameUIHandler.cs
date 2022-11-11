using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] GameObject gameOverScreen;


    private void OnEnable()
    {
        GameManager.OnScoreUpdated += UpdateScore;
        GameManager.OnLivesUpdated += UpdateLives;
        GameManager.OnGameOver += GameOver;
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    private void OnDisable()
    {
        GameManager.OnScoreUpdated -= UpdateScore;
        GameManager.OnLivesUpdated -= UpdateLives;
        GameManager.OnGameOver -= GameOver;
    }

    public void UpdateLives(int lives)
    {
        livesText.text = lives.ToString();

    }

    public void UpdateScore(int currentScore)
    {
        scoreText.text = currentScore.ToString();

    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
     
}
