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
    [SerializeField] TextMeshProUGUI perviousHighScore;
    [SerializeField] private FloatReference totalFlow;

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
        string s = "Final Score";
        if (totalFlow.Value > PlayerPrefs.GetFloat("score", 0))
        {
            PlayerPrefs.SetFloat("score", totalFlow.Value);
            s = "New High Score";
        }

        perviousHighScore.text = $"High Score: {PlayerPrefs.GetFloat("score", 0)}";

        finalScore.text = $"{s}: {totalFlow.Value}";

        totalFlow.Value = 0;
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
