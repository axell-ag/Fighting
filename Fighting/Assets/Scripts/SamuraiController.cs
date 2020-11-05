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

    [SerializeField] private Transform _player;
    private bool _chill = false;
    private bool _angry = false;
    private bool _goBack = false;

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
        _speed = 5f;
        _jumpHeight = 12f;
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
        print("Нанесли урон игроку");
        Attack();
        yield return new WaitForSeconds(1f);
        _animator.SetInteger("StateSamurai", 1);
        _isAttacking = true;
        print(_isAttacking);
    }

    private void Angry()
    {
        //transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
        /*if (Vector2.Distance(transform.position, _player.position) < 2f)
        {
            // Swap the position of the cylinder.
            //transform.position = new Vector2(transform.position.x - .5f, transform.position.y);
            //transform.position = transform.position;
            _animator.SetInteger("StateSamurai", 3);
        }*/
        if (Vector2.Distance(transform.position, _player.position) > 2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
            _animator.SetInteger("StateSamurai", 2);
        }


        if (transform.position.x < _player.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.position.x > _player.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 5f, ForceMode2D.Impulse);
        }
    }*/
}