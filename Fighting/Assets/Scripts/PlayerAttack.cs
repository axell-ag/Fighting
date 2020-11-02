using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /*[SerializeField] private Transform _enemyDetect;
    //public float Health;
    //public float Armor;
    public int _damage = 7;
    //Enemy enemy;
    //PlayerController PlayerController;


    private void Start()
    {
        //enemy = gameObject.GetComponent<Enemy>();
        //PlayerController = gameObject.GetComponent<PlayerController>();
    }
    private void Update()
    {
        
    }

    *//*public void OnAttackButtonDown()
    {
        Attack();
    }*//*

    public void Attack() 
    {
        RaycastHit2D enemyCheck = Physics2D.Raycast(_enemyDetect.position, Vector2.left, .1f);

        if (enemyCheck.collider)
        {
            Enemy enemy = new Enemy() { Health = 100f };
            //Enemy enemy = gameObject.GetComponent<Enemy>();
                //TestControler testControler = gameObject.GetComponent<TestControler>();
            print("нанесли уронa");
            //Health -= Damage;
            //print(Health);
            //print(PlayerEnemy);
            //print(enemy.Health);
            //print(Health);
            if (enemy.Health > 0)
            {
                print("получили урон");
                enemy.TakeDamage(_damage);
                return;
            }
        }
    }*/


    [SerializeField] private Transform _attackPose;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _wahtIsEnemy;
    [SerializeField] private int _damage;

    public void Attack ()
    {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPose.position, _attackRange, _wahtIsEnemy);
            for (int i = 0; i < colliders.Length; i++)
            {
                print("итерация");
                print(i);
                colliders[i].GetComponent<Enemy>().TakeDamage(_damage);
            }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPose.position, _attackRange);
    }
}
