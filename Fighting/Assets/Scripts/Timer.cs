using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text _textTimer;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private Main _main;
    private float _time = 60f;
    void Start()
    {
        _textTimer.text = _time.ToString();
        _main = GetComponent<Main>();
        _main._timeScale = 1f;
    }

   
    public void Update()
    {
        //_time -= Time.deltaTime;
        _textTimer.text =  Mathf.Round(_time).ToString();
        if (_main._timeScale == 1)
        {
            _time -= Time.deltaTime;
        }
        if (_time <= 0f)
        {
            _loseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
