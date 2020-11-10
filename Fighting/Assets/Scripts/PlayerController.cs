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
    [SerializeField] private Text _textHp;
    [SerializeField] private GameObject _effect;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private int _damage;
    [SerializeField] private float _health;
    private float _armor;
    private float _speed;
    private float _jumpHeight = 12f;
    private bool _isGrounded = true;
    private bool _isAttacking = false;
    private Vector2 moveVelocity;

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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPose.position, _attackRange, _wahtIsEnemy);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<SamuraiController>().TakeDamage(_damage);
        }
    }




    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(_effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }*/
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPose.position, _attackRange);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = Random.Range(5, 15);
        _health = 100f;
    }
    private void Update()
    {
        GroundCheck();
        Flip();
        _textHp.text = _health.ToString();
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
            _speed = 7f * _joystick.Horizontal;
            _effect.SetActive(true);
            _animator.SetInteger("StateSwordsman", 2);
        }
        if (_joystick.Horizontal == 0 && !_isAttacking && _health > 0)
        {
            _speed = 0f;
            _effect.SetActive(false);
            _animator.SetInteger("StateSwordsman", 1);
        }
        if (_joystick.Vertical >= .9f && _isGrounded && !_isAttacking && _health > 0)
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
        _health -= Damage;
        _animator.SetInteger("StateSwordsman", 4);
        print(_health);
        if (_health <= 0)
        {
            _animator.SetInteger("StateSwordsman", 6);
            StartCoroutine(PlayerDies());
            //gameObject.SetActive(false);
        }
    }

    private IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(.6f);
        _isAttacking = false;
    }

    private IEnumerator PlayerDies()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
