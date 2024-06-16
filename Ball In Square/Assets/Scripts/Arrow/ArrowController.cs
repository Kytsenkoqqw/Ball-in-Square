using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private Transform _targetObject;

    [SerializeField] private float _maxDistance = 0.3f;
    [SerializeField] private float _rotationSpeed = 80f;

    private float MaxRotationAngle => 60f + _angleDifference;
    private float MinRotationAngle => -60f + _angleDifference;
    private float _angleDifference = 0f;
    private float _currentRotationAngle = 0f;

    private bool _rotatingRight = true;

    private Vector3 _initialAimPosition; // Переменная для хранения начальной позиции прицела

    void Update()
    {
        Rotation();

        // Обработка нажатия кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            _initialAimPosition = transform.position; // Запоминаем текущее положение прицела
        }
    }

    private void Rotation()
    {
        float rotationDelta = _rotationSpeed * Time.deltaTime;
        if (_rotatingRight)
        {
            _currentRotationAngle += rotationDelta;
            if (_currentRotationAngle >= MaxRotationAngle)
            {
                _currentRotationAngle = MaxRotationAngle;
                _rotatingRight = false;
            }
        }
        else
        {
            _currentRotationAngle -= rotationDelta;
            if (_currentRotationAngle <= MinRotationAngle)
            {
                _currentRotationAngle = MinRotationAngle;
                _rotatingRight = true;
            }
        }

        Vector3 offset = new Vector3(Mathf.Sin(_currentRotationAngle * Mathf.Deg2Rad), Mathf.Cos(_currentRotationAngle * Mathf.Deg2Rad), 0) * _maxDistance;
        transform.position = _targetObject.position + offset;

        transform.up = transform.position - _targetObject.position;
    }

    public Vector3 GetInitialAimPosition()
    {
        return _initialAimPosition;
    }
}
