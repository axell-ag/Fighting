using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected float _health;
    protected float _armor;
    protected float _damage;
    protected Animator _animator;
    protected Rigidbody2D _rigidbody;
    protected float _speed;
    protected float _jumpHeight;
    protected bool _isGrounded = true;
    protected bool _isAttacking = false;

    protected void Start()
    {

    }    
    public void TakeDamage(int Damage)
    {
        _health -= Damage;
        if (_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
