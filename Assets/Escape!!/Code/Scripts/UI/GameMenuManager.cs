using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;
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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OptionsToggle()
    {
        if (!optionsPanel.activeSelf)
        {
            audioText.text = (Mathf.CeilToInt(PlayerPrefs.GetFloat("volume", 1) * 100)).ToString();
            audioSlider.value = PlayerPrefs.GetFloat("volume", 1);
        }
        mainPanel.SetActive(optionsPanel.activeSelf);
        optionsPanel.SetActive(!optionsPanel.activeSelf);
    }

    public void SetAudio()
    {
        PlayerPrefs.SetFloat("volume", audioSlider.value);
    }

    public void Mute()
    {

    }
}
