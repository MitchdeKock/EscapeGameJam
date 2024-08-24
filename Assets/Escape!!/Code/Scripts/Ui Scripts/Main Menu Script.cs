using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Buttons")]
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button TutorialButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private Button TutorialBackButton;
    [SerializeField] private Button CreditsBackButton;

    [Header("Menu")]
    [SerializeField] private GameObject Menu;

    [Header("Sub Menus")]
    [SerializeField] private GameObject Tutorial;
    [SerializeField] private GameObject Credits;
    [SerializeField] private GameObject DefaultMenu;
    void Start()
    {
        //PauseManager.Pause();
        PlayButton.onClick.AddListener(onStart);
        TutorialButton.onClick.AddListener(OpenTutorial);
        CreditsButton.onClick.AddListener(OpenCredits);
        TutorialBackButton.onClick.AddListener(ReturnToMenu);
        CreditsBackButton.onClick.AddListener(ReturnToMenu);
        ReturnToMenu();
    }

    private void OpenCredits()
    {
        DefaultMenu.SetActive(false);
        Credits.SetActive(true);

    }
    private void OpenTutorial ()
    {
        DefaultMenu.SetActive(false);
        Tutorial.SetActive(true);

    }

    private void ReturnToMenu()
    {
        DefaultMenu?.SetActive(true);
        Tutorial.SetActive(false);
        Credits.SetActive(false);
    }
    public void onStart()
    {
        SceneManager.LoadScene("Game");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
