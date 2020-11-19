using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] protected Transform _groundCheck, _attackPose, _player;
    [SerializeField] protected GameObject _winScreen, _effect, _loseScreen;
    [SerializeField] protected Text _textHp, _textArmor, _textAttack;
    [SerializeField] protected LayerMask _whatIsGround, _wahtIsEnemy;
    [SerializeField] protected Joystick _joystick;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected Main _main;

    protected Rigidbody2D _rigidbody;
    protected Animator _animator;

    protected int _damage;
    protected float _armor, _speed, _health;
    protected float _jumpHeight = 12f;
    protected bool _isGrounded = true;
    protected bool _isAttacking = true;

    private float _bonusHp = 12f;
    private int _bonusAttack = 5;

    public virtual void MoveCharacter()
    {
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
            JumpEnemy(0);
        }
        else
        {
            JumpEnemy(180);
        }
    }

    protected void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, 0.2f, _whatIsGround);
        _isGrounded = colliders.Length > 1;
    }

    protected void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPose.position, _attackRange, _wahtIsEnemy);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<CharacterController>().TakeDamage(_damage);
        }
    }

    protected void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void JumpEnemy(float rotation)
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(_attackPose.position, Vector2.right, 0.1f, _whatIsGround);
        if (hit.collider != null)
        {
            _rigidbody.velocity = Vector2.up * _jumpHeight;
            _animator.SetInteger("StateSamurai", 4);
        }
        transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    private void TakeDamage(int Damage)
    {
        _animator.SetInteger("StateSwordsman", 4);
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hp")
        {
            _health += _bonusHp;
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.name == "Attack")
        {
            _damage += _bonusAttack;
            collision.gameObject.SetActive(false);
        }
    }

    private IEnumerator DoAttack()
    {
        _isAttacking = false;
        _animator.SetInteger("StateSamurai", 3);
        Attack();
        yield return new WaitForSeconds(1.2f);
        _animator.SetInteger("StateSamurai", 1);
        _isAttacking = true;
    }
}