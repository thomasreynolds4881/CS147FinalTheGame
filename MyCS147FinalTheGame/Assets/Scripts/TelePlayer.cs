using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TelePlayer : MonoBehaviour
{
    public GameObject coins;
    public GameObject textmesh;
    public GameObject blackOutSquare;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeBlackOutSquare(Collider2D other, int fadeSpeed = 5)
    {
        Color objectColor = blackOutSquare.GetComponent<SpriteRenderer>().color;
        float fadeAmount;

        while (blackOutSquare.GetComponent<SpriteRenderer>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackOutSquare.GetComponent<SpriteRenderer>().color = objectColor;
            yield return null;
        }
        other.transform.position = other.GetComponent<UpdateCheckpoint>().currCheckpoint;
        for (int i = 0; i < coins.transform.childCount; i++)
        {
            coins.transform.GetChild(i).gameObject.SetActive(true);
            yield return null;
        }
        while (blackOutSquare.GetComponent<SpriteRenderer>().color.a > 0)
        {
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackOutSquare.GetComponent<SpriteRenderer>().color = objectColor;
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            other.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
            StartCoroutine(FadeBlackOutSquare(other));
            other.GetComponent<UpdateCheckpoint>().coinGrabbed = false;
            other.GetComponent<PlayerController>().deaths++;
            textmesh.GetComponent<TextMesh>().text = other.GetComponent<PlayerController>().deaths.ToString();
        }
    }
}
