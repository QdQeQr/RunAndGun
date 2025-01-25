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

    // Update is called once per frame
    private void FixedUpdate()
    {
        _rigidbody2D.angularVelocity = _speed;
    }

    void Update()
    {
    }
}