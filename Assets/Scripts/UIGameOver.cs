using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{

    public int displayedPoints = 0;
    public TextMeshProUGUI pointsUI;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instace.OnGameStateUpdate.AddListener(GameStateUpdated);
    }

    private void GameStateUpdated(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.GameOver)
        {
            displayedPoints = 0;
            StartCoroutine(DisplayedPointsCoroutine());
        }
    }

    IEnumerator DisplayedPointsCoroutine()
    {
        while (displayedPoints < GameManager.Instace.Points)
        {
            displayedPoints++;
            pointsUI.text = displayedPoints.ToString();
            yield return new WaitForFixedUpdate();
        }

        displayedPoints = GameManager.Instace.Points;
        pointsUI.text = displayedPoints.ToString();

        yield return null;
    }

    public void PlayAgainBtnClicked()
    {
        GameManager.Instace.RestartGame();
    }


    public void ExitGameBtnClicked()
    {
        GameManager.Instace.ExitGame();
    }
}
