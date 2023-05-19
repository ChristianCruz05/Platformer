using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackPanelController : MonoBehaviour
{
    [SerializeField] Button restartLevel;
    [SerializeField] Button backToLevel;
    [SerializeField] Button goToMainMenu;

    [SerializeField] GameObject BackPanel;
    [SerializeField] GameObject ControlsPanel;
    [SerializeField] string currentScene;
    private void Start()
    {
        restartLevel.onClick.AddListener(Restart);
        goToMainMenu.onClick.AddListener(GoToMenu);
        backToLevel.onClick.AddListener(BackToGame);


    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (BackPanel.activeInHierarchy == true)
            {
                BackPanel.SetActive(false);
            }
            else
            {
                BackPanel.SetActive(true);
            }

        }
        if (Input.GetKeyDown(KeyCode.F1) && BackPanel.activeInHierarchy == false)
        {
            if(ControlsPanel.activeInHierarchy == true)
            {
                ControlsPanel.SetActive(false);
            }
            else
            {
                ControlsPanel.SetActive(true);
            }

        }
    }
    private void Restart()
    {
        ScenesManager.Instance.LoadScene(currentScene);
        AudioManager.Instance.PlaySFX("click");
    }
    private void GoToMenu()
    {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu);
        AudioManager.Instance.PlaySFX("click");
        AudioManager.Instance.PlayMusic("campfire");
    }
    private void BackToGame()
    {
        AudioManager.Instance.PlayMusic("campfire");
        BackPanel.SetActive(false);
    }

}
