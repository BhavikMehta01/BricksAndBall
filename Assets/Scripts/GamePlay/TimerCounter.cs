using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCounter : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;

    public Text TimerTxt;

    void Start()
    {
        Time.timeScale = 1;
        TimerOn = true;
    }

    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.fixedUnscaledDeltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                TimeLeft = 0;
                TimerOn = false;
               // transform.parent.GetComponent<DisplayLeaderBoardValue>().getBatterScoreBtn.gameObject.SetActive(true);
                transform.parent.GetComponent<DisplayLeaderBoardValue>().activeScoreBtn.gameObject.SetActive(false);
                CanvasManager.instance.optionToPlay.SetActive(true);
                CanvasManager.instance.leaderBoardUI.gameObject.SetActive(false);

            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
} 
