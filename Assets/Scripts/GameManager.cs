using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnScoreUpdated;
    public static event Action<int> OnLivesUpdated;
    public static event Action OnGameOver;

    [SerializeField] AsteroidSpawner asteroidSpawner;
    [SerializeField] PlayerController playerController;

    private int currentLives = 3;
    private int currentScore = 0;
    private int currentLevel = 1;


    private void OnEnable()
    {
        PlayerController.OnPlayerDied += PlayerDied;
        Asteroid.OnAsteroidDestroyed += Asteroid_OnAsteroidDestroyed;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDied -= PlayerDied;
        Asteroid.OnAsteroidDestroyed -= Asteroid_OnAsteroidDestroyed;
    }


    private void Asteroid_OnAsteroidDestroyed(Asteroid obj)
    {
        IncreaseScore(obj.GetPointValue());
    }


    private void PlayerDied()
    {
        LostLife(1);
    }

    public void IncreaseScore(int value)
    {
        currentScore += value;

        OnScoreUpdated?.Invoke(currentScore);

        CheckLevelComplete();
    }

    public void LostLife(int livesLost)
    {
        currentLives -= livesLost;
        OnLivesUpdated?.Invoke(currentLives);

        if (currentLives <= 0)
        {
            OnGameOver?.Invoke();

            Time.timeScale = 0;
        }
    }

    private void CheckLevelComplete()
    {
        int asteroidCount = asteroidSpawner.GetAsteroidCount();

        Debug.Log("Asteroid Count: " + asteroidCount.ToString());
        if (asteroidSpawner.GetAsteroidCount() == 0)
        {
            currentLevel++;
            StartLevel();

        }
    }

    private void StartLevel()
    {
        asteroidSpawner.SpawnObjects(currentLevel * 4);
        playerController.ResetPlayerPosition();
    }
}
