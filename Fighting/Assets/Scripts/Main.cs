using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private SamuraiController _samuraiController;
    public float _timeScale;
    public void ReloadLevel()
    {
        _timeScale = 1f;
        Time.timeScale = 1f;
        _playerController.enabled = true;
        _samuraiController.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PauseOn ()
    {
        _timeScale = 0f;
        Time.timeScale = 0f;
        _playerController.enabled = false;
        _samuraiController.enabled = false;
    }
    public void PauseOff()
    {
        _timeScale = 1f;
        Time.timeScale = 1f;
        _playerController.enabled = true;
        _samuraiController.enabled = true;
    }
}
