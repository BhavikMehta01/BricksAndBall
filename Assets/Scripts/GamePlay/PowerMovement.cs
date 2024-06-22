using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMovement : MonoBehaviour
{
    void Update()
    {
        transform.Translate((-transform.up * 3f * Time.deltaTime));
        Destroy(gameObject, 5f);
    }
}