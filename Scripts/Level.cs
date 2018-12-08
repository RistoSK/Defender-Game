using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Level : MonoBehaviour
{
    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    { 
        SceneManager.LoadScene(1);
        GameSession gameSession = FindObjectOfType<GameSession>();

        if (gameSession)
        {
            gameSession.ResetGame();
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverDelay());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
}
