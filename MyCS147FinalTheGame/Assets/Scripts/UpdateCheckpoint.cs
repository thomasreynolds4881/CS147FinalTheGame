using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCheckpoint : MonoBehaviour
{
    public Vector3 currCheckpoint;
    public GameObject wall;
    public bool coinGrabbed = false;
    public int points;
    public GameObject textmesh;

    // Start is called before the first frame update
    void Start()
    {
        currCheckpoint = new Vector3(-8.83f, -4.04f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            // set new checkpoint
            if (currCheckpoint.x < other.transform.position.x)
            {
                // reset coins and stuff if checkpoint is returned to
                if (coinGrabbed)
                {
                    points++;
                    coinGrabbed = false;
                    textmesh.GetComponent<TextMesh>().text = points.ToString();
                }

                currCheckpoint = other.transform.position;

                // change camera
                Vector3 nextScreen = new Vector3(this.transform.parent.transform.position.x + 20, 0, -10);
                this.transform.parent.transform.position = nextScreen;

                // spawn at next screen
                this.transform.position = currCheckpoint;

                // move following wall
                wall.transform.position = new Vector3(wall.transform.position.x + 20, wall.transform.position.y, wall.transform.position.z);
            }
        }
    }
}
