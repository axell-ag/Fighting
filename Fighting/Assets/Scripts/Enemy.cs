using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayerController
{
    private void Start()
    {
        _health = 100f;
        _damage = 10f;
    }

    private void Update()
    {
        print(_health);
        TakeDamage(7f);

    }
}
