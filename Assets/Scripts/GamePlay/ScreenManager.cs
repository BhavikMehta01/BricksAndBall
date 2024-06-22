using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    float screenSize;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Screen.currentResolution);
        screenSize = (float)Screen.width / (float)Screen.height;
        Debug.Log(screenSize);
        if(screenSize > 0.5f)
        {
            Camera.main.orthographicSize = 4.5f;
        }
        else
        {
            Camera.main.orthographicSize = 5.5f;
        }
    }  
}
