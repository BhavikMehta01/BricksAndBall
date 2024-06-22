using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxManager : MonoBehaviour
{
    public static BoxManager instance;
    [SerializeField] int boxValue;
    public TextMeshProUGUI value;
    public GameObject psBlast;
    Spawn spawnBox;
    public bool gameEnd;

    [Header("Power Handler")]
    public bool isBomb;
    public bool isSpeed;
    public bool isRedBox;
    public bool isDoublePower;
    [SerializeField] GameObject bombPower;
    [SerializeField] GameObject speedPower;
    [SerializeField] GameObject redBoxPower;
    [SerializeField] GameObject doublePower;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        spawnBox = transform.parent.parent.GetComponent<Spawn>() ;
        boxValue = Random.Range(1, 8);
        boxValue = boxValue * spawnBox.valueOfMultiply;
        value.text = boxValue.ToString();
        if (isBomb) bombPower.SetActive(true);
        if (isSpeed) speedPower.SetActive(true);
        if (isRedBox) redBoxPower.SetActive(true);
        if (isDoublePower) doublePower.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate((-transform.up * 3f * Time.deltaTime));
        Destroy(gameObject, 5f); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == PreferenceManager.instance.PowerBox)
        {
            if (isBomb)
            {
                StartCoroutine(WaitForDestroy(true));
                Debug.Log(PlayerObjectManager.instance.boxManagers.Count);
                for (int i = 0; i < PlayerObjectManager.instance.boxManagers.Count; i++)
                {
                    if (PlayerObjectManager.instance.boxManagers[i].gameObject)
                    {
                        PlayerObjectManager.instance.boxManagers[i].GetComponent<BoxManager>().psBlast.SetActive(true);
                        ShootManager.instance.score = ShootManager.instance.score + int.Parse(PlayerObjectManager.instance.boxManagers[i].GetComponent<BoxManager>().value.text);
                        //PlayerObjectManager.instance.boxManagers.Remove(PlayerObjectManager.instance.boxManagers[i]);
                        Destroy(PlayerObjectManager.instance.boxManagers[i].gameObject, 0.4f);
                    }
                }
            }
            else
            {
                ShootManager.instance.score = ShootManager.instance.score + boxValue;
                if(isRedBox || isDoublePower || isSpeed)
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(gameObject,0.5f);
                }
                else
                {
                    Destroy(gameObject);
                }
            }       
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            //bricksTouchSound.Play();
            SoundManagerGame.instance.allSounds[0].Play();
            Instantiate(PlayerObjectManager.instance.bullateBlastPrefab, transform);
            Destroy(collision.gameObject);
            if (boxValue > 1)
            {
                boxValue--;
                value.text = boxValue.ToString();
                ShootManager.instance.score++;
            }
            else
            {
                psBlast.SetActive(true);
                if (isBomb)
                {
                    StartCoroutine(WaitForDestroy(true));
                    for (int i = 0; i < PlayerObjectManager.instance.boxManagers.Count; i++)
                    {
                        if (PlayerObjectManager.instance.boxManagers[i].gameObject)
                        {
                            PlayerObjectManager.instance.boxManagers[i].GetComponent<BoxManager>().psBlast.SetActive(true);
                            ShootManager.instance.score = ShootManager.instance.score + int.Parse(PlayerObjectManager.instance.boxManagers[i].GetComponent<BoxManager>().value.text);
                            //PlayerObjectManager.instance.boxManagers.Remove(PlayerObjectManager.instance.boxManagers[i]);
                            Destroy(PlayerObjectManager.instance.boxManagers[i].gameObject, 0.4f);
                        }
                    }

                    /*foreach (var item in PlayerObjectManager.instance.boxManagers)
                    {
                        item.GetComponent<BoxManager>().psBlast.SetActive(true);
                        ShootManager.instance.score = ShootManager.instance.score + int.Parse(item.GetComponent<BoxManager>().value.text);
                        PlayerObjectManager.instance.boxManagers.Remove(item);
                        Destroy(item.gameObject, 0.5f);
                    }*/
                }
                else if(isSpeed || isRedBox || isDoublePower)
                {
                    psBlast.SetActive(true);
                    Destroy(psBlast, 0.2f);
                    WaitForbricksOff();
                   // StartCoroutine(WaitForDestroy(false));
                }
                else
                {
                    StartCoroutine(WaitForDestroy(true));
                }
                /*      if (PlayerObjectManager.instance.boxManagers.Contains(gameObject))
                      {
                          Debug.Log("yes");
                          PlayerObjectManager.instance.boxManagers.Remove(gameObject);
                      }*/
            }
        }
        if (boxValue > 0)
        {
            if (collision.transform.tag == "Player")
            {
                gameEnd = true;
                MoveManager.instance.speed = 0;
                CanvasManager.instance.isPlaying = false;
                // StartCoroutine(WaitForSetScoreOnRoundCompeletePane());
                CanvasManager.instance.SetScoreOnRoundCompeletePane();
                Time.timeScale = 0;
           
            }
        }
    }

    void WaitForbricksOff()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }



    IEnumerator WaitForDestroy(bool isDestroy)
    {
        if (PlayerObjectManager.instance.boxManagers.Contains(gameObject))
        {
            PlayerObjectManager.instance.boxManagers.Remove(gameObject);
        }
        if (isDestroy)
        {
            yield return new WaitForSeconds(0.0f);
            Destroy(gameObject);
        }
    }

    public IEnumerator WaitForGameOverPanel()
    {
        
        yield return new WaitForSecondsRealtime(2.5f);
        Debug.Log("game over wait end..");
        CanvasManager.instance.leaderBoardUI.gameObject.SetActive(true);
/*        if (PlayerPrefs.HasKey(PreferenceManager.instance.isLogin))
        {
            if (PreferenceManager.instance.GetLonginStatus() == 1)
            {
                CanvasManager.instance.leaderBoardUI.gameObject.SetActive(true);
                //SceneManager.LoadScene("MenuScene");
            }
            else
            {
                CanvasManager.instance.RankSignInUI.SetActive(true);
            }
        }
        else
        {
            CanvasManager.instance.RankSignInUI.SetActive(true);
        }*/
    }


   /* IEnumerator WaitForGameOverPanel()
    {

        Debug.Log("game over wait..");
        Debug.Log("game over wait..");
        CanvasManager.instance.SetScoreOnRoundCompeletePane((isDone) =>
        {
            if (isDone)
            {
                yield return new WaitForSecondsRealtime(2.5f);
                Debug.Log("game over wait end..");
                if (PlayerPrefs.HasKey(PreferenceManager.instance.isLogin))
                {
                    if (PreferenceManager.instance.GetLonginStatus() == 1)
                    {
                        CanvasManager.instance.leaderBoardUI.gameObject.SetActive(true);
                        //SceneManager.LoadScene("MenuScene");
                    }
                    else
                    {
                        CanvasManager.instance.RankSignInUI.SetActive(true);
                    }
                }
                else
                {
                    CanvasManager.instance.RankSignInUI.SetActive(true);
                }

            }
        });


        *//* yield return new WaitForSeconds(1.5f);
         Debug.Log("game over wait end wfs..");*/

        /* if (PreferenceManager.instance.GetLonginStatus() == 1)
           {
               CanvasManager.instance.homrBtnObj.SetActive(false);
               CanvasManager.instance.retryBtnObj.SetActive(false);
           }
           else
           {
               CanvasManager.instance.homrBtnObj.SetActive(true);
               CanvasManager.instance.retryBtnObj.SetActive(true);
           }*//*
    }*/
}