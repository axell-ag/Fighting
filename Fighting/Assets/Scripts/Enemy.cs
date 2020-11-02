using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float Health;
   
    private void Start()
    {
        Health = 90f;
        print(Health);
       
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
