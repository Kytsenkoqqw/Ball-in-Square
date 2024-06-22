using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMark : MonoBehaviour
{
    [SerializeField] private GameObject _trailBallPrefab; // Префаб мячика для следа
    [SerializeField] private int _trailCount = 5; // Количество мячиков в следе
    [SerializeField] private float _spawnInterval = 0.25f; // Интервал появления мячиков в следе
    [SerializeField] private float _trailLifetime = 0.5f; // Время жизни мячиков в следе

    private Queue<GameObject> _trailBallsQueue = new Queue<GameObject>();
    private float _timeSinceLastSpawn;

    void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn >= _spawnInterval)
        {
            _timeSinceLastSpawn = 0f;
            SpawnTrailBall();
        }
    }

    private void SpawnTrailBall()
    {
        if (_trailBallsQueue.Count >= _trailCount)
        {
            Destroy(_trailBallsQueue.Dequeue());
        }

        GameObject trailBall = Instantiate(_trailBallPrefab, transform.position, Quaternion.identity);
        _trailBallsQueue.Enqueue(trailBall);

        // Запускаем корутину для уничтожения мячика через _trailLifetime секунд
        StartCoroutine(DestroyTrailBallAfterTime(trailBall, _trailLifetime));
    }

    private IEnumerator DestroyTrailBallAfterTime(GameObject trailBall, float time)
    {
        yield return new WaitForSeconds(time);
        if (trailBall != null)
        {
            _trailBallsQueue.Dequeue();
            Destroy(trailBall);
        }
    }
}
