using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamuraiController : CharacterController
{
    /*[SerializeField] private Transform _groundCheck, _attackPose, _player;
    [SerializeField] private LayerMask _whatIsGround, _wahtIsEnemy;
    [SerializeField] private float _attackRange;
    [SerializeField] private Text _textHp, _textArmor, _textAttack;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private Main _main;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private float _health;
    private int _damage;
    private float _armor;
    private float _speed = 9f;
    private float _jumpHeight = 12f;
    private bool _isGrounded = true;
    private bool _isAttacking = true;
    private bool _aggressive = false;

    [SerializeField] private Transform _hp;
    [SerializeField] private GameObject _bonus;
    private bool isBonus = false;
    private float _waitTime;*/

    /*public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPose.position, _attackRange, _wahtIsEnemy);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<PlayerController>().TakeDamage(_damage);
        }
    }*/

    /*public void TakeDamage(int Damage)
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
        if (_health <= 10 && !_aggressive)
        {
            _health = 20f;
            _speed = 8f;
            _armor = 10f;
            _aggressive = true;
        }
        if (_health <= 0)
        {
            _health = 0;
            _animator.SetInteger("StateSamurai", 6);
            StartCoroutine(SumaraiDies());
        }
    }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPose.position, _attackRange);
    }

    private new void Start()
    {
        this.GetComponent<SamuraiController>()._health = 10f;
        this.GetComponent<SamuraiController>()._armor = 0f;
        _speed = 9f;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
        this.GetComponent<SamuraiController>()._damage = 10;

        /*_waitTime = Random.Range(1, 20);
        StartCoroutine(EnableBonus());*/
    }

    private void Update()
    {
        GroundCheck();
        Angry();
        DiesSamurai();
        _textHp.text = _health.ToString();
        _textArmor.text = _armor.ToString();
        _textAttack.text = _damage.ToString();
    }

    /*private void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, 0.2f, _whatIsGround);
        _isGrounded = colliders.Length > 1;
    }*/

    /*private IEnumerator DoAttack()
    {
        _isAttacking = false;
        _animator.SetInteger("StateSamurai", 3);
        Attack();
        yield return new WaitForSeconds(1.2f);
        _animator.SetInteger("StateSamurai", 1);
        _isAttacking = true;
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hp")
        {
            _health += 10f;
            collision.gameObject.SetActive(false);
        }
    }

    /*private void Angry()
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

    }*/

    public void DiesSamurai()
    {
        if (_health <= 0)
        {
            _main.GetComponent<Main>().PauseOn();
            _winScreen.SetActive(true);
        }
    }
    /*private IEnumerator SumuraiDies()
    {
        //_animator.SetInteger("StateSamurai", 1);
        yield return new WaitForSeconds(1f);
        //_main.GetComponent<Main>
        _main.GetComponent<Main>().PauseOn();
        _winScreen.SetActive(true);
        //gameObject.SetActive(false);
    }*/

    /*IEnumerator EnableBonus()
    {
        yield return new WaitForSeconds(_waitTime);
        _bonus.SetActive(true);
        isBonus = true;
    }*/
}