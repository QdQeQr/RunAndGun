using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject weapon;

    private static readonly int SpeedX = Animator.StringToHash("speedX");
    [SerializeField] private GameObject weaponDummy;

    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;

    [SerializeField] private LayerMask groundMask;

    private Animator _animator;
    private Rigidbody2D _rb;

    private bool _canJump;
    private float _horizontal;
    private int _counter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
            _canJump = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("money"))
        {
            _counter = _counter + 1;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
            _canJump = true;
    }

    protected void OnJump(InputValue value)
    {
        if (_canJump == true)
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnAttack(InputValue value)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject cloneWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        Vector2 myPosition = transform.position;
        Vector2 direction = (mousePosition - myPosition).normalized;
        weaponDummy.SetActive(false);
        
        cloneWeapon.GetComponent<Rigidbody2D>().AddForce(direction * 10, ForceMode2D.Impulse);
    }

    private void OnMove(InputValue value)
    {
        _horizontal = value.Get<Vector2>().x;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_horizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        if (_horizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);

        var velocityY = _rb.linearVelocityY;
        Vector2 newVelocty = transform.right * (speed * _horizontal);
        newVelocty.y = velocityY;

        _rb.linearVelocity = newVelocty;
        var speedX = Math.Abs(speed * _horizontal);
        _animator.SetFloat(SpeedX, speedX);

        var hit = Physics2D.Raycast(transform.position, Vector2.down, 1, groundMask);

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
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}