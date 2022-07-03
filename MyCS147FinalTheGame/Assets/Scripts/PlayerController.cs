using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float speed; // Initialize speed
    public bool isTouchingWall = false;
    public bool isTouchingGround = false;
    public int jumpsLeft = 1;
    public int dashLeft = 1;
    public int deaths = 0;
    private bool facingRight = true;

    public float dashForce;
    public float startDashTimer;

    float currentDashTimer;
    bool isDashing;
    int dashDirection;

    void Start() {

    }

    void Update() {
        // check for jump and dash movement every frame
        Jump();
        Dash();
        Restart();

        // execute flip if need be
        if (!facingRight && Input.GetAxis("Horizontal") > 0) {
            Flip();
        } else if (facingRight && Input.GetAxis("Horizontal") < 0) {
            Flip();
        }

    }

    void FixedUpdate()
    {
        // prevents jittery wall contact
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f); // Only take horizontal
        transform.position += movement * Time.fixedDeltaTime * speed;
    }

    void Jump()
    {
        // jump with space bar
        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            if (isTouchingWall) {
                // wall jump
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 15f), ForceMode2D.Impulse);

            } else {
                // normal jump
                Vector3 v = gameObject.GetComponent<Rigidbody2D>().velocity;
                v.y = 0f;
                if (!isDashing)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = v;
                    if (!isTouchingGround)
                    {
                        jumpsLeft -= 1;
                        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 20f), ForceMode2D.Impulse);
                    }
                    else
                    {
                        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 14f), ForceMode2D.Impulse);
                    }
                }
            }

        }
    }


    void Dash()
    {
        // start dash animation
        if (Input.GetButtonDown("Dash") && dashLeft > 0 && !isTouchingGround)
        {
            isDashing = true;
            dashLeft -= 1;
            currentDashTimer = startDashTimer;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if (facingRight)
            {
                dashDirection = 1;
            }
            else
            {
                dashDirection = -1;
            }
        }

        if (isDashing)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * dashDirection * dashForce;
            currentDashTimer -= Time.deltaTime;

            if (currentDashTimer <= 0 || isTouchingWall)
            {
                isDashing = false;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }


    void Flip()
    {
        // turn eyes and wall collider
        facingRight = !facingRight;
        if (facingRight) {
            this.transform.GetChild(1).transform.position += new Vector3(0.45f, 0f, 0f);
            this.transform.GetChild(2).transform.position += new Vector3(0.95f, 0f, 0f);
        } else {
            this.transform.GetChild(1).transform.position += new Vector3(-0.45f, 0f, 0f);
            this.transform.GetChild(2).transform.position += new Vector3(-0.95f, 0f, 0f);
        }
    }

    void Restart()
    {
        if (Input.GetButtonDown("Restart")){
            SceneManager.LoadScene("SampleScene");
        }
    }

}