using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferenceManager : MonoBehaviour
{
    public static PreferenceManager instance;
    [SerializeField] bool deleteAll;

    [Header("Tags")]
    public string PowerBox = "PowerBox";
    public string PowerSpeed = "PowerSpeed";
    public string PowerDouble = "PowerDouble";
    public string PowerBomb = "PowerBomb";
    public string BoxCover = "BoxCover";

    public string isLogin = "isLogin";
    public string identityToken = "identityToken";
    public string userEmail = "userEmail";
    public string userName = "userName";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (deleteAll) PlayerPrefs.DeleteAll();
        SetToken("eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiODA5OTM1MjgxMmRkYTBkNzg2MTNjZDAzYTA0YTc2M2M0MjAxZThjN2E0ODA1ZWNjZTgwODlmNDdjOTZkMTdkZDhlNDdmYjkxZDI3ZDY5YmMiLCJpYXQiOjE2NjAwNjI3MDYuNDE5MTIsIm5iZiI6MTY2MDA2MjcwNi40MTkxMjUsImV4cCI6MTY5MTU5ODcwNi40MTYzODcsInN1YiI6IjQyIiwic2NvcGVzIjpbXX0.BGEdam5VdTgZf7CH5XlMRLIH7UaO4Dwhpq5dJCS-3H9f9g7wds-8QFEjiPYMpy4RPt48ldlRw_L9dKIfEXqNj67p-FG1n17xN2RdCQQ4MrnWsqTuuEGlxtLMog7RzNjaSbFlTzQM2zib_-cs0ZdegaoBIPNpJMnRCxZTnxCECsqmjAcpy7qDBSwI-nq5tlOGL8OILwsFl_TdG5iqdn-dcpT60mKumFK1Jk1dLUeCel_GcUzKEUeaGL1OWze0FmZri1VuMep7u8iQbSu21ikqjKC87zHmvnwWji5rRRZpGwjQkpiDvrG39a0sbXqImUxNdsV3ijJr1PgYBaKeNfjETDsQyghEuRvJsp_7rAF2LbPkz7YM-uiZhCQEZoRZIOkoQOmrFivx6eD81818UH3bWOpSP94z0rfRBl9TeJ6HzYFVMQgBx4ItRSQj84dpyvpghO2y6DwtUhKadjOvzGo-8_vTzklwlHqCoNr3DprcVyHSgCnWFFslsqRjo4EqavtA8alLOq53kuukid-w293_FLMVH_NV-o3EeiRhsnVx0-aawu1DyeMAIkuyhGREhfcviYecmsgxVd9gDzo7Qs9xCOnnSevhlPC_t9PCW7R3iN_N-e7NXLRP6ZNLLY6CQurlF37AC5JHCNPM9wiGPI23-kKZbWPoonK1MG5ZVF_MWJs");
    }

    public void SetToken(string token)
    {
        PlayerPrefs.SetString("User_token", token);
    }
    public string GetToken()
    {
        return PlayerPrefs.GetString("User_token");
    }
    public void SetSound(int sound)
    {
        PlayerPrefs.SetInt("Sound", sound);
    }

    public int GetSound()
    {
        return PlayerPrefs.GetInt("Sound");
    }
    public void SetMusic(int music)
    {
        PlayerPrefs.SetInt("Music", music);
    }

    public int GetMusic()
    {
        return PlayerPrefs.GetInt("Music");
    }
    public void SetBestScore(int bestScore)
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestScore");
    }

    public void SetBestTimetaken(int time)
    {
        PlayerPrefs.SetInt("Timetaken", time);
    } 

    public int GetTimetaken()
    {
        return PlayerPrefs.GetInt("Timetaken");
    }

    public void SetLonginStatus(int val)
    {
        PlayerPrefs.SetInt(isLogin, val);
    }

    public int GetLonginStatus()
    {
        return PlayerPrefs.GetInt(isLogin);
    }

    /// <summary>
    /// Save User Email
    /// </summary>
    /// <param name="_email"></param>
    public void SetUserEmail(string _email)
    {
        PlayerPrefs.SetString(userEmail, _email);
    }
    public string GetUserEmail()
    {
        return PlayerPrefs.GetString(userEmail);
    }

    /// <summary>
    /// Save User Name
    /// </summary>
    /// <param name="_email"></param>
    public void SetUserName(string _name)
    {
        PlayerPrefs.SetString(userName, _name);
    }
    public string GetUserName()
    {
        return PlayerPrefs.GetString(userName);
    }

    /// <summary>
    /// Save User identityToken
    /// </summary>
    /// <param name="_email"></param>
    public void SetUseridentityToken(string _identityToken)
    {
        PlayerPrefs.SetString(identityToken, _identityToken);
    }
    public string GetUseridentityToken()
    {
        return PlayerPrefs.GetString(identityToken);
    }

    /// <summary>
    /// First step for Tank tutorial
    /// </summary>
    public void SetTutorialDoubleTapOnTank()
    {
        PlayerPrefs.SetInt("DoubleTap", 1);
    }

    public int GetTutorialDoubleTapOnTank()
    {
        return PlayerPrefs.GetInt("DoubleTap");
    }

    public void SetTutorialOnTank()
    {
        PlayerPrefs.SetInt("powerSelected", 1);
    }

    public int GetTutorialOnTank()
    {
        return PlayerPrefs.GetInt("powerSelected");
    }
}
