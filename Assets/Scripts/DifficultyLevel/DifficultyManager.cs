using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private EnemyMove _enemyMove; // Ссылка на EnemyMove
    [SerializeField] private int _enemySpawnScoreThreshold = 7; // Порог для добавления нового врага
    [SerializeField] private int _enemyScaleScoreThreshold = 5; // Порог для увеличения размера врагов
    [SerializeField] private Vector3 _scaleIncrease = new Vector3(0.80f, 0.80f, 0.80f); // Величина увеличения размера
    
    private int _lastScaleIncreaseScore = 0;
    
    public void CheckDifficulty(int currentScore)
    {
        if (currentScore % _enemySpawnScoreThreshold == 0 && currentScore != 0)
        {
            _enemyMove.SpawnNewEnemy();
        }

        if (currentScore % _enemyScaleScoreThreshold == 0 && currentScore != 0)
        {
            ScaleEnemies();
            _lastScaleIncreaseScore = currentScore;
        }
    }

    private void ScaleEnemies()
    {
        foreach (GameObject enemy in _enemyMove.GetEnemies())
        {
            enemy.transform.localScale += _scaleIncrease;
        }
    }
}
