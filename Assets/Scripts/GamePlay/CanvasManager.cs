using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    [Header("GamePlay")]
    public bool isPlaying;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject timerObj;
    [SerializeField] Text timerText;
    public bool isTimerOn;
    public float timeRemaining;
    public GameObject blurcanvas;
    public GameObject errorMsgPanel;
    public Text errorMsgPText;

    [Header("ScorePanel RoundComplete")]
    [SerializeField] GameObject scorePanel;
    [SerializeField] Text currentScoreText;
    [SerializeField] Text bestScoreOnScorePanelText;

    [Header("BestScore RoundComplete")]
    [SerializeField] GameObject bestScorePanel;
    [SerializeField] Text BestScoreOnBestScorePanelText;

    [Header("Main Objects")]
    [SerializeField] GameObject paddle;
    [SerializeField] GameObject enemyspawnObj;
    [SerializeField] GameObject powerspawnObj;
    [SerializeField] GameObject blastParticle;
    public GameObject handObj;
    public Text tutorialMsg;
    public bool isHandObjOn;

    [Header("Panel_Score")]
    public GameObject homrBtnObj;
    public GameObject retryBtnObj;

    [Header("LeaderBoad Panel")]
    public DisplayLeaderBoardValue leaderBoardUI;

    [Header("Confirm Panel")]
    public GameObject confirmPanel;


    [Header("Balance Panel")]
    public GameObject balancePanel;

    [Header("After Purchased Ticket")]
    public GameObject PaymentProgressPanel;
    public GameObject Panel_TicketPurchasePanel;
    public GameObject LeaderBoard_ActivedScorePanel;

    [Header("RankSignin Panel")]
    public GameObject RankSignInUI;
    [SerializeField] Button closeBtn;

    [Header("OptionToPlay")]
    public GameObject optionToPlay;

    private void Awake()
    {
        if (instance == null) instance = this;
        closeBtn.onClick.AddListener(() => SceneManager.LoadScene("MenuScene"));
    }
    private void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(Countdown(8));
    }
    // Update is called once per frame
    void Update()
    {
        if(isPlaying) scoreText.text = ShootManager.instance.score.ToString();
        if(isTimerOn)
        {
            timerObj.SetActive(true);
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTime(timeRemaining);
            }else
            {
                timeRemaining = 0;
                timerObj.SetActive(false);
                isTimerOn = false;
            }
        }

    }

    void UpdateTime(float currentTime)
    {
        currentTime += 1;
        float min = Mathf.FloorToInt(currentTime / 60);
        float sec = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}", min, sec);
    }

    public void SetScoreOnRoundCompeletePane()
    {
        blastParticle.SetActive(true);
       
        SoundManagerGame.instance.allSounds[2].Play();
        StartCoroutine(SetScore());
       
    }
    IEnumerator Countdown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
    }
    IEnumerator SetScore()
    {
        yield return new WaitForSecondsRealtime(0.8f);
        enemyspawnObj.SetActive(false);
        powerspawnObj.SetActive(false);
        currentScoreText.text = ShootManager.instance.score.ToString();
        if (PlayerPrefs.HasKey("BestScore"))
        {
            if (int.Parse(currentScoreText.text) > PreferenceManager.instance.GetBestScore())
            {
                bestScorePanel.SetActive(true);
                BestScoreOnBestScorePanelText.text = ShootManager.instance.score.ToString();
                PreferenceManager.instance.SetBestScore(int.Parse(currentScoreText.text));
            }
            else
            {
                scorePanel.SetActive(true);
            }
            bestScoreOnScorePanelText.text = PreferenceManager.instance.GetBestScore().ToString();
           // StartCoroutine(LoadLeaderBoard());
            Debug.Log("Score .. ");
            //action = true;
            //  Invoke("LoadLeaderBoard", 1f);
        }
        else
        {
            Debug.Log("Best Score Called");
            bestScorePanel.SetActive(true);
            BestScoreOnBestScorePanelText.text = ShootManager.instance.score.ToString();
            PreferenceManager.instance.SetBestScore(int.Parse(currentScoreText.text));
           // action = true;
        }
        if (MoveManager.instance.timeTaken > PreferenceManager.instance.GetTimetaken())
        {
            PreferenceManager.instance.SetBestTimetaken(MoveManager.instance.timeTaken);
        }

        StartCoroutine(BoxManager.instance.WaitForGameOverPanel());
    }

    public void ShowHandTutorialOnTank()
    {
        handObj.SetActive(true);
        isHandObjOn = true;
       // blurcanvas.SetActive(true);
        tutorialMsg.text = "Double tap on tank";
        PreferenceManager.instance.SetTutorialDoubleTapOnTank();
        Time.timeScale = 0;
       // StartCoroutine(StartGameAgine());
    }

    IEnumerator StartGameAgine()
    {
        yield return new WaitForSecondsRealtime(2f);
        handObj.SetActive(false);
      //  blurcanvas.SetActive(false);
        tutorialMsg.text = "";
        Time.timeScale = 1;
        paddle.GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator LoadLeaderBoard()
    {
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("loadLeaderBoard");
        leaderBoardUI.gameObject.SetActive(true);
    }
}
