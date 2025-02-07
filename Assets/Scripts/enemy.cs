using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float raycastDistance; // Дистанция для проверки земли под монстром
    public float moveSpeed; // Скорость движения монстра
    public LayerMask groundLayerMask; // Слой с монстром
    public Transform RaycastTransform;
    private int isFacingRight = 1; // Направление движения монстра

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        CheckGround();
    }

    void Move()
    {
        _rb.linearVelocity = transform.right * (moveSpeed * isFacingRight);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void CheckGround()
    {
        // Луч для проверки земли под монстром
        RaycastHit2D hit = Physics2D.Raycast(RaycastTransform.position, Vector2.down, raycastDistance, groundLayerMask);
        
            // Если луч не попал в землю
        if (hit.collider) 
        { 
            // Находим угол нормали
            var angle = Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg;
            
            // Сдвигаем угол на -90°, чтобы персонаж "встал" на поверхность
            var finalAngle = angle - 90f;

            // Применяем вращение
            transform.rotation = Quaternion.Euler(0, 0, finalAngle);
            }
            else
            {
                Flip();
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
    }

    void Flip()
    {
        // Разворот монстра
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFacingRight *= -1;
    }
}