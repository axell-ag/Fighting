using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /*public float Health;
    public float Armor;
    public float Damage;*/
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    public PlayerAttack _attack;
    public float Health;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _groundCheck;


    public void OnAttackButtonDown()
    {
        
            _attack.Attack();

    }
    private void Start()
    {
        Health = 90f;
        print(Health);
        _attack = GetComponent<PlayerAttack>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        print("получили урон");
        print(Health);
        if (Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
