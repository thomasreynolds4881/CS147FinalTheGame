using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpHandle : MonoBehaviour
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

            Player.GetComponent<PlayerController>().isTouchingWall = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {

            Player.GetComponent<PlayerController>().isTouchingWall = false;
        }
    }

}
