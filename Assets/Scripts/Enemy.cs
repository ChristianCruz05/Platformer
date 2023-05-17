using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth = 100f;

    [SerializeField] FloatingStatusBar healthbar;


    public Transform playerPos;
    public bool isChasing;
    public float chaseDistance;

    public float chaseSpeed;

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
        if (isChasing)
        {
            if(transform.position.x > playerPos.position.x)
            {
                transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                transform.position += Vector3.left * chaseSpeed * Time.deltaTime;
            }
            if (transform.position.x < playerPos.position.x)
            {
                transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
                transform.position += Vector3.right * chaseSpeed * Time.deltaTime;
            }
        } 
        else
        {
            if (Vector2.Distance(transform.position, playerPos.position) < chaseDistance)
            {
                isChasing = true;
            }
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, chaseDistance);
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
