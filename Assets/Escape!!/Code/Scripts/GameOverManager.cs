using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI finalScore;
    [SerializeField] private FloatReference totalKills;

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
        finalScore.text = $"FINAL SCORE: {totalKills.Value}";
        if (totalKills.Value > PlayerPrefs.GetFloat("score", 0))
        {
            PlayerPrefs.SetFloat("score", totalKills.Value);
        }
    }
    public void Restart()
    {
        PauseManager.UnPause();
        SceneManager.LoadScene("Game");
    }

    public void BackToMain()
    {
        PauseManager.UnPause();
        SceneManager.LoadScene("MainMenu");
    }
}
