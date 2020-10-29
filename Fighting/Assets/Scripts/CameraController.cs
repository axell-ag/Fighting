using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _speed = 3f;
    [SerializeField] private Transform _target;

    private void Start()
    {
        transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z);
    }

    private void Update()
    {
        Vector3 position = _target.position;
        position.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, position, _speed * Time.deltaTime); ;
    }
}
