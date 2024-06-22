using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserData : MonoBehaviour
{
    public static UserData instance;
    private JsonData jsonData;
    private string jsonString;

    public UserSO _userSO;

    private string social_id;
    private string social_type;
    private string username;
    private string email;
    string device_token;
    string userPath;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
       GetUserData();
    }
    public void GetUserData()
    {
        Debug.Log("UserDataEnterAPI");
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
        social_id = "1234569";
        username = "mahes25";
        email = "mahesh25@gmail.com";
      //  byte[] token = UnityEngine.iOS.NotificationServices.deviceToken;
       /* if (token != null)
        {*/
            device_token = "fWCXjesBRpmY5S-Co0Yrim:MCEsWu9YVdJYcduuRHAUFPHUR1j_XCP6DzavPgBy";
        //}

/*#if UNITY_EDITOR
        social_id = "1234569";
        username = "mahes25";
        email = "mahesh25@gmail.com";
        byte[] token = UnityEngine.iOS.NotificationServices.deviceToken;
        if (token != null)
        {
            device_token = "fWCXjesBRpmY5S-Co0Yrim:MCEsWu9YVdJYcduuRHAUFPHUR1j_XCP6DzavPgBy";
        }

#elif UNITY_IPHONE || PLATFORM_IOS || UNITY_IOS
        social_id = PreferenceManager.instance.GetUseridentityToken();
        username = PreferenceManager.instance.GetUserName();
        email = PreferenceManager.instance.GetUserEmail();
         byte[] token = UnityEngine.iOS.NotificationServices.deviceToken;
        if (token != null)
        {
            device_token = System.BitConverter.ToString(token).Replace("-", "");
        }
#endif*/

        userPath = Api_Constant.API_PATH + Api_Constant.LOGIN_SIGNUP; // profile_url = profile_url + id;
        Debug.Log(userPath);
        WWWForm form = new WWWForm();
        Debug.Log(social_id);
        Debug.Log(username);
        Debug.Log(email);
        //Debug.Log(device_token.ToString());
        Debug.Log(SystemInfo.deviceType.ToString());
        Debug.Log(SystemInfo.deviceName.ToString());
        Debug.Log(SystemInfo.deviceUniqueIdentifier);
        Debug.Log(Application.version);
        Debug.Log(SystemInfo.systemMemorySize.ToString());
        Debug.Log(SystemInfo.operatingSystem.ToString());
        
        form.AddField("social_id", social_id);
        form.AddField("social_type", "apple");
        form.AddField("username", username);
        form.AddField("email", email);
      //  form.AddField("device_token", device_token);
        form.AddField("device_type", SystemInfo.deviceType.ToString());
        form.AddField("device_name", SystemInfo.deviceName.ToString());
        form.AddField("device_unique_id", SystemInfo.deviceUniqueIdentifier);
        form.AddField("app_version", Application.version);
        form.AddField("device_ram", SystemInfo.systemMemorySize.ToString());
        form.AddField("device_os", SystemInfo.operatingSystem.ToString());
        UnityWebRequest webRequest = UnityWebRequest.Post(userPath, form);
        webRequest.SetRequestHeader("Accept", "application/json");
       // webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
        {
            Debug.Log(": Error: " + webRequest.error);
        }
        else
        {
            jsonString = webRequest.downloadHandler.text;
            Debug.Log("GetData: " + jsonString);
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
                                        pass_jsonData["message"].ToString(),
                                        pass_jsonData["token"].ToString());

        ApiValueStore.instance.wallet = (int)pass_jsonData["data"]["wallet"];
        ApiValueStore.instance.remain_ticket = (int)pass_jsonData["data"]["tickets"]["remain_ticket"];
        ApiValueStore.instance.userName = pass_jsonData["data"]["username"].ToString();
        _userSO = userSO;
        ApiValueStore.instance.token = _userSO.Token;
        PreferenceManager.instance.SetToken(_userSO.Token);

        // after login jump to leaderboard panel
        if (SceneManager.GetActiveScene().name == "GamePlayScene")
        {
            ApiValueStore.instance.bestScoreGetData.GetBestScoreData((isDone) =>
            {
                //if(isDone)
                   // CanvasManager.instance.leaderBoardUI.gameObject.SetActive(true);
            });
        }
        
        else
        {
            MenuManager.instance.profileIcon.SetActive(true);
            MenuManager.instance.profileIcon.GetComponentInChildren<Text>().text = PreferenceManager.instance.GetUserName();
            MenuManager.instance.amountObj.SetActive(true);
            MenuManager.instance.amountObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = ApiValueStore.instance.wallet.ToString();
            MenuManager.instance.ticketsObj.SetActive(true);
            MenuManager.instance.ticketsObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = ApiValueStore.instance.remain_ticket.ToString();

            MenuManager.instance.CheckLoginButton(false);
        }
        //StartCoroutine(SaveBestScoreSO(_best3scoreData,_top4scoreData));
    }
}
