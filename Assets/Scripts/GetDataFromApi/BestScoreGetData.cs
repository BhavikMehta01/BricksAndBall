using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BestScoreGetData : MonoBehaviour
{
    private JsonData jsonData;
    private string jsonString;

    public BestScoreSO _bestScoreSO;

    string bestScorePath;

    private void Start()
    {
        //GetBestScoreData();
    }
    public void GetBestScoreData(Action<bool> callback)
    {
        StartCoroutine(GetBestScore((pass_jsonData, getData) =>
        {
            if (getData)
            {
                SaveBestScoreData(pass_jsonData,(isDone) =>
                {
                    callback(getData);
                    ApiValueStore.instance.runningTournamentData.GetRunningTournamentData();
                });
            }
        }));
    }
    string score = 500.ToString();
    string timetaken = 200.ToString();
    private IEnumerator GetBestScore(Action<JsonData, bool> callBack)
    {
        bestScorePath = Api_Constant.API_PATH + Api_Constant.BEST_SCORE; // profile_url = profile_url + id;
        //  String token = PreferenceManager.instance.GetToken();
        //String token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiNmFmYWRkMDQzNzQzMjNlMjRiYzA0MTk3NjBiZjllMjNiN2RhNjI0ZTYzZDkzODA4Y2I3ZjZmNWVhM2IzZTNiN2I3OGViMDk4YTUwMjBkNTIiLCJpYXQiOjE2NjAzOTA5MzUuOTYyMzY1LCJuYmYiOjE2NjAzOTA5MzUuOTYyMzcsImV4cCI6MTY5MTkyNjkzNS45NTg3NTMsInN1YiI6IjQyIiwic2NvcGVzIjpbXX0.0zetoc9a7cKocUG4-ixgG9dW4ZctSMgCqK28Mey17NDtm1cgWN_UIk2AMOWuWdKbROH19jeea1W6b55JNbF8v1eD-wJP40yTU14vaMqbOBHDIRpE1HDyyqmAcYEuMQmesb3Plp-n0uxHflxMT9mFoz4M25RK0TBusIk4vnznTGESO62AXL-GBcRLirZNyXae7grc9gPKmMDY02tHlPkkqDAWxDhYKSy6EXvT_l-ossu1KinvQ2_11iT0LL3LHubgRpt6BDX521devcE6JYQropqgBPr2chNDzSTvstuvskJr_X_4Ul8A2qB58tZh9_9MM-bnZMJxI-bWsayxXaz5m6iC3PQgaOExkImr6nJA-uWpDlv4tuWmbllWAS5Plz6Bw4h759rdpLPqxC5KbG8P1-5BqW8KQPmK2ZSeMrguFeSHsIabUfwmMWIUKk04CC5QqXIkaMkRf3mqGoUERbmUehyk19SJdMFtyUzfJQ3etae_CTFNLY8Mgi3DxvEeKskDz8uOXYWiXMiZ_AS51ni2TF_tON_x7KoOQPyj11DEjXB7q491qJ7mzpF-6dR0eJ3d1SL-E03iblAoVGbfunFDwWFxdct-zeIOKKI7Dv9KYnLFscJEzp2yQDbS0Zy_JZzijp90c53Xzn3vgttSAQD2cpvDCzgwjvZHZy18MZpUgyo";
        //Debug.Log(token);
        WWWForm form = new WWWForm();
        form.AddField("score", PreferenceManager.instance.GetBestScore().ToString());
        form.AddField("timetaken", PreferenceManager.instance.GetTimetaken().ToString());
        UnityWebRequest webRequest = UnityWebRequest.Post(bestScorePath, form);
        webRequest.SetRequestHeader("Accept", "application/json");
        webRequest.SetRequestHeader("Authorization", "Bearer " + ApiValueStore.instance.token);
      //  webRequest.SetRequestHeader("Authorization", "Bearer " + "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiNmFlYTQxZGI5MDUxMGEwMmJmZWVjYzQwMDU5OGUxODU2OGYxYzkwMDkyOTRjNTNlYTY1ZmE0N2Y0NDFhNzY5OGIzNTE1ZDE5NWY5MzhhNjkiLCJpYXQiOjE2NjA3MDg1OTQuODMxMjM5LCJuYmYiOjE2NjA3MDg1OTQuODMxMjQzLCJleHAiOjE2OTIyNDQ1OTQuODI4Mzg5LCJzdWIiOiI0MiIsInNjb3BlcyI6W119.RRS7IMem3GQYBy8-9jZeZOyPjXFBIppDsa-eH5sPW1CluBV8VX4zViYILyXGl5zhlivWPLUoHacZFIF7tJQsgdVPs4sSPhbMJS1n6neYifsy-M6PQG_xEHtsCY-XlMKQzfqjZlyecawCc4iK_MPnjJGktPLnJZ5I02YD19jVjno0N4OfvS2pcCih1k7w0TepJNJb-XP6-05kN3Xxm2MZJbKewYHZVT2y1EhTu6kQ3dtw1aC6wCH0SorxF3c2FOjzTTJoLN9Gpp3ZF2q4qg1ZECNddDbkZ8Z5t7mzpxpSb54NfcdFf-yZgT_SHHIU9pR6ZFDpkhQzP-C3sSQTrbPQBthJfQ50P6EQ76YyagmrSVmokJ8pSNynRZBtAIp1JdSkOY_emzIsmlFMVSQzMzcPNOxBVp969071LMk9-XJb73vdmc2h2rWdTFLfAmKaVBgpsxoZCpnUc5MJabZouY_AqC93gNiO7QY95Qn_j4w-yk4c0WH1U8ryVA68JpPrJUatFKCQ1rtsG-UEe568PvKJeSv5nup4WaLfqM2sGzuGjkm6y9nnO6cPfhOTF-FmB3sfQp2DHLzW-rZsS1mPOGEKD_b9R5SnijVK_ilfheHijH8UxkOU9B3r2_1RLyn78sT4T130VhlLdLiBveDSRBMRiE6ml8OBA_8A012sdHt_uLM");
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
        {
            Debug.Log(": Error: " + webRequest.error);
        }
        else
        {
            jsonString = webRequest.downloadHandler.text;
            Debug.Log("GetBestScoreData: " + jsonString);
            jsonData = JsonMapper.ToObject(jsonString);
            if (jsonData["success"].ToString().ToLower() == "true")
            {
                callBack(jsonData, true);
            }
        }
    }

    private void SaveBestScoreData(JsonData pass_jsonData,Action<bool> callback)
    {
        List<Best3Score> _best3scoreData = new List<Best3Score>();
        if (pass_jsonData["data"]["leader_board"].Count > 0)
        {
              _best3scoreData = GetBest3ScoreData(pass_jsonData["data"]["leader_board"][0]);
        }

        List<Top4Score> _top4scoreData = new List<Top4Score>();
        if (pass_jsonData["data"]["leader_board"].Count > 0)
        {
         
              _top4scoreData = GetTop4ScoreData(pass_jsonData["data"]["leader_board"][1]);
        }

          StartCoroutine(SaveBestScoreSO((int)pass_jsonData["data"]["is_my_best"],
                                        (int)pass_jsonData["data"]["is_eligible_for_prize"],
                                        (int)pass_jsonData["data"]["is_best"],
                                        (int)pass_jsonData["data"]["best_score"],
                                        _best3scoreData,_top4scoreData,callback));
    }

    private List<Best3Score> GetBest3ScoreData(JsonData item)
    {
        List<Best3Score> _3score = new List<Best3Score>();
        JsonData msData = JsonMapper.ToObject(JsonMapper.ToJson(item));
        foreach (JsonData B3S in msData)
        {
            _3score.Add(
                new Best3Score(
                    B3S["score"].ToString(),
                    B3S["timetaken"].ToString(),
                    (int)B3S["tournament_id"],
                    (int)B3S["user_id"],
                    B3S["username"].ToString(),
                    (int)B3S["present"],
                    (int)B3S["rank"],
                    (int)B3S["prize"]
                    ));
        }
        return _3score;
    }

    private List<Top4Score> GetTop4ScoreData(JsonData item)
    {
        List<Top4Score> _4score = new List<Top4Score>();
        JsonData msData = JsonMapper.ToObject(JsonMapper.ToJson(item));
        foreach (JsonData B3S in msData)
        {
            _4score.Add(
                new Top4Score(
                    B3S["score"].ToString(),
                    B3S["timetaken"].ToString(),
                    (int)B3S["tournament_id"],
                    (int)B3S["user_id"],
                    B3S["username"].ToString(),
                    (int)B3S["present"],
                    (int)B3S["rank"],
                    (int)B3S["prize"]
                    ));
        }
        return _4score;
    }

    private IEnumerator SaveBestScoreSO(int is_my_best, int is_eligible_for_prize,int is_best,
        int best_score,List<Best3Score> best3scoreData, List<Top4Score> top4scoreData,Action<bool> callback)
    {
        _bestScoreSO = ScriptableObject.CreateInstance<BestScoreSO>();
      //  Debug.Log("Username" + top4scoreData[0].Username);
        BestScoreSO bestScoreSO = new BestScoreSO(is_my_best, is_eligible_for_prize, is_best,
            best_score,best3scoreData, top4scoreData);
        _bestScoreSO = bestScoreSO;
        ApiValueStore.instance.is_eligible_for_prize = _bestScoreSO.Is_eligible_for_prize;
        CanvasManager.instance.leaderBoardUI.SetBestScoreInUI(_bestScoreSO);
        callback(true);
        yield return null;
    }
}
