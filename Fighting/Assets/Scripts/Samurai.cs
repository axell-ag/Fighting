using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Samurai : CharacterController
{
    public void DiesSamurai()
    {
        if (_health <= 0)
        {
            _main.GetComponent<Main>().PauseOn();
            _winScreen.SetActive(true);
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
        _rigidbody = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
        _health = 100f;
        _armor = 20f;
        _speed = 8f;
        _damage = 9;
    }

    private void Update()
    {
        GroundCheck();
        MoveCharacter();
        DiesSamurai();
        _textHp.text = _health.ToString();
        _textArmor.text = _armor.ToString();
        _textAttack.text = _damage.ToString();
    }
}