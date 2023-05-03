using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float movespeed = 5f;
    public Rigidbody2D rb;
    public float jumpForce = 1f;
    public GameObject particles;
    float horizontalMovement;

    bool isGrounded = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            Instantiate(particles, transform.position, Quaternion.identity);
        }
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
        if (collision.transform.tag == "PowerUp")
        {
            Destroy(collision.transform.gameObject);
            jumpForce *= 2 ;

        }
    }
}
