using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Game Obejcts")]
    [SerializeField] private GameObject pausemenu;
    [SerializeField] private GameObject defaultScreen;
    [SerializeField] private GameObject SettingsMenu;

    private CoreHealthHandler coreScriptComponent;

    [Header("Buttons")]
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button SettingsMenuButton;
    [SerializeField] private Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        pausemenu.SetActive(false);
        coreScriptComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<CoreHealthHandler>();

        ResumeButton.onClick.AddListener(resume);
        ExitButton.onClick.AddListener(Exit);
        SettingsMenuButton.onClick.AddListener(settingsOpened);
        backButton.onClick.AddListener(Back);

        SettingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && coreScriptComponent.Health > 0)
        {
            PauseManager.TogglePause();
            pausemenu.SetActive(!pausemenu.activeSelf);
        }
    }

    public void resume()
    {
        PauseManager.TogglePause();
        pausemenu.SetActive(!pausemenu.activeSelf);
    }

    private void Exit()
    {
        PauseManager.TogglePause();
        SceneManager.LoadScene("MainMenu");
    }
    private void Back()
    {
        defaultScreen.SetActive(true);
        SettingsMenu.SetActive(false );
    }
    private void settingsOpened()
    {
        defaultScreen.SetActive(false) ;
        SettingsMenu.SetActive(true);
    }
}
