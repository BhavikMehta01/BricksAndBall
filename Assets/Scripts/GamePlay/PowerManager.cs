using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    [SerializeField] PlayerObjectManager playerObjectManager;
    int blastvalue;

    public bool isRedPowerCollected;
    public bool isSpeedPowerCollected;
    public bool isDoubleSpeedPowerCollected;

    
    int redboxcounter,speedcounter,doublecounter;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == PreferenceManager.instance.PowerBox)
        {
            isRedPowerCollected = true;
            redboxcounter++;
            PlayerObjectManager.instance.redBoxtext.text = redboxcounter.ToString("D2");
            collision.gameObject.SetActive(false);
            if (!PlayerPrefs.HasKey("DoubleTap"))
            {
                CanvasManager.instance.ShowHandTutorialOnTank();
            }
        }
        if (collision.transform.tag == PreferenceManager.instance.PowerSpeed)
        {
            isSpeedPowerCollected = true;
            speedcounter++;
            PlayerObjectManager.instance.speedtext.text = speedcounter.ToString("D2");
            collision.gameObject.SetActive(false);
            if (!PlayerPrefs.HasKey("DoubleTap"))
            {
                CanvasManager.instance.ShowHandTutorialOnTank();
            }
        }
        if (collision.transform.tag == PreferenceManager.instance.PowerDouble)
        {
            isDoubleSpeedPowerCollected = true;
            doublecounter++;
            PlayerObjectManager.instance.doubletext.text = doublecounter.ToString("D2");
            collision.gameObject.SetActive(false);
            if (!PlayerPrefs.HasKey("DoubleTap"))
            {
                CanvasManager.instance.ShowHandTutorialOnTank();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == PreferenceManager.instance.PowerBox)
        {
            isRedPowerCollected = true;
            redboxcounter++;
            PlayerObjectManager.instance.redBoxtext.text = redboxcounter.ToString("D2");
            collision.gameObject.SetActive(false);
            if (!PlayerPrefs.HasKey("DoubleTap"))
            {
                CanvasManager.instance.ShowHandTutorialOnTank();
            }
        }
        if (collision.transform.tag == PreferenceManager.instance.PowerSpeed)
        {
            isSpeedPowerCollected = true;
            speedcounter++;
            PlayerObjectManager.instance.speedtext.text = speedcounter.ToString("D2");
            collision.gameObject.SetActive(false);
            if (!PlayerPrefs.HasKey("DoubleTap"))
            {
                CanvasManager.instance.ShowHandTutorialOnTank();
            }
        }
        if (collision.transform.tag == PreferenceManager.instance.PowerDouble)
        {
            isDoubleSpeedPowerCollected = true;
            doublecounter++;
            PlayerObjectManager.instance.doubletext.text = doublecounter.ToString("D2");
            collision.gameObject.SetActive(false);
            if (!PlayerPrefs.HasKey("DoubleTap"))
            {
                CanvasManager.instance.ShowHandTutorialOnTank();
            }
        }
    }

    public void DoublePowerActive()
    {
        if(doublecounter > 0)
        {
            doublecounter--;
            playerObjectManager.doubletext.text = doublecounter.ToString("D2");
            playerObjectManager.isPlayerDouble = true;
            if (doublecounter == 0)
            {
                isDoubleSpeedPowerCollected = false;
              //  playerObjectManager.doubletext.transform.parent.parent.gameObject.SetActive(false);
            }
            playerObjectManager.isPowerActive = true;
            StartCoroutine(WaitForNextPowerOn(PreferenceManager.instance.PowerDouble));
        }
    }

    public void SpeedPowerActive()
    {
        if (speedcounter > 0) 
        { 
            speedcounter--;
            playerObjectManager.speedtext.text = speedcounter.ToString("D2");
            playerObjectManager.isPlayerSpeedUp = true;
            if (speedcounter == 0)
            {
                isSpeedPowerCollected = false;
              //  playerObjectManager.speedtext.transform.parent.parent.gameObject.SetActive(false);
            }
            playerObjectManager.isPowerActive = true;
            StartCoroutine(WaitForNextPowerOn(PreferenceManager.instance.PowerSpeed));
        }
    }

    public void PowerBoxActive()
    {
        if(redboxcounter > 0)
        {
            redboxcounter--;
            playerObjectManager.redBoxtext.text = redboxcounter.ToString("D2");
            playerObjectManager.isPlayerPowerOn = true;
            playerObjectManager.powerBox.SetActive(true);

            if (redboxcounter == 0)
            {
                isRedPowerCollected = false;
               // playerObjectManager.redBoxtext.transform.parent.parent.gameObject.SetActive(false);
            }
            playerObjectManager.isPowerActive = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(WaitForNextPowerOn(PreferenceManager.instance.PowerBox));
        }
    }

    IEnumerator WaitForNextPowerOn(string power)
    {
        CanvasManager.instance.isTimerOn = true;
        CanvasManager.instance.timeRemaining = 5;
        MoveManager.instance.StartGameAgin_PowerCanvasOff();
        yield return new WaitForSeconds(5f);
        playerObjectManager.isPowerActive = false;
        switch (power)
        {
            case "PowerBox":
                playerObjectManager.isPlayerPowerOn = false;
                playerObjectManager.powerBox.SetActive(false);
              //  yield return new WaitForSeconds(0.5f);
                GetComponent<BoxCollider2D>().isTrigger = false;
                gameObject.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
                break;

            case "PowerSpeed":
                playerObjectManager.isPlayerSpeedUp = false;
                break;

            case "PowerDouble":
                playerObjectManager.isPlayerDouble = false;
                break;

            default:
                break;
        }
    }
}
