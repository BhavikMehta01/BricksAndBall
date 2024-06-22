using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCoverMovement : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate((-transform.up * 2 * Time.deltaTime));
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log(collision.transform.tag);
            collision.transform.GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log(GetComponent<BoxCollider2D>().enabled);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log(collision.transform.tag);
            collision.transform.GetComponent<BoxCollider2D>().enabled = true;
            Debug.Log(GetComponent<BoxCollider2D>().enabled);
        }
    }
}
