using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public static MoveManager instance;
    [HideInInspector]
    public float speed;
    PowerManager powerManager;
    Touch touch;
    bool isOnPowerCanvas = false;
    const float m_doubleTime = 0.5f;
    float m_doubleStart;
    public int timeTaken;
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        speed = 0.0035f;

        powerManager = GetComponent<PowerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanvasManager.instance.isPlaying && !isOnPowerCanvas && !CanvasManager.instance.isHandObjOn)
        {
            timer += Time.deltaTime;
            timeTaken = (int)timer % 60;
            Debug.Log(timeTaken);
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speed, transform.position.y, transform.position.z);

                    var pos = transform.position;
                    pos.x = Mathf.Clamp(transform.position.x, -2.0f, 2.0f);
                    transform.position = pos;
                }
            }
        }
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null && !isOnPowerCanvas && CanvasManager.instance.isPlaying)
        {
            if(hit.collider.gameObject.name == "Paddle" && !PlayerObjectManager.instance.isPowerActive)
            {   
                DoubleClick();
            }
        }
    }

    void DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if ((Time.time - m_doubleStart) <= m_doubleTime)
                OnDoubleClick();

            m_doubleStart = Time.time;
        }
    }

    void OnDoubleClick()
    {
        Debug.Log(powerManager.isRedPowerCollected + " -r " + powerManager.isSpeedPowerCollected + " -S" + powerManager.isDoubleSpeedPowerCollected + " -D ");
        if(powerManager.isRedPowerCollected || powerManager.isSpeedPowerCollected || powerManager.isDoubleSpeedPowerCollected)
        {
            if (CanvasManager.instance.handObj.activeInHierarchy)
            {
                CanvasManager.instance.handObj.SetActive(false);
                CanvasManager.instance.tutorialMsg.text = "";
            }
         //   speed = 0.0f;
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            CanvasManager.instance.blurcanvas.SetActive(true);
            if (transform.position.x > 0.25f) PlayerObjectManager.instance.powerCanavs.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2f, 0);
            else PlayerObjectManager.instance.powerCanavs.GetComponent<RectTransform>().anchoredPosition = new Vector2(2f, 0);
            PlayerObjectManager.instance.powerCanavs.SetActive(true);
            if (!PlayerPrefs.HasKey("powerSelected"))
            {
                PreferenceManager.instance.SetTutorialOnTank();
                CanvasManager.instance.tutorialMsg.text = "Select the Power Up";
                PlayerObjectManager.instance.powerCanavs.transform.GetChild(PlayerObjectManager.instance.powerCanavs.transform.childCount - 1).gameObject.SetActive(true);
            }
            else
            {
                PlayerObjectManager.instance.powerCanavs.transform.GetChild(PlayerObjectManager.instance.powerCanavs.transform.childCount - 1).gameObject.SetActive(false);
                StartCoroutine(OnPowerCanavasOff());
            }
            isOnPowerCanvas = true;
            Time.timeScale = 0;
        }
        else
        {
            CanvasManager.instance.errorMsgPanel.SetActive(true);
            CanvasManager.instance.errorMsgPText.text = "No Power up available";

            Invoke("waitForRemoveErrorMasg",1f);
        }
    }

    void waitForRemoveErrorMasg()
    {
        CanvasManager.instance.errorMsgPanel.SetActive(false);
    }

    IEnumerator OnPowerCanavasOff()
    {
        
        yield return new WaitForSecondsRealtime(3f);
       // speed = 0.0035f;
        StartGameAgin_PowerCanvasOff();
    }

    public void StartGameAgin_PowerCanvasOff()
    {
        CanvasManager.instance.tutorialMsg.text = "";
        
        GetComponent<SpriteRenderer>().sortingOrder = -2;
        CanvasManager.instance.blurcanvas.SetActive(false);
        Time.timeScale = 1;
        isOnPowerCanvas = false;
        PlayerObjectManager.instance.powerCanavs.SetActive(false);
        CanvasManager.instance.isHandObjOn = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            speed = 0.001f;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            speed = 0.0035f;
        }
    }
}