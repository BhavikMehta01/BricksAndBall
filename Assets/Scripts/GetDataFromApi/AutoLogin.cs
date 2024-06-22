using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoLogin : MonoBehaviour
{
    public static AutoLogin instance;
    private JsonData jsonData;
    private string jsonString;

    public UserSO _userSO;


    string userPath;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
       // GetUserData();
    }
    public void GetUserData()
    {
        Debug.Log("Call AutoLogin");
        StartCoroutine(GetUser((pass_jsonData, getData) =>
        {
            if (getData)
            {
                SaveUserData(pass_jsonData);
            }
        }));
    }

    private IEnumerator GetUser(Action<JsonData, bool> callBack)
    {

        userPath = Api_Constant.API_PATH + Api_Constant.AUTO_LOGIN; // profile_url = profile_url + id;
        Debug.Log(Api_Constant.AUTO_LOGIN);
        WWWForm form = new WWWForm();
        form.AddField("token", PreferenceManager.instance.GetToken());
    
        UnityWebRequest webRequest = UnityWebRequest.Post(userPath, form);
        webRequest.SetRequestHeader("Accept", "application/json");
        webRequest.SetRequestHeader("Authorization", "Bearer " + PreferenceManager.instance.GetToken());
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
        {
            Debug.Log(": Error: " + webRequest.error);
        }
        else
        {
            jsonString = webRequest.downloadHandler.text;
            Debug.Log("Get Autologin Data: " + jsonString);
            jsonData = JsonMapper.ToObject(jsonString);
            if (jsonData["success"].ToString().ToLower() == "true")
            {
                callBack(jsonData, true);
            }
        }
    }

    private void SaveUserData(JsonData pass_jsonData)
    {
        tickets tickets = new tickets((int)pass_jsonData["data"]["tickets"]["total_ticket"],
                                      (int)pass_jsonData["data"]["tickets"]["used_ticket"],
                                      (int)pass_jsonData["data"]["tickets"]["remain_ticket"]);
        UserSO userSO = new UserSO((int)pass_jsonData["data"]["user_id"],
                                        pass_jsonData["data"]["username"].ToString(),
                                        pass_jsonData["data"]["email"].ToString(),
                                        pass_jsonData["data"]["wallet"].ToString(),
                                        pass_jsonData["data"]["sound"].ToString(),
                                        tickets,
                                        pass_jsonData["message"].ToString()
                                        );

        _userSO = userSO;
        //StartCoroutine(SaveBestScoreSO(_best3scoreData,_top4scoreData));
    }
}
