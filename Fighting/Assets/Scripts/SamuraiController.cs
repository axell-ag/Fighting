using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamuraiController : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _attackPose;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _wahtIsEnemy;
    [SerializeField] private float _attackRange;
    [SerializeField] private Text _textHp, _textArmor, _textAttack;

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
        _damage = Random.Range(7, 20);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPose.position, _attackRange, _wahtIsEnemy);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<PlayerController>().TakeDamage(_damage);
        }
    }

    public void TakeDamage(int Damage)
    {
        _animator.SetInteger("StateSamurai", 5);
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
        if (_health <= 10)
        {
            _health = 20f;
            _speed = 8f;
            _armor = 10f;
        }
        if (_health <= 0)
        {
            _health = 0;
            _animator.SetInteger("StateSamurai", 6);
            StartCoroutine(SumaraiDies());
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
        _armor = 20f;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
        _damage = Random.Range(7, 20);
    }

    private void Update()
    {
        GroundCheck();
        Angry();
        _textHp.text = _health.ToString();
        _textArmor.text = _armor.ToString();
        _textAttack.text = _damage.ToString();
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
        yield return new WaitForSeconds(.7f);
        _animator.SetInteger("StateSamurai", 1);
        _isAttacking = true;
    }

    private void Angry()
    {
        RaycastHit2D hit;

        if (Vector2.Distance(transform.position, _player.position) <= 1.7f && _isAttacking)
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
                _animator.SetInteger("StateSamurai", 4);
            }
        }
        else if (transform.position.x > _player.position.x)
        {
            hit = Physics2D.Raycast(_attackPose.position, Vector2.left, 0.1f, _whatIsGround);
            if (hit.collider != null)
            {
                _rigidbody.velocity = Vector2.up * _jumpHeight;
                _animator.SetInteger("StateSamurai", 4);
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