using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text _textTimer;
    private float _timer = 60f;
    void Start()
    {
        _textTimer.text = _timer.ToString();
    }

   
    void Update()
    {
        _timer -= Time.deltaTime;
        _textTimer.text =  Mathf.Round(_timer).ToString();
    }
}
