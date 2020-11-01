using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HealthController
{
    private PlayerAttack _attack;

    private new void Start()
    {
        base.Start();
        Health = 90f;
             
    }
    private void Update()
    {
        
    }
}
