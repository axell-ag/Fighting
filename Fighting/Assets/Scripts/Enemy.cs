using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterController
{
    private new void Start()
    {
        _health = 90f;
        print(_health);
    }


    public new void TakeDamage(int Damage)
    {
        _health -= Damage;
        print("получили урон");
        print(_health);
        if (_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
