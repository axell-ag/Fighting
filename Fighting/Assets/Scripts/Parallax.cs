using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _lenght, _startPosition;

    [SerializeField] private GameObject _camera;
    [SerializeField] private float _parallaxEffect;

    void Start()
    {
        _startPosition = transform.position.x;
        _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }


    void Update()
    {
        float temp = _camera.transform.position.x * (1 - _parallaxEffect);
        float dist = _camera.transform.position.x * _parallaxEffect;

        transform.position = new Vector3(_startPosition + dist, transform.position.y, transform.position.z);

        if (temp > _startPosition + _lenght)
            _startPosition += _lenght;
        else if (temp < _startPosition - _lenght)
            _startPosition -= _lenght;
    }
}
