using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public int points;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // check if object is a coin
        if (other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            // coin has been grabbed
            this.transform.parent.GetChild(0).GetComponent<UpdateCheckpoint>().coinGrabbed = true;
        }
    }
}
