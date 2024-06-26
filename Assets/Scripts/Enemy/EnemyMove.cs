using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _squareSize = 7f;
    private float _speed = 2f;

    private Vector3[] _corners;
    private List<int> _currentTargetIndices = new List<int>();
    private List<GameObject> _movingObjects = new List<GameObject>();
    private bool _start = false;

    void Start()
    {
        BallController.OnPlayerDied += StopMove;

        _corners = new Vector3[4];
        _corners[0] = new Vector3(-_squareSize / 2, -_squareSize / 2, 0);
        _corners[1] = new Vector3(_squareSize / 2, -_squareSize / 2, 0);
        _corners[2] = new Vector3(_squareSize / 2, _squareSize / 2, 0);
        _corners[3] = new Vector3(-_squareSize / 2, _squareSize / 2, 0);
        GameObject object1 = Instantiate(_prefab, _corners[0], Quaternion.identity);
        GameObject object2 = Instantiate(_prefab, _corners[2], Quaternion.identity);

        _movingObjects.Add(object1);
        _movingObjects.Add(object2);
        _currentTargetIndices.Add(1);
        _currentTargetIndices.Add(3);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _start = true;
        }

        if (_start == true)
        {
            MoveEnemy();
        }
    }

    private void OnDestroy()
    {
        BallController.OnPlayerDied -= StopMove;
    }

    private void MoveEnemy()
    {
        for (int i = 0; i < _movingObjects.Count; i++)
        {
            GameObject movingObject = _movingObjects[i];
            int currentTargetIndex = _currentTargetIndices[i];
            Vector3 targetPosition = _corners[currentTargetIndex];

            movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, targetPosition, _speed * Time.deltaTime);

            if (Vector3.Distance(movingObject.transform.position, targetPosition) < 0.01f)
            {
                movingObject.transform.position = targetPosition;
                _currentTargetIndices[i] = (currentTargetIndex + 1) % 4;
            }
        }
    }

    public void SpawnNewEnemy()
    {
        int spawnIndex = _movingObjects.Count % 4;
        GameObject newEnemy = Instantiate(_prefab, _corners[spawnIndex], Quaternion.identity);
        _movingObjects.Add(newEnemy);
        _currentTargetIndices.Add((spawnIndex + 1) % 4);
    }

    public List<GameObject> GetEnemies()
    {
        return _movingObjects;
    }

    private void StopMove()
    {
        _speed = 0;
    }
}
