using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiController : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _attackPose;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _wahtIsEnemy;
    [SerializeField] private float _attackRange;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    [SerializeField] private float _health;
    private int _damage;
    private float _armor;
    private float _speed = 5f;
    private float _jumpHeight = 12f;
    private bool _isGrounded = true;
    private bool _isAttacking = true;

    [SerializeField] private Transform _player;

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPose.position, _attackRange, _wahtIsEnemy);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<PlayerController>().TakeDamage(_damage);
        }
    }

    public void TakeDamage(int Damage)
    {
        _health -= Damage;
        _animator.SetInteger("StateSamurai", 5);
        print(_health);
        if (_health <= 0)
        {
            _animator.SetInteger("StateSamurai", 6);
            StartCoroutine(SumaraiDies());
            //gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPose.position, _attackRange);
    }

    private void Start()
    {
        _health = 100f;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
        _damage = Random.Range(5, 15);
    }

    private void Update()
    {
        GroundCheck();
        Angry();
    }

    private void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, 0.2f, _whatIsGround);
        _isGrounded = colliders.Length > 1;
    }

    private IEnumerator DoAttack()
    {
        _isAttacking = false;
        _animator.SetInteger("StateSamurai", 3);
        Attack();
        yield return new WaitForSeconds(2f);
        _animator.SetInteger("StateSamurai", 1);
        _isAttacking = true;
    }

    private void Angry()
    {
        RaycastHit2D hit;

        if (Vector2.Distance(transform.position, _player.position) < 1.7f && _isAttacking)
        {
            StartCoroutine(DoAttack());
        }
        if (Vector2.Distance(transform.position, _player.position) > 1.7f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
            _animator.SetInteger("StateSamurai", 2);
        }

        if (transform.position.x < _player.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            hit = Physics2D.Raycast(_attackPose.position, Vector2.right, 0.1f, _whatIsGround);
            if (hit.collider != null)
            {
                _rigidbody.velocity = Vector2.up * _jumpHeight;
            }
        }
        else if (transform.position.x > _player.position.x)
        {
            hit = Physics2D.Raycast(_attackPose.position, Vector2.left, 0.1f, _whatIsGround);
            if (hit.collider != null)
            {
                _rigidbody.velocity = Vector2.up * _jumpHeight;
            }
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

    }

    private IEnumerator SumaraiDies()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}