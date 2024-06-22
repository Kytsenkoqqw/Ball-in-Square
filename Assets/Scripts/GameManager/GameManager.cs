using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;

    private void Start()
    {
        BallController.OnPlayerDied += ShowLosePanel;
    }

    private void OnDestroy()
    {
        BallController.OnPlayerDied -= ShowLosePanel;
    }

    private void ShowLosePanel()
    {
        _losePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
