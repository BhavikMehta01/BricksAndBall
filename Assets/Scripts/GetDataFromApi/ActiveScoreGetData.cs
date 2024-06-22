using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ActiveScoreGetData : MonoBehaviour
{
    private JsonData jsonData;
    private string jsonString;

    public ActivateScoreSO _activeScoreSO;

    string activeScorePath;

    private void Start()
    {
       // GetActiveScoreData();
    }
    public void GetActiveScoreData()
    {
        StartCoroutine(GetActiveScore((pass_jsonData, getData) =>
        {
            if (getData)
            {
                StartCoroutine(SaveActiveScoreSO((int)pass_jsonData["status"], (bool)pass_jsonData["success"], pass_jsonData["message"].ToString()));
            }
        }));
    }
   // string score = PreferenceManager.instance.GetBestScore().ToString();
  //  string timetaken = PreferenceManager.instance.GetTimetaken().ToString();
  //  string timetaken = 0.ToString();
    string tournament_id = 0.ToString();
    private IEnumerator GetActiveScore(Action<JsonData, bool> callBack)
    {
        activeScorePath = Api_Constant.API_PATH + Api_Constant.ACTIVATE_SCORE; // profile_url = profile_url + id;
        String token = PreferenceManager.instance.GetToken();
        Debug.Log("turnament_id - " + ApiValueStore.instance.runningTournament_id);
        WWWForm form = new WWWForm();
        form.AddField("score", PreferenceManager.instance.GetBestScore().ToString());
        form.AddField("timetaken", PreferenceManager.instance.GetTimetaken().ToString());
        form.AddField("is_free", "0");
        form.AddField("tournament_id", ApiValueStore.instance.runningTournament_id);
        UnityWebRequest webRequest = UnityWebRequest.Post(activeScorePath, form);
        webRequest.SetRequestHeader("Accept", "application/json");
        // webRequest.SetRequestHeader("Authorization", "Bearer " + '"'+ token + '"');
        //webRequest.SetRequestHeader("Authorization", "Bearer " + "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiNmFmYWRkMDQzNzQzMjNlMjRiYzA0MTk3NjBiZjllMjNiN2RhNjI0ZTYzZDkzODA4Y2I3ZjZmNWVhM2IzZTNiN2I3OGViMDk4YTUwMjBkNTIiLCJpYXQiOjE2NjAzOTA5MzUuOTYyMzY1LCJuYmYiOjE2NjAzOTA5MzUuOTYyMzcsImV4cCI6MTY5MTkyNjkzNS45NTg3NTMsInN1YiI6IjQyIiwic2NvcGVzIjpbXX0.0zetoc9a7cKocUG4-ixgG9dW4ZctSMgCqK28Mey17NDtm1cgWN_UIk2AMOWuWdKbROH19jeea1W6b55JNbF8v1eD-wJP40yTU14vaMqbOBHDIRpE1HDyyqmAcYEuMQmesb3Plp-n0uxHflxMT9mFoz4M25RK0TBusIk4vnznTGESO62AXL-GBcRLirZNyXae7grc9gPKmMDY02tHlPkkqDAWxDhYKSy6EXvT_l-ossu1KinvQ2_11iT0LL3LHubgRpt6BDX521devcE6JYQropqgBPr2chNDzSTvstuvskJr_X_4Ul8A2qB58tZh9_9MM-bnZMJxI-bWsayxXaz5m6iC3PQgaOExkImr6nJA-uWpDlv4tuWmbllWAS5Plz6Bw4h759rdpLPqxC5KbG8P1-5BqW8KQPmK2ZSeMrguFeSHsIabUfwmMWIUKk04CC5QqXIkaMkRf3mqGoUERbmUehyk19SJdMFtyUzfJQ3etae_CTFNLY8Mgi3DxvEeKskDz8uOXYWiXMiZ_AS51ni2TF_tON_x7KoOQPyj11DEjXB7q491qJ7mzpF-6dR0eJ3d1SL-E03iblAoVGbfunFDwWFxdct-zeIOKKI7Dv9KYnLFscJEzp2yQDbS0Zy_JZzijp90c53Xzn3vgttSAQD2cpvDCzgwjvZHZy18MZpUgyo");
        webRequest.SetRequestHeader("Authorization", "Bearer " + ApiValueStore.instance.token);
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
            if ((int)jsonData["status"] == 1)
            {
                callBack(jsonData, true);
            }
            else
            {
                Debug.Log("Active score = 0");
            }
        }
    }
    private IEnumerator SaveActiveScoreSO(int status, bool success, string msg)
    {
        _activeScoreSO = ScriptableObject.CreateInstance<ActivateScoreSO>();

        ActivateScoreSO activeScoreSO = new ActivateScoreSO(status,success,msg);
        _activeScoreSO = activeScoreSO;

        yield return null;
    }
}
