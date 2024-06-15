using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _ballSpeed = 5f;
    private Vector2 _direction; // Направление движения мяча
    private bool _isMoving = false; // Флаг для определения, двигается ли мяч
    [SerializeField] private ArrowController _arrowController; // Ссылка на компонент прицела
    private Rigidbody2D _rigidbody;

    void Start()
    {
        _arrowController = FindObjectOfType<ArrowController>(); // Получаем ссылку на компонент прицела
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = true; // Включаем кинематику, чтобы мы могли управлять движением вручную
        _rigidbody.gravityScale = 0; // Отключаем гравитацию для кинематического объекта
    }

    void Update()
    {
        // Логика запуска мяча по траектории от текущей позиции прицела
        if (Input.GetMouseButtonDown(0) && !_isMoving)
        {
            Vector3 currentAimPosition = _arrowController.GetInitialAimPosition(); // Получаем текущую позицию прицела
            _direction = ((Vector2)currentAimPosition - _rigidbody.position).normalized;
            _isMoving = true;
            _rigidbody.isKinematic = false; // Выключаем кинематику, чтобы Rigidbody2D реагировал на физику
            _rigidbody.velocity = _direction * _ballSpeed; // Устанавливаем скорость мяча
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Barrier"))
        {
            _isMoving = false;
            _ballSpeed = 0f;
            _rigidbody.velocity = Vector2.zero; // Обнуляем скорость мяча
            _rigidbody.isKinematic = true; // Включаем кинематику, чтобы мяч перестал реагировать на физику
            Debug.Log("Ball collided with barrier.");
        }
    }
}

