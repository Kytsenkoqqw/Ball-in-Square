using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BallController : MonoBehaviour
{
    public static Action OnPlayerDied;

    [SerializeField] private Transform _arrowTransform;
    [SerializeField] private float _moveSpeed = 15f;
    [SerializeField] private GameObject _aimArrow;
    [SerializeField] private ScoreManager _scoreManager;
    private AudioSource _audioSource;
    
    private Vector3 _moveDirection;
    private Vector3 _currentPosition;
    
    private bool _isMove = false;
    
    private Rigidbody2D _rb;
    
    private ArrowController _arrowRotationScript;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        _arrowRotationScript = _arrowTransform.GetComponent<ArrowController>();
        _currentPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _rb.velocity == Vector2.zero)
        {
            _audioSource.Play();
            _aimArrow.SetActive(false);
            _isMove = true;
            _moveDirection = (_arrowTransform.position - transform.position).normalized;
        }
        MoveBall();
    }

    

    private void MoveBall()
    {
        if(_isMove == true)
        {
            _rb.velocity = _moveDirection * _moveSpeed;
        }
        else
        {
            _isMove = false;
        }

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.layer == LayerMask.NameToLayer("Barrier"))
        {
            _aimArrow.SetActive(true);
            _rb.velocity = Vector2.zero;
            _isMove = false;
            
            Vector2 collisionPoint = collision.GetContact(0).point;
            var angle = GetAngle(_currentPosition, collision.collider.transform.position);
            _currentPosition = collision.collider.transform.position;
            _arrowRotationScript.OnBallCollision(collisionPoint, angle);
        }
        else if (collision.gameObject.GetComponent<CircleCollider2D>())
        {
            OnPlayerDied?.Invoke();
            Destroy(gameObject);
            _scoreManager.GameOver();
        }
    }
    
    private float GetAngle(Vector2 from, Vector2 to)
    {
        return Vector2.SignedAngle(from, to);
    }
}

