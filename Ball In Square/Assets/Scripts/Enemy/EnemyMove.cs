using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 3f; // Скорость движения врага

    private Vector2[] movementDirections = { Vector2.right, Vector2.down, Vector2.left, Vector2.up }; // Направления движения по граням квадрата
    private int currentDirectionIndex = 0; // Индекс текущего направления движения

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Отключаем гравитацию для врага-мяча
        MoveInCurrentDirection();
    }

    private void Update()
    {
        // Проверяем, достиг ли враг конца текущего отрезка (грани квадрата)
        if (HasReachedEndOfCurrentDirection())
        {
            // Если достиг, переключаемся на следующее направление движения
            currentDirectionIndex = (currentDirectionIndex + 1) % movementDirections.Length;
            MoveInCurrentDirection();
        }
    }

    private void MoveInCurrentDirection()
    {
        // Начать движение в текущем направлении
        Vector2 direction = movementDirections[currentDirectionIndex];
        rb.velocity = direction * moveSpeed;
    }

    private bool HasReachedEndOfCurrentDirection()
    {
        // Проверяем, достиг ли враг конца текущего отрезка (грани квадрата)
        Vector2 direction = movementDirections[currentDirectionIndex];
        Vector2 currentPosition = rb.position;
        Vector2 nextPosition = currentPosition + direction * moveSpeed * Time.deltaTime;

        // Если следующая позиция уже за гранью квадрата, вернуть true
        if (!IsPositionWithinSquare(nextPosition))
        {
            return true;
        }

        return false;
    }

    private bool IsPositionWithinSquare(Vector2 position)
    {
        // Проверяем, находится ли позиция внутри квадрата (по X и Y от -4 до 4, например)
        float squareSize = 8f; // Размер квадрата, в который ограничены движения
        float boundary = squareSize / 2f;

        if (Mathf.Abs(position.x) > boundary || Mathf.Abs(position.y) > boundary)
        {
            return false;
        }

        return true;
    }
}
