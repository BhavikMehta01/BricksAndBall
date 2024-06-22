using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLeaderBoardValue : MonoBehaviour
{
    public Button getBatterScoreBtn;
    public Button activeScoreBtn;
    public Button playBtn;
    [SerializeField] Text amount;
    [SerializeField] Text avilableTicket;
    [SerializeField] GameObject blueBanner;
    [SerializeField] GameObject yellowBanner;
    [SerializeField] GameObject redBanner;
    [SerializeField] GameObject greenBanner;
    [SerializeField] Transform scoreBannerParents;
    [SerializeField] GameObject timerleft;
    [SerializeField] List<UITop3Player> uITop3Players = new List<UITop3Player>();
  //  [SerializeField] List<UITop4Player> uITop4Players = new List<UITop4Player>();

    private void OnEnable()
    {
        getBatterScoreBtn.gameObject.SetActive(false);
        activeScoreBtn.gameObject.SetActive(false);
        playBtn.gameObject.SetActive(false);
        amount.text = ApiValueStore.instance.wallet.ToString();
        avilableTicket.text = ApiValueStore.instance.remain_ticket.ToString();
        Debug.Log("is_eligible_for_prize - " + ApiValueStore.instance.is_eligible_for_prize);
        if (ApiValueStore.instance.is_eligible_for_prize == 1)
        {
            if (ApiValueStore.instance.isPurshceTicket)
            {
                playBtn.gameObject.SetActive(true);
            }
            else
            {
                activeScoreBtn.gameObject.SetActive(true);
                activeScoreBtn.onClick.AddListener(ActiveScorePrecess);
                timerleft.SetActive(true);
            }
        }
        else
        {
            getBatterScoreBtn.gameObject.SetActive(true);
            //getBatterScoreBtn.onClick.AddListener(RePlay);
        }
        SetBestScoreInUI(ApiValueStore.instance.bestScoreGetData._bestScoreSO);
    }


    public void SetBestScoreInUI(BestScoreSO bestScoreSO)
    {
        if(scoreBannerParents.childCount > 0)
        {
            foreach (GameObject item in scoreBannerParents)
            {
                Destroy(item);
            }
        }
        for (int i = 0; i < uITop3Players.Count; i++)
        {
            uITop3Players[i].score.text = bestScoreSO.best3Score[i].Score;
            uITop3Players[i].name.text = bestScoreSO.best3Score[i].Username;
        }
        if(bestScoreSO.top4Score.Count > 0)
        {
            for (int i = 0; i < bestScoreSO.top4Score.Count; i++)
            {
                if(bestScoreSO.top4Score[i].Present == 0)
                {
                    GameObject _bBanner = Instantiate(blueBanner, scoreBannerParents);
                    _bBanner.transform.GetChild(0).GetComponent<Text>().text = bestScoreSO.top4Score[i].Username;
                    _bBanner.transform.GetChild(1).GetComponent<Text>().text = bestScoreSO.top4Score[i].Score;
                    _bBanner.transform.GetChild(2).GetComponent<Text>().text = bestScoreSO.top4Score[i].Prize.ToString();
                }
                else
                {
                    if (ApiValueStore.instance.isPurshceTicket)
                    {
                         GameObject _bBanner = Instantiate(greenBanner, scoreBannerParents);
                        _bBanner.transform.GetChild(0).GetComponent<Text>().text = bestScoreSO.top4Score[i].Username;
                        _bBanner.transform.GetChild(1).GetComponent<Text>().text = bestScoreSO.top4Score[i].Score;
                        _bBanner.transform.GetChild(2).GetComponent<Text>().text = bestScoreSO.top4Score[i].Prize.ToString();
                    }
                    else
                    {
                        if(bestScoreSO.is_eligible_for_prize == 1)
                        {
                            GameObject _bBanner = Instantiate(redBanner, scoreBannerParents);
                            _bBanner.transform.GetChild(0).GetComponent<Text>().text = bestScoreSO.top4Score[i].Username;
                            _bBanner.transform.GetChild(1).GetComponent<Text>().text = bestScoreSO.top4Score[i].Score;
                            _bBanner.transform.GetChild(2).GetComponent<Text>().text = bestScoreSO.top4Score[i].Prize.ToString();
                        }else if(bestScoreSO.is_eligible_for_prize == 0)
                        {
                            GameObject _bBanner = Instantiate(yellowBanner, scoreBannerParents);
                            _bBanner.transform.GetChild(0).GetComponent<Text>().text = bestScoreSO.top4Score[i].Username;
                            _bBanner.transform.GetChild(1).GetComponent<Text>().text = bestScoreSO.top4Score[i].Score;
                            _bBanner.transform.GetChild(2).GetComponent<Text>().text = bestScoreSO.top4Score[i].Prize.ToString();
                        }
                    }
                }
            /*    uITop4Players[i].score.text = bestScoreSO.top4Score[i].Score;
                uITop4Players[i].name.text = bestScoreSO.top4Score[i].Username;*/
            }
        }
    }
  
    void ActiveScorePrecess()
    {
        Time.timeScale = 1f;
        CanvasManager.instance.confirmPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public class UITop3Player
{
    public Text score;
    public Text name;
}

[System.Serializable]
public class UITop4Player
{
    public Text score;
    public Text name;
}