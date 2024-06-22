using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveLeft : MonoBehaviour
{
    float speed = 8;

    void Update()
    {

        if (PlayerObjectManager.instance.isPlayerDouble)
        {
            transform.Translate((transform.up * speed * Time.deltaTime));
            transform.Translate((-transform.right / 1.2f * Time.deltaTime));
        }
        else
        {
            transform.Translate((transform.up * speed * Time.deltaTime));
            /*Destroy(gameObject);*/
        }
    
        Destroy(gameObject, 1f);
    }
}