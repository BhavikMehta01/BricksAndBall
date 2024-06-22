using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RunningTournamentData : MonoBehaviour
{
    private JsonData jsonData;
    private string jsonString;

    public RunningTournamentSO _runningTournamentSO;

    string runningTournamentPath;

    private void Start()
    {
         //GetRunningTournamentData();
    }
    public void GetRunningTournamentData()
    {
        StartCoroutine(GetRunningTournament((pass_jsonData, getData) =>
        {
            if (getData)
            {
                SaveRunningTournamentData(pass_jsonData);
            }
        }));
    }
    string score = 0.ToString();
    private IEnumerator GetRunningTournament(Action<JsonData, bool> callBack)
    {
        runningTournamentPath = Api_Constant.API_PATH + Api_Constant.RUNNING_TOURNAMENT;

        String token = PreferenceManager.instance.GetToken();

        UnityWebRequest webRequest = UnityWebRequest.Get(runningTournamentPath);
        webRequest.SetRequestHeader("Accept", "application/json");
        // webRequest.SetRequestHeader("Authorization", "Bearer " + '"'+ token + '"');
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
            jsonData = JsonMapper.ToObject(jsonString);
            if (jsonData["success"].ToString().ToLower() == "true")
            {
                callBack(jsonData["data"]["running"], true);
            }
        }
    }

    private void SaveRunningTournamentData(JsonData pass_jsonData)
    {
      /*  List<Prizepoll> _prizepollData = new List<Prizepoll>();
        if (pass_jsonData["prize_poll"].Count > 0)
        {
              _prizepollData = GetPrizePollData(pass_jsonData["prize_poll"]);
        }
       */
        StartCoroutine(SaveSaveRunningTournamentSO((int)pass_jsonData["id"],
                                                   pass_jsonData["name"].ToString(), 
                                                   pass_jsonData["start_time"].ToString(),
                                                   pass_jsonData["end_time"].ToString(),
                                                   pass_jsonData["total_duration"].ToString(),
                                                   (int)pass_jsonData["ticket_used"],
                                                   (int)pass_jsonData["prize_pool"]));
    }

    private List<Prizepoll> GetPrizePollData(JsonData item)
    {
        List<Prizepoll> prizePoll = new List<Prizepoll>();
        JsonData msData = JsonMapper.ToObject(JsonMapper.ToJson(item));
        foreach (JsonData B3S in msData)
        {
            prizePoll.Add(
                new Prizepoll(
                    (int)B3S["id"],
                    (int)B3S["tournament_id"],
                    B3S["rank"].ToString(),
                    B3S["prize"].ToString(),
                    B3S["created_at"].ToString(),
                    B3S["updated_at"].ToString()
                    ));
        }
     
        return prizePoll;
    }

    private IEnumerator SaveSaveRunningTournamentSO(int id,string name,string start_time, string end_time,
                                                    string total_duration,int ticket_used, int prizepoll)
    {
        _runningTournamentSO = ScriptableObject.CreateInstance<RunningTournamentSO>();

        RunningTournamentSO runningTournamentSO = new RunningTournamentSO(id,name, start_time, end_time,total_duration,ticket_used, prizepoll);
        _runningTournamentSO = runningTournamentSO;

        ApiValueStore.instance.runningTournament_id = id;
        ApiValueStore.instance.prize_pool = prizepoll;

        yield return null;
    }
}
