using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _attackPose;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _wahtIsEnemy;
    [SerializeField] private float _attackRange;
    [SerializeField] private Text _textHp, _textArmor, _textAttack;
    [SerializeField] private GameObject _effect, _loseScreen;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    [SerializeField] private Main _main;

    private int _damage;
    private float _health;
    private float _armor;
    private float _speed;
    private float _jumpHeight = 12f;
    private bool _isGrounded = true;
    private bool _isAttacking = false;

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

    public void Attack()
    {
        _damage = Random.Range(5, 15);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPose.position, _attackRange, _wahtIsEnemy);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<SamuraiController>().TakeDamage(_damage);
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
        //_main = GetComponent<Main>();
        _damage = Random.Range(5, 15);
        _health = 100f;
        _armor = 50f;
    }
    private void Update()
    {
        GroundCheck();
        Flip();
        _textHp.text = _health.ToString();
        _textArmor.text = _armor.ToString();
        _textAttack.text = _damage.ToString();


    }

    private void FixedUpdate()
    {
        MovePlayer();
        _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
    }

    private void MovePlayer()
    {
        if (_joystick.Horizontal != 0 && !_isAttacking && _health > 0)
        {
            _speed = 10f * _joystick.Horizontal;
            _effect.SetActive(true);
            _animator.SetInteger("StateSwordsman", 2);
        }
        if (_joystick.Horizontal == 0 && !_isAttacking && _health > 0)
        {
            _speed = 0f;
            _effect.SetActive(false);
            _animator.SetInteger("StateSwordsman", 1);
        }
        /*if (_joystick.Vertical >= .9f && _isGrounded && !_isAttacking && _health > 0)
        {
            _rigidbody.velocity = Vector2.up * _jumpHeight;
        }*/
    }

    public void Jump()
    {
        if (_isGrounded && !_isAttacking && _health > 0)
        {
            _rigidbody.velocity = Vector2.up * _jumpHeight;
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
        if (!_isGrounded)
        {
            _animator.SetInteger("StateSwordsman", 3);
        }
    }

    public void TakeDamage(int Damage)
    {
        _animator.SetInteger("StateSwordsman", 4);
        if (_armor > 0)
        {
            Damage /= 2;
            _armor -= Damage;
            _health -= Damage;
        }
        else if (_armor <= 0)
        {
            _armor = 0f;
            _health -= Damage;
        }
        if (_health <= 0)
        {
            _health = 0;
            _animator.SetInteger("StateSwordsman", 6);
            StartCoroutine(PlayerDies());
        }
    }

    private IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(1f);
        _isAttacking = false;
    }

    private IEnumerator PlayerDies()
    {
        yield return new WaitForSeconds(2f);
        _main.GetComponent<Main>().PauseOn();
        _loseScreen.SetActive(true);
        //gameObject.SetActive(false);
    }
}
