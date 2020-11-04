using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiController : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] private Transform _attackPose;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _wahtIsEnemy;
    [SerializeField] private int _damage;
    [SerializeField] private int _positionOfPatrol;
    [SerializeField] private Transform _point;
    [SerializeField] private float _stoppingDistance;
    private Rigidbody2D _rigidbody;
    private float _health;
    private Animator _animator;

    private float _armor;
    private float _speed;
    private float _jumpHeight;
    private bool _isGrounded = true;
    private bool _isAttacking = true;

    private bool _moveRight;
    [SerializeField] private Transform _player;
    private bool _chill = false;
    private bool _angry = false;
    private bool _goBack = false;

    /*public void OnAttackButtonDown()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _characterAttack.Attack();
            _animator.SetInteger("StateSamurai", 3);
            StartCoroutine(DoAttack());
        }
    }*/

    public void Attack()
    {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPose.position, _attackRange, _wahtIsEnemy);
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].GetComponent<PlayerController>().TakeDamage(_damage);
            }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && _isAttacking)
        {
            /*_animator.SetInteger("StateSamurai", 3);
            print("Нанесли урон игроку");
            Attack();*/
            StartCoroutine(DoAttack());
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
        _health = 100f;
        _speed = 10f;
        _jumpHeight = 12f;
        //_player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        GroundCheck();
        
        if (Vector2.Distance(transform.position, _point.position) < _positionOfPatrol && _angry)
        {
            _chill = true;
        }

        if (Vector2.Distance(transform.position, _player.position) < _stoppingDistance)
        {
            _angry = true;
            _chill = false;
            _goBack = false;
        }
        if (Vector2.Distance(transform.position, _player.position) > _stoppingDistance)
        {
            _goBack = true;
            _angry = false;
        }

        if (_chill)
        {
            Chill();
        }
        if (_angry)
        {
            Angry();
        }
        if (_goBack)
        {
            GoBack();
        }
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
        print("Нанесли урон игроку");
        Attack();
        yield return new WaitForSeconds(1f);
        /*_animator.SetInteger("StateSamurai", 3);
        print("Нанесли урон игроку");
        Attack();*/
        _animator.SetInteger("StateSamurai", 1);
        _isAttacking = true;
        print(_isAttacking);
    }

    private void Chill()
    {
        if (transform.position.x > _point.position.x + _positionOfPatrol)
        {
            _moveRight = true;
        }
        else if (transform.position.x < _point.position.x - _positionOfPatrol)
        {
            _moveRight = false;
        }

        if (_moveRight)
        {
            transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime, transform.position.y);
        }
    }

    private void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }

    private void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, _point.position, _speed * Time.deltaTime);
    }
}
