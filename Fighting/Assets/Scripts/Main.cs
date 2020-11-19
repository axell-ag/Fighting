using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Samurai _samurai;
    [SerializeField] private GameObject _hp, _attack;
    public float _timeScale;

    public void ReloadLevel()
    {
        Pause(1f, true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PauseOn()
    {
        Pause(0f, false);  
    }
    public void PauseOff()
    {
        Pause(1f, true);
    }
    private void Pause(float timeScale, bool character)
    {
        _timeScale = timeScale;
        Time.timeScale = timeScale;
        _player.enabled = character;
        _samurai.enabled = character;
    }
    private void Start()
    {
        StartCoroutine(StartBonus(_hp, 17f));
        StartCoroutine(StartBonus(_attack, 50f));
    }
    private IEnumerator StartBonus(GameObject bonus, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        bonus.SetActive(true);
    }
}
