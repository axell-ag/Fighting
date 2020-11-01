using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float Health;
    public float Armor;
    public float Damage;

    protected void Start()
    {
        if (Health <= 0f)
        {
            Health = 100f;
        }
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
