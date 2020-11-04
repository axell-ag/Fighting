using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /*[SerializeField] private Transform _attackPose;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _wahtIsEnemy;
    [SerializeField] private int _damage;
    private float _health;
    private Animator _animator;
    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPose.position, _attackRange, _wahtIsEnemy);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<PlayerController>().TakeDamage(_damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            _animator.SetInteger("StateSamurai", 3);
            print("Нанесли урон игроку");
            Attack();
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
        _health = 90f;
        print(_health);
    }


    public void TakeDamage(int Damage)
    {
        _health -= Damage;
        print("получили урон");
        print(_health);
        if (_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }*/

}
