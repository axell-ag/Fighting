using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private LayerMask _whatIsGround;

    /*private float _health;
    private float _armor;
    private float _damage;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _speed;
    private float _jumpHeight;
    private bool _isGrounded = true;
    private bool _isAttacking = false;*/

    [SerializeField] private CharacterAttack _characterAttack;

    public void OnAttackButtonDown()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _characterAttack = new CharacterAttack { };
            _characterAttack.Attack();
            _animator.SetInteger("State", 1);
            StartCoroutine(DoAttack());   
        }
    }

    private new void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = 100f;
        _speed = 8f;
        _jumpHeight = 10f;
        _isAttacking = false;
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
            _animator.SetInteger("State", 7);
        }
        else if (_joystick.Horizontal == 0 && !_isAttacking)
        {
            _animator.SetInteger("State", 5);
        }

        if (_joystick.Vertical >= .9f && _isGrounded && !_isAttacking)
        {
            _rigidbody.velocity = Vector2.up * _jumpHeight;
            _animator.SetInteger("State", 6);
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
        yield return new WaitForSeconds(.4f);
        _isAttacking = false;
    }
}
