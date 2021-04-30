using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHandle : MonoBehaviour
{
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            Player.GetComponent<PlayerController>().jumpsLeft = 1;
            Player.GetComponent<PlayerController>().dashLeft = 1;
            Player.GetComponent<PlayerController>().isTouchingGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {

            Player.GetComponent<PlayerController>().isTouchingGround = false;
        }
    }

}
