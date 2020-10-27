using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed = 6f;
    private float _jumpHeight = 10f;
    private Animator animator;
    private Rigidbody2D rigidbody;

    [SerializeField] private GameObject GroundCheck;
    [SerializeField] private Joystick Joystick;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        rigidbody.velocity = new Vector2(Joystick.Horizontal * _speed, rigidbody.velocity.y);
        if (Joystick.Vertical >= .5f)
        {
            /*rigidbody.AddForce(transform.up * _jumpHeight, ForceMode2D.Impulse);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, _jumpHeight);*/
            rigidbody.velocity = Vector2.up * _jumpHeight;
        }
    }
}
