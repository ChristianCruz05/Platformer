using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    //stats
    public float movespeed = 5f;
    public Rigidbody2D rb;
    public float jumpForce = 1f;
    float horizontalMovement;

    //effects
    TrailRenderer trail;
    public GameObject particles;
    Color originalTrailColor;
    Color originalPlayerColor;
    SpriteRenderer playerSprite;

    //check
    private bool isGrounded = false;
    private bool isFacingRight = true;

    //health
    public int maxHealth = 100;
    private int currentHealth;

    public TextMeshProUGUI HealthText;

    //dashing
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;


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
        if(horizontalMovement > 0f && !isFacingRight)
        {
            Flip();
        }
        if(horizontalMovement < 0f && isFacingRight)
        {
            Flip();
        }
        if (isDashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            Instantiate(particles, transform.position, Quaternion.identity);
        }
        if(currentHealth <= 0)
        {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.Sandbox);
        }
    }
    private void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * movespeed * Time.deltaTime;
        transform.Translate(horizontalMovement, 0, 0); //tranform, move on its own
    }

    private void Flip()
    {
        Vector3 playerScale = gameObject.transform.localScale;
        playerScale.x *= -1;
        gameObject.transform.localScale = playerScale;
        
        isFacingRight = !isFacingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "ground")
        {
            AudioManager.Instance.PlaySFX("impact_metal");
            isGrounded = true;
        }
        if (collision.transform.tag == "JumpPowerUp")
        {
            Destroy(collision.transform.gameObject);
            //jumpForce *= 2 ;
            StartCoroutine(JumpBoost());
            Debug.Log("Jump");
            AudioManager.Instance.PlaySFX("pop");
        }
        if (collision.transform.tag == "SpeedPowerUp")
        {
            Destroy(collision.transform.gameObject);
            //movespeed *= 2;
            StartCoroutine(SpeedUp());
            Debug.Log("Speed");
            AudioManager.Instance.PlaySFX("pop");

        }
        if (collision.transform.tag == "HealthPotion")
        {
            AudioManager.Instance.PlaySFX("pop");
            if (currentHealth < maxHealth)
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
        if (collision.transform.tag == "Enemy")
        {
            StartCoroutine(TakeDamage());

            Destroy(collision.transform.gameObject);
            
        }
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "NextLevel")
        {
            StartCoroutine(NextLevel());
        }
    }

    private IEnumerator TakeDamage()
    {
        playerSprite.color = Color.red;
        if (currentHealth <= 0)
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

    private IEnumerator Dash()
    {
        trail.startColor = Color.white;
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale; //store original gravity
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        trail.startColor = originalTrailColor;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        
    }
    private IEnumerator NextLevel()
    {
        trail.startColor = Color.green;
        playerSprite.color = Color.green;
        yield return new WaitForSeconds(1.5f);
        ScenesManager.Instance.LoadNextScene();
    }
}
