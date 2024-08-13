using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Scene gameScene;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private TMP_Text audioText;
    void Start()
    {
        audioSlider.onValueChanged.AddListener(delegate { SliderValueChanged(); });
    }

    public void SliderValueChanged()
    {
        audioText.text = (Mathf.CeilToInt(audioSlider.value * 100)).ToString();
        SetAudio();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void AudioToggle()
    {
        if (!audioPanel.activeSelf)
        {
            audioText.text = (Mathf.CeilToInt(PlayerPrefs.GetFloat("volume", 1) * 100)).ToString();
            audioSlider.value = PlayerPrefs.GetFloat("volume", 1);
        }
        mainPanel.SetActive(audioPanel.activeSelf);
        audioPanel.SetActive(!audioPanel.activeSelf);
    }

    public void SetAudio()
    {
        PlayerPrefs.SetFloat("volume", audioSlider.value);
    }

    public void Mute()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
}
