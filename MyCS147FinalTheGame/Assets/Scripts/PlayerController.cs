using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float speed = 5f; // Initialize speed
    public bool isTouchingWall = false;
    public bool isTouchingGround = false;
    public int jumpsLeft = 1;
    public int dashLeft = 1;
    public int deaths = 0;
    private bool facingRight = true;

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
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 8f), ForceMode2D.Impulse);

            } else {
                // normal jump
                Vector3 v = gameObject.GetComponent<Rigidbody2D>().velocity;
                v.y = 0f;
                gameObject.GetComponent<Rigidbody2D>().velocity = v;
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 8f), ForceMode2D.Impulse);
                if(!isTouchingGround)
                {
                    jumpsLeft -= 1;
                }
            }

        }
    }

    void Dash()
    {
        // dash using F or mouse1
        if (Input.GetButtonDown("Dash") && dashLeft > 0)
        {
            if (facingRight)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(8f, 0f), ForceMode2D.Impulse);
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-8f, 0f), ForceMode2D.Impulse);
            }
            if (!isTouchingGround)
            {
                dashLeft -= 1;
            }
        }
    }

    void Flip()
    {
        // turn eyes and wall collider
        facingRight = !facingRight;
        if (facingRight) {
            this.transform.GetChild(1).transform.position += new Vector3(0.45f, 0f, 0f);
            this.transform.GetChild(2).transform.position += new Vector3(0.978f, 0f, 0f);
        } else {
            this.transform.GetChild(1).transform.position += new Vector3(-0.45f, 0f, 0f);
            this.transform.GetChild(2).transform.position += new Vector3(-0.978f, 0f, 0f);
        }
    }

    void Restart()
    {
        if (Input.GetButtonDown("Restart")){
            SceneManager.LoadScene("SampleScene");
        }
    }

}