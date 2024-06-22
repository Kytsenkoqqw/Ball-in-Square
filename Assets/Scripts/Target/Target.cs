using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private ScoreManager scoreManager;
    private float squareSize = 8f; 
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Respawn();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void Respawn()
    {
        float boundary = squareSize / 2f;
        float randomX = Random.Range(-boundary, boundary);
        float randomY = Random.Range(-boundary, boundary);
        transform.position = new Vector2(randomX, randomY);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.Play();
            scoreManager.AddScore(1);
            Respawn();
        }
    }
}
