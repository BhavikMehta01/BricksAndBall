using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2;
    public int bulletValue;
    bool isSetValue;
    void Update()
    {
        transform.Translate((transform.up * speed * Time.deltaTime));
             /*if (PlayerObjectManager.instance.isPlayerDouble)
        {
            Debug.Log("bulletValue / 2 : " + bulletValue / 2);
            if (bulletValue / 2 == 0 && !isSetValue)
            {
                Debug.Log("Right");
                isSetValue = true;
                transform.Translate((transform.right * 2 * Time.deltaTime));
            }
            else if(!isSetValue)
            {
                Debug.Log("left");
                isSetValue = true;
                transform.Translate((-transform.right * 2 * Time.deltaTime));
            }
        }*/

        Destroy(gameObject, 0.9f);
    }
}
