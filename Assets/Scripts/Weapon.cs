using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            // Проверяем, что сам объект (к которому прикреплен скрипт) имеет тег "weapon"
            if (gameObject.CompareTag("weapon"))
            {
                // Удаляем этот объект (оружие)
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("enemy"))
        {
            // Проверяем, что сам объект (к которому прикреплен скрипт) имеет тег "weapon"
            if (gameObject.CompareTag("weapon"))
            {
                // Удаляем этот объект (оружие)
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _rigidbody2D.angularVelocity = _speed;
    }

    void Update()
    {
    }
}