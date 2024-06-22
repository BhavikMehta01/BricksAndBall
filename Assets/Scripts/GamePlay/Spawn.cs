using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("Box")]
    public GameObject[] boxPrefab;
    public Transform[] points;
    public float beat = (60 / 130) * 2;
    private float timer;
    public int valueOfMultiply = 1;

    [Header("Power")]
    public GameObject[] powerPrefab;
    public Transform[] powerPoints;
    bool isPowerOn;

/*    [Header("BoxCover")]
    [SerializeField] GameObject BoxcoverPrefab;
    [SerializeField] Transform BoxcoverPoint;*/
    
  //  public List<GameObject> tempBox = new List<GameObject>();

    bool isBomb = false;
    bool isSpeed = false;
    bool isRedBox = false;
    bool isDouble = false;
    private void Start()
    {
        StartCoroutine(WaitForMultiply());
        //boxManagers = new List<GameObject>();
    }

    GameObject tempEnemy;
    GameObject tempEnemyForBomb;
    GameObject tempPower;
    // Update is called once per frame
    void Update()
    {
        if (timer > beat)
        {
            if (isBomb)
            {
                CreateBombInBricks();
            }
            else if(isSpeed)
            {
                CreateSpeedInBricks();
            }else if (isRedBox)
            {
                CreateRedBoxInBricks();
            }else if (isDouble)
            {
                CreateDoubleInBricks();
            }
            else
            {
                for (int i = 0; i < points.Length; i++)
                {
                    
                    tempEnemy =  Instantiate(
                                boxPrefab[Random.Range(0,4)],
                                points[i].transform);
                }
            }
            //Instantiate(BoxcoverPrefab, BoxcoverPoint);
            if (!isPowerOn && !PlayerObjectManager.instance.isPlayerPowerOn && !PlayerObjectManager.instance.isPlayerSpeedUp && !PlayerObjectManager.instance.isPlayerDouble)
            {
                isPowerOn = true;

              GameObject power =  Instantiate(
                                    powerPrefab[Random.Range(0, 3)],
                                    powerPoints[Random.Range(0, 4)].transform
                                );
                StartCoroutine(WaitForNextPower(Random.Range(4, 10)));
              /*  if(power.transform.tag == PreferenceManager.instance.PowerBox)
                {
                    boxManagers = tempBox;
                }*/
            }
            //tempBox.Clear();

            timer -= beat;
        }
        timer += Time.deltaTime;
    }

    private void CreateSpeedInBricks()
    {
       // PlayerObjectManager.instance.boxManagers.Clear();
        isSpeed = false;
        int speedPositionnumber = Random.Range(0, 5);
        tempEnemy = Instantiate(boxPrefab[Random.Range(0, 4)], points[speedPositionnumber].transform);
        tempEnemy.GetComponent<BoxManager>().isSpeed = true;
        StartCoroutine(WaitForNextPower("speed"));

       // PlayerObjectManager.instance.boxManagers.Add(tempEnemy);
        for (int i = 0; i < points.Length; i++)
        {
            if (speedPositionnumber != i)
            {
                tempEnemy = Instantiate(
                        boxPrefab[Random.Range(0, 4)],
                         points[i].transform);
               // PlayerObjectManager.instance.boxManagers.Add(tempEnemy);
            }
        }
    }

    private void CreateRedBoxInBricks()
    {
        //PlayerObjectManager.instance.boxManagers.Clear();
        isRedBox = false;
        int redBoxPositionnumber = Random.Range(0, 5);
        Debug.Log(boxPrefab.Length + " - " + redBoxPositionnumber);
        tempEnemy = Instantiate(boxPrefab[Random.Range(0, 4)], points[redBoxPositionnumber].transform);
        tempEnemy.GetComponent<BoxManager>().isRedBox = true;
        StartCoroutine(WaitForNextPower("redbox"));

       // PlayerObjectManager.instance.boxManagers.Add(tempEnemy);
        for (int i = 0; i < points.Length; i++)
        {
            if (redBoxPositionnumber != i)
            {
                tempEnemy = Instantiate(
                        boxPrefab[Random.Range(0, 4)],
                         points[i].transform);
               // PlayerObjectManager.instance.boxManagers.Add(tempEnemy);
            }
        }
    }

    private void CreateDoubleInBricks()
    {
      //  PlayerObjectManager.instance.boxManagers.Clear();
        isDouble = false;
        int doubleBoxPositionnumber = Random.Range(0, 5);
        Debug.Log(boxPrefab.Length + " - " + doubleBoxPositionnumber);
        tempEnemy = Instantiate(boxPrefab[Random.Range(0, 4)], points[doubleBoxPositionnumber].transform);
        tempEnemy.GetComponent<BoxManager>().isDoublePower = true;
        StartCoroutine(WaitForNextPower("double"));

       // PlayerObjectManager.instance.boxManagers.Add(tempEnemy);
        for (int i = 0; i < points.Length; i++)
        {
            if (doubleBoxPositionnumber != i)
            {
                tempEnemy = Instantiate(
                        boxPrefab[Random.Range(0, 4)],
                         points[i].transform);
             //   PlayerObjectManager.instance.boxManagers.Add(tempEnemy);
            }
        }
    }
    private void CreateBombInBricks()
    {
        PlayerObjectManager.instance.boxManagers.Clear();
        isBomb = false;
        int bombPositionnumber = Random.Range(0, 5);
        tempEnemyForBomb = Instantiate(boxPrefab[Random.Range(0, 4)], points[bombPositionnumber].transform);
        tempEnemyForBomb.GetComponent<BoxManager>().isBomb = true;
        StartCoroutine(WaitForNextPower("bomb"));

        PlayerObjectManager.instance.boxManagers.Add(tempEnemyForBomb);
        for (int i = 0; i < points.Length; i++)
        {
            if (bombPositionnumber != i)
            {
                tempEnemyForBomb = Instantiate(
                        boxPrefab[Random.Range(0, 4)],
                         points[i].transform);
                PlayerObjectManager.instance.boxManagers.Add(tempEnemyForBomb);
            }
        }
    }

    /// <summary>
    /// Multiply value of box value
    /// </summary>
    IEnumerator WaitForMultiply()
    {
        yield return new WaitForSeconds(0.5f);
        beat = 2;
        yield return new WaitForSeconds(2f);
        isBomb = true;
        yield return new WaitForSeconds(Random.Range(3,8));
        isSpeed = true;
        yield return new WaitForSeconds(Random.Range(5, 10));
        isRedBox = true;
        yield return new WaitForSeconds(Random.Range(9, 10));
        isRedBox = true;
        yield return new WaitForSeconds(25);
        valueOfMultiply++;
        StartCoroutine("WaitForMultiply");
    }

    /// <summary>
    /// Wait For next Power
    /// </summary>
    /// <param name="waitValue"></param>
    /// <returns></returns>
    IEnumerator WaitForNextPower(int waitValue)
    {
        yield return new WaitForSeconds(waitValue);
        isPowerOn = false;
    }

    IEnumerator WaitForNextPower(string powerActiveName)
    {
        switch (powerActiveName)
        {
            case "bomb":
                yield return new WaitForSeconds(Random.Range(10,20));
                isBomb = true;
                break;

            case "speed":
                yield return new WaitForSeconds(Random.Range(12, 20));
                isSpeed = true;
                break;

            case "redbox":
                yield return new WaitForSeconds(Random.Range(10, 25));
                isRedBox = true;
                break;

            case "double":
                yield return new WaitForSeconds(Random.Range(8, 20));
                isDouble = true;
                break;

            default:
                break;
        }
    }
}
