using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] private float _damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.TryGetComponent(out EnemyController health))
        {
            health.ApplyDamage(_damage);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame

    void Update()
    {
    }
}