using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _enemyDetect;
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

    /*public void OnAttackButtonDown()
    {
        Attack();
    }*/

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
    }
}
