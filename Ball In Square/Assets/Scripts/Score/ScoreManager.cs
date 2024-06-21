using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText; 
    [SerializeField] private GameObject _gameOverPanel; 
    [SerializeField] private TextMeshProUGUI _currentScoreText; 
    [SerializeField] private TextMeshProUGUI _highScoreText; 
    [SerializeField] private BackgroundColorChanger _backgroundColorChanger; 
    [SerializeField] private DifficultyManager _difficultyManager;

    private int _score = 0;

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        _score += amount;
        UpdateScoreText();
        
        _difficultyManager.CheckDifficulty(_score);
        
        if (_score % 5 == 0 && _score != 0)
        {
            _backgroundColorChanger.ChangeBackgroundColor();
        }
    }

    private void UpdateScoreText()
    {
        _scoreText.text = _score.ToString();
    }

    public void GameOver()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (_score > highScore)
        {
            highScore = _score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        _currentScoreText.text = _score.ToString();
        _highScoreText.text = highScore.ToString();
        _gameOverPanel.SetActive(true);
    }
}
