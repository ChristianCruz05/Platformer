using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIMainMenu : MonoBehaviour
{
    [SerializeField] Button _startGame;
    
    void Start()
    {
        _startGame.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        ScenesManager.Instance.LoadNewGame();
    }
}
