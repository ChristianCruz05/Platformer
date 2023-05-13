using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelSelect : MonoBehaviour
{
    [SerializeField] Button LevelOne;
    [SerializeField] Button LevelTwo;
    [SerializeField] Button Sandbox;

    void Start()
    {
        LevelOne.onClick.AddListener(LoadLevelOne);
        LevelTwo.onClick.AddListener(LoadLevelTwo);
        Sandbox.onClick.AddListener(LoadSandbox);
    }

    private void LoadLevelOne()
    {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.LevelOne);
    }
    private void LoadLevelTwo()
    {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.LevelTwo);
    }
    private void LoadSandbox()
    {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.Sandbox);
    }
}
