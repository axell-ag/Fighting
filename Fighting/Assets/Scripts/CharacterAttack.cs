using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPose;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _wahtIsEnemy;
    [SerializeField] private int _damage;

    public void Attack ()
    {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPose.position, _attackRange, _wahtIsEnemy);
            for (int i = 0; i < colliders.Length; i++)
            {
                //colliders[i].GetComponent<Enemy>().TakeDamage(_damage);
            }
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPose.position, _attackRange);
    }*/
}
