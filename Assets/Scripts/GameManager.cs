using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instace;

    public float timeToMatch = 10f;
    public float currentTimeToMatch = 0;

    public enum GameState
    {
        Idle,
        InGame,
        GameOver
    }
    public GameState gameState;

    public int Points = 0;
    public UnityEvent OnPointsUpdate;
    public UnityEvent<GameState> OnGameStateUpdate;

    private void Awake()
    {
        if (Instace == null)
        {
            Instace = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        if (gameState == GameState.InGame)
        {
            currentTimeToMatch += Time.deltaTime;
            if (currentTimeToMatch > timeToMatch)
            {
                gameState = GameState.GameOver;
                OnGameStateUpdate?.Invoke(gameState);
            }
        }
    }


    public void AddPoint(int newPoints)
    {
        Points += newPoints;
        OnPointsUpdate?.Invoke();
        currentTimeToMatch = 0;
    }

    public void RestartGame()
    {
        Points = 0;
        gameState = GameState.InGame;
        OnGameStateUpdate?.Invoke(gameState);
        currentTimeToMatch = 0f;
    }

    public void ExitGame()
    {

    }
}
