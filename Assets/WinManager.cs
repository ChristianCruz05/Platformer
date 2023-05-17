using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    GameObject[] enemies;
    public TextMeshProUGUI remainingEnemiesText;
    public GameObject winText;
    public int enemiesLeft = 0;
    void Start()
    {
        
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        remainingEnemiesText.SetText("Remaining Enemies: " + enemies.Length.ToString());

        if (enemies.Length <= 0)
        {
            //win
            Debug.Log("WINNNNN");
            StartCoroutine(Win());
            winText.SetActive(true);
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(3f);
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu);
    }
}
