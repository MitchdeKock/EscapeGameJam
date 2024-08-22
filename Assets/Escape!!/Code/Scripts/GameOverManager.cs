using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshPro finalScore;

    private CoreHealthHandler _coreHealth;
    private void Start()
    {
        _coreHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();
        _coreHealth.OnHealthValueChanged += isGameOver;
    }
    public void isGameOver(int health)
    {
        if (health<=0)
        {
            SetGameOver();
        }
    }
    public void SetGameOver()
    {
        PauseManager.Pause();
        gameOverScreen.SetActive(true);
    }
    public void Restart()
    {
        PauseManager.UnPause();
        SceneManager.LoadScene(SceneManager.GetSceneByName("Game").buildIndex);
    }
}
