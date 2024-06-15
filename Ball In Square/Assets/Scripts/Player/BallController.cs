using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _ballSpeed = 5f;
    private Vector3 _direction; // Направление движения мяча
    private bool _isMoving = false; // Флаг для определения, двигается ли мяч
    [SerializeField] private ArrowController _arrowController; // Ссылка на компонент прицела

    void Start()
    {
        _arrowController = FindObjectOfType<ArrowController>(); // Получаем ссылку на компонент прицела
    }

    void Update()
    {
        // Логика запуска мяча по траектории от текущей позиции прицела
        if (Input.GetMouseButtonDown(0) && !_isMoving)
        {
            Vector3 currentAimPosition = _arrowController.GetInitialAimPosition(); // Получаем текущую позицию прицела
            _direction = (currentAimPosition - transform.position).normalized;
            _isMoving = true;
        }

        // Перемещение мяча, если он движется
        if (_isMoving)
        {
            MoveBall();
        }
    }

    // Метод для перемещения мяча с постоянной скоростью
    private void MoveBall()
    {
        transform.position += _direction * _ballSpeed * Time.deltaTime;
    }
}

