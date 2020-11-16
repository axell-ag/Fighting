using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private GameObject _hp;
    private bool isBonus = false;
    private float _waitTime;
    void Start()
    {
        _waitTime = Random.Range(1, 20);
        StartCoroutine(EnableBonus());
    }

    IEnumerator EnableBonus()
    {
        yield return new WaitForSeconds(_waitTime);
        _hp.SetActive(true);
        isBonus = true;
    }
}
