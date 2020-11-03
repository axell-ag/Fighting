using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiController : CharacterController
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private CharacterAttack _characterAttack;
    public void OnAttackButtonDown()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _characterAttack.Attack();
            _animator.SetInteger("StateSamurai", 3);
            StartCoroutine(DoAttack());
        }
    }

    private new void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = 100f;
        _speed = 10f;
        _jumpHeight = 12f;
        _isAttacking = false;
    }

    private void Update()
    {
        GroundCheck();
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
