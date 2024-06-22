using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetTicketsData : MonoBehaviour
{
    private JsonData jsonData;
    private string jsonString;

    public GetTicketsSO _getTicketsSO;

    string GetTicketsPath;

    private void Start()
    {
        Invoke("WaitForUserData",2f);
    }
    void WaitForUserData()
    {
         GetTicketData();
    }
    public void GetTicketData()
    {
        StartCoroutine(GetTickets((pass_jsonData, getData) =>
        {
            if (getData)
            {
                SaveGetTicketsData(pass_jsonData);
            }
        }));
    }

    private IEnumerator GetTickets(Action<JsonData, bool> callBack)
    {
        GetTicketsPath = Api_Constant.API_PATH + Api_Constant.GET_TICKETS;
        String token = PreferenceManager.instance.GetToken();

        UnityWebRequest webRequest = UnityWebRequest.Get(GetTicketsPath);
        webRequest.SetRequestHeader("Accept", "application/json");
       // webRequest.SetRequestHeader("Authorization", "Bearer " + '"'+ token + '"');
      //  webRequest.SetRequestHeader("Authorization", "Bearer " + "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiNmFmYWRkMDQzNzQzMjNlMjRiYzA0MTk3NjBiZjllMjNiN2RhNjI0ZTYzZDkzODA4Y2I3ZjZmNWVhM2IzZTNiN2I3OGViMDk4YTUwMjBkNTIiLCJpYXQiOjE2NjAzOTA5MzUuOTYyMzY1LCJuYmYiOjE2NjAzOTA5MzUuOTYyMzcsImV4cCI6MTY5MTkyNjkzNS45NTg3NTMsInN1YiI6IjQyIiwic2NvcGVzIjpbXX0.0zetoc9a7cKocUG4-ixgG9dW4ZctSMgCqK28Mey17NDtm1cgWN_UIk2AMOWuWdKbROH19jeea1W6b55JNbF8v1eD-wJP40yTU14vaMqbOBHDIRpE1HDyyqmAcYEuMQmesb3Plp-n0uxHflxMT9mFoz4M25RK0TBusIk4vnznTGESO62AXL-GBcRLirZNyXae7grc9gPKmMDY02tHlPkkqDAWxDhYKSy6EXvT_l-ossu1KinvQ2_11iT0LL3LHubgRpt6BDX521devcE6JYQropqgBPr2chNDzSTvstuvskJr_X_4Ul8A2qB58tZh9_9MM-bnZMJxI-bWsayxXaz5m6iC3PQgaOExkImr6nJA-uWpDlv4tuWmbllWAS5Plz6Bw4h759rdpLPqxC5KbG8P1-5BqW8KQPmK2ZSeMrguFeSHsIabUfwmMWIUKk04CC5QqXIkaMkRf3mqGoUERbmUehyk19SJdMFtyUzfJQ3etae_CTFNLY8Mgi3DxvEeKskDz8uOXYWiXMiZ_AS51ni2TF_tON_x7KoOQPyj11DEjXB7q491qJ7mzpF-6dR0eJ3d1SL-E03iblAoVGbfunFDwWFxdct-zeIOKKI7Dv9KYnLFscJEzp2yQDbS0Zy_JZzijp90c53Xzn3vgttSAQD2cpvDCzgwjvZHZy18MZpUgyo");
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
        }
    }

    private void SaveGetTicketsData(JsonData pass_jsonData)
    {
        List<Data> _Data = new List<Data>();
        Debug.Log("Counter - " + pass_jsonData["data"].Count);
        if (pass_jsonData["data"].Count > 0)
        {
              _Data = GetData(pass_jsonData["data"]);
        }
       
        StartCoroutine(SaveGetTicketSO((int)pass_jsonData["status"], _Data));
    }

    private List<Data> GetData(JsonData item)
    {
        List<Data> data = new List<Data>();
        JsonData msData = JsonMapper.ToObject(JsonMapper.ToJson(item));
        foreach (JsonData B3S in msData)
        {
            data.Add(
                new Data(
                    (int)B3S["id"],
                    (int)B3S["total_ticket"],
                    (int)B3S["total_price"],
                    B3S["created_at"].ToString(),
                    (int)B3S["is_best"]
                    ));
        }
     
        return data;
    }

    private IEnumerator SaveGetTicketSO(int id, List<Data> data)
    {
        _getTicketsSO = ScriptableObject.CreateInstance<GetTicketsSO>();

        GetTicketsSO getTicketsSO = new GetTicketsSO(id,data);
        _getTicketsSO = getTicketsSO;
        ApiValueStore.instance.getTicketsSO = _getTicketsSO;
        yield return null;
    }
}
