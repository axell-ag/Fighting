using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _speed = 6f;
    private float _jumpHeight = 7f;
    private bool _isGrounded = true;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private LayerMask _whatIsGround;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, 0.2f, _whatIsGround);
        _isGrounded = colliders.Length > 1;
        if (_joystick.Vertical >= .5f && _isGrounded)
        {
            _rigidbody.velocity = Vector2.up * _jumpHeight;
        }
        if (_joystick.Horizontal > 0)
        {

        }
        _rigidbody.velocity = new Vector2(_joystick.Horizontal * _speed, _rigidbody.velocity.y);
    }


}
