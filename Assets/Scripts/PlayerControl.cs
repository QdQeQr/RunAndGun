using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    
    [SerializeField] private LayerMask _groundMask;

    private Animator _animator;
    private Rigidbody2D _rb;

    private bool canJump = false;
    private float _horizontal;
    private int _counter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            canJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "money")
        {
            _counter = _counter + 1;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            canJump = true;
        }
    }

    protected void OnJump(InputValue value)
    {
        if (canJump == true)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnMove(InputValue value)
    {
        _horizontal = value.Get<Vector2>().x;
    }

    // Update is called once per frame
    void Update()
    {
        if (_horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (_horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        float velocityY = _rb.linearVelocityY;
        Vector2 newVelocty = transform.right * _speed * _horizontal;
        newVelocty.y = velocityY;
        
        _rb.linearVelocity = newVelocty;
        float speedX = Math.Abs(_speed * _horizontal);
        _animator.SetFloat("speedX", speedX);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1, _groundMask);

        if (hit.collider)
        {
            // Находим угол нормали
            float angle = Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg;
            
            // Сдвигаем угол на -90°, чтобы персонаж "встал" на поверхность
            float finalAngle = angle - 90f;
            
            // Применяем вращение
            transform.rotation = Quaternion.Euler(0, 0, finalAngle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}