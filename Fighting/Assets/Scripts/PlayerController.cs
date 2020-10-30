using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _speed = 6f;
    private float _jumpHeight = 10f;
    private bool _isGrounded = true;
    private bool _isAttacking = false;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private GameObject _sword;

    protected float _health;
    protected float _armor;
    protected float _damage;


    public void OnAttackButtonDown()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _animator.SetInteger("State", 7);
            StartCoroutine(DoAttack());
        }

    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sword.SetActive(false);
        _health = 100f;
        _damage = 7f;
    }
    private void Update()
    {
        GroundCheck();
        Flip();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (_joystick.Horizontal != 0 && !_isAttacking)
        {
            _rigidbody.velocity = new Vector2(_joystick.Horizontal * _speed, _rigidbody.velocity.y);
            _animator.SetInteger("State", 2);
        }
        else if (_joystick.Horizontal == 0 && !_isAttacking)
        {
            _animator.SetInteger("State", 1);
        }

        if (_joystick.Vertical >= .9f && _isGrounded && !_isAttacking)
        {
            _rigidbody.velocity = Vector2.up * _jumpHeight;
            //_rigidbody.AddForce(transform.up * _jumpHeight, ForceMode2D.Impulse);
            // _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _jumpHeight);
            _animator.SetInteger("State", 3);
        }
    }

    private void Flip()
    {
        if (_joystick.Horizontal < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (_joystick.Horizontal > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, 0.2f, _whatIsGround);
        _isGrounded = colliders.Length > 1;
    }

    private IEnumerator DoAttack()
    {
        _sword.SetActive(true);
        yield return new WaitForSeconds(.4f);
        _sword.SetActive(false);
        _isAttacking = false;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sword")
        {
            _health -= _damage;
            print(_health);

            if (_health < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    protected void TakeDamage(float damage)
    {

    }
}
