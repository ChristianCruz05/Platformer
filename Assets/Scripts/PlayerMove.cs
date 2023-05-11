using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    //public float maxSpeed;
    //public float maxJump;

    public float movespeed = 5f;
    public Rigidbody2D rb;
    public float jumpForce = 1f;
    float horizontalMovement;

    TrailRenderer trail;
    public GameObject particles;
    Color originalTrailColor;
    Color originalPlayerColor;
    SpriteRenderer playerSprite;

    bool isGrounded = false;

    public int maxHealth = 100;
    public int currentHealth;

    public TextMeshProUGUI HealthText;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        trail = GetComponentInChildren<TrailRenderer>();
        HealthText.SetText("Health: " + currentHealth.ToString());
        originalTrailColor = trail.startColor;
        playerSprite = GetComponent<SpriteRenderer>();
        originalPlayerColor = playerSprite.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            Instantiate(particles, transform.position, Quaternion.identity);
        }
        HealthText.SetText("Health: " + currentHealth.ToString());
    }
    void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * movespeed * Time.deltaTime;
        transform.Translate(horizontalMovement, 0, 0); //tranform, move on its own
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "ground")
        {
            isGrounded = true;
        }
        if (collision.transform.tag == "JumpPowerUp")
        {
            Destroy(collision.transform.gameObject);
            //jumpForce *= 2 ;
            StartCoroutine(JumpBoost());
            Debug.Log("Jump");
        }
        if (collision.transform.tag == "SpeedPowerUp")
        {
            Destroy(collision.transform.gameObject);
            //movespeed *= 2;
            StartCoroutine(SpeedUp());
            Debug.Log("Speed");

        }
        if (collision.transform.tag == "HealthPotion")
        {
            if(currentHealth < maxHealth)
            {
                currentHealth += 10;
            }
            
            Destroy(collision.transform.gameObject);
            Debug.Log("Health");
        }
        if (collision.transform.tag == "Bullet")
        {
            StartCoroutine(TakeDamage());
            
            Destroy(collision.transform.gameObject);
            Debug.Log("Bullet");
        }
    }

    private IEnumerator TakeDamage()
    {
        playerSprite.color = Color.red;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        if (currentHealth > 0)
        {
            currentHealth -= 10;
        }
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = originalPlayerColor;

    }
    private IEnumerator SpeedUp()
    {
        trail.startColor = Color.blue;
        movespeed *= 2 ;
        yield return new WaitForSeconds(5f);
        movespeed /= 2;
        trail.startColor = originalTrailColor;
    }
    private IEnumerator JumpBoost()
    {
        trail.startColor = Color.red;
        jumpForce *= 2;
        yield return new WaitForSeconds(5f);
        jumpForce /= 2;
        trail.startColor = originalTrailColor;
    }
}
