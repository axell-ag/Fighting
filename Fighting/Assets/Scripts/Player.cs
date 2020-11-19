using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharacterController
{
    public void OnAttackButtonDown()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            Attack();
            _animator.SetInteger("StateSwordsman", 5);
            StartCoroutine(DoAttack());
        }
    }

    public override void MoveCharacter()
    {
        if (_joystick.Horizontal != 0 && !_isAttacking)
        {
            Move(10f, true, 2);
        }
        else if (_joystick.Horizontal == 0 && !_isAttacking)
        {
            Move(0f, false, 1);
        }
        if (!_isGrounded)
        {
            _animator.SetInteger("StateSwordsman", 3);
        }

        void Move(float speed, bool effect, int animationNumber)
        {
            _speed = speed * _joystick.Horizontal;
            _effect.SetActive(effect);
            _animator.SetInteger("StateSwordsman", animationNumber);
        }
    }
    public void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody.velocity = Vector2.up * _jumpHeight;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPose.position, _attackRange);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = 7;
        _health = 100f;
        _armor = 50f;
        _isAttacking = false;
    }

    private void Update()
    {
        GroundCheck();
        Flip();
        Dies();
        _textHp.text = _health.ToString();
        _textArmor.text = _armor.ToString();
        _textAttack.text = _damage.ToString();

    }

    private void FixedUpdate()
    {
        MoveCharacter();
        _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
    }

    private void Flip()
    {
        Quaternion flip = _joystick.Horizontal < 0 ? transform.localRotation = Quaternion.Euler(0, 180, 0) : transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void Dies()
    {
        if (_health <= 0)
        {
            _main.GetComponent<Main>().PauseOn();
            _loseScreen.SetActive(true);
        }
    }

    private IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(1f);
        _isAttacking = false;
    }
}