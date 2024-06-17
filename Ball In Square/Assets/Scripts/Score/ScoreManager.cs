using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText; // UI текст для отображения счета
    [SerializeField] private GameObject _gameOverPanel; // Панель конца игры
    [SerializeField] private TextMeshProUGUI _currentScoreText; // Текст текущего счета на панели
    [SerializeField] private TextMeshProUGUI _highScoreText; // Текст лучшего результата на панели
    [SerializeField] private ScoreManager _scoreManager;
    private int _score = 0;

    
    private void Start()
    {
        UpdateScoreText();
        _gameOverPanel.SetActive(false); // Скрываем панель конца игры в начале
    }

    public void AddScore(int amount)
    {
        _score += amount;
        UpdateScoreText();
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

        // Обновляем текст на панели
        _currentScoreText.text = _score.ToString();
        _highScoreText.text = highScore.ToString();

        // Показываем панель конца игры
        _gameOverPanel.SetActive(true);
    }
}
