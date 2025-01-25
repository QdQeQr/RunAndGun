using System;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private Collider2D _groundChecker;
    [SerializeField] private float _enemySpeed;

    private Vector2 _direction = Vector2.right;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * (Time.deltaTime * _enemySpeed));
    }

    private void FixedUpdate()
    {
        
        List<Collider2D> colliders = new();
        _groundChecker.Overlap(colliders);

        bool isGround = false;
        
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "ground")
            {
                isGround = true;
                break;
            }
        }

        if (isGround == false)
        {
            _direction *= -1;
        }
    }
}
