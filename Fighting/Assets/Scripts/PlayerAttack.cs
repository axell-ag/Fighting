using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _enemyDetect;
    public float Health;
    public float Armor;
    public int _damage = 7;
    [SerializeField] private GameObject _destroy;



    private void Update()
    {
        
    }

    public void OnAttackButtonDown()
    {
        Attack();
    }

    public void Attack()
    {
        RaycastHit2D enemyCheck = Physics2D.Raycast(_enemyDetect.position, Vector2.left, .1f);

        if (enemyCheck.collider)
        {
            HealthController Health = GetComponent<HealthController>();
            Enemy enemy = GetComponent<Enemy>();
            print("нанесли урон");
            //Health -= Damage;
            //print(Health);
            if (Health != null)
            {
                Health.TakeDamage(_damage);
                //return;
            }
        }
    }
}
