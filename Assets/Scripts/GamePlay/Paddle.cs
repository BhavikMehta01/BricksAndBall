using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // private Ball Ball;
    //  private AudioManager audioManager;
    //   private GameManager gameManager;
    private float mouseXPos;
    private float zDistance, leftCorner, rightCorner;

    private float firstX = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Restrict paddle position
        zDistance = transform.position.z - Camera.main.transform.position.z;
        // 
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        leftCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDistance)).x + sprite.bounds.size.x/2;
        rightCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, zDistance)).x - sprite.bounds.size.x/2;
    }
 
    // Update is called once per frame
    private void MoveWithMouse()
    {
        // Main gameplay
        // Move paddle with mouse
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = zDistance;
        mouseXPos = Camera.main.ScreenToWorldPoint(mousePos).x;
        Vector3 paddlePos = gameObject.transform.position;
        paddlePos.x = Mathf.Clamp(mouseXPos, leftCorner, rightCorner);
        gameObject.transform.position = paddlePos;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        }
        else if (Input.GetMouseButton(0))
        {
            float currentX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;

            float distance = Mathf.Abs(Mathf.Abs(currentX) - Mathf.Abs(firstX));
            if (CanvasManager.instance.isPlaying) MoveWithMouse();
        }
        
    }
}
