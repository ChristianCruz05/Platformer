using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth = 100f;

    [SerializeField] FloatingStatusBar healthbar;
    private void Awake()
    {
        healthbar = GetComponentInChildren<FloatingStatusBar>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(50f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "PlayerBullet")
        {
            Debug.Log("ow");
            TakeDamage(50f);
        }
    }
    private void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);
        if(currentHealth <= 0)
        {
            Debug.Log("Killed");
            Destroy(gameObject);
        }

    }
}
