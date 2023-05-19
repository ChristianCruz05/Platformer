using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButtonSFX : MonoBehaviour
{
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(AddSFX);
    }

    
    void Update()
    {
        
    }
    void AddSFX()
    {
        AudioManager.Instance.PlaySFX("click");
    }
}
