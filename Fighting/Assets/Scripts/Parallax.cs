using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    /*[SerializeField] private float _speed;
    [SerializeField] private float _endX;
    [SerializeField] private float _startX;

    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);

        if (transform.position.x <= _endX)
        {
            Vector2 pos = new Vector2(_startX, transform.position.y);
            transform.position = pos;
        }
    }*/

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
