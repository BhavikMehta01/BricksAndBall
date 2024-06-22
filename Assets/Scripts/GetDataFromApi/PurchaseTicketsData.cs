using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PurchaseTicketsData : MonoBehaviour
{
    private JsonData jsonData;
    private string jsonString;

    public PurchaseTicketsSO _purchaseTicketsSO;

    string path;

    private void Start()
    {
        //GetPurchaseTicketsData();
    }
    public void GetPurchaseTicketsData()
    {
        StartCoroutine(GetBestScore((pass_jsonData, getData) =>
        {
            if (getData)
            {
                 SavePurchaseTicketsData(pass_jsonData);
            }
        }));
    }

    private IEnumerator GetBestScore(Action<JsonData, bool> callBack)
    {
        path = Api_Constant.API_PATH + Api_Constant.PURCHASE_TICKETS; // profile_url = profile_url + id;
        //  String token = PreferenceManager.instance.GetToken();
        //String token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiNmFmYWRkMDQzNzQzMjNlMjRiYzA0MTk3NjBiZjllMjNiN2RhNjI0ZTYzZDkzODA4Y2I3ZjZmNWVhM2IzZTNiN2I3OGViMDk4YTUwMjBkNTIiLCJpYXQiOjE2NjAzOTA5MzUuOTYyMzY1LCJuYmYiOjE2NjAzOTA5MzUuOTYyMzcsImV4cCI6MTY5MTkyNjkzNS45NTg3NTMsInN1YiI6IjQyIiwic2NvcGVzIjpbXX0.0zetoc9a7cKocUG4-ixgG9dW4ZctSMgCqK28Mey17NDtm1cgWN_UIk2AMOWuWdKbROH19jeea1W6b55JNbF8v1eD-wJP40yTU14vaMqbOBHDIRpE1HDyyqmAcYEuMQmesb3Plp-n0uxHflxMT9mFoz4M25RK0TBusIk4vnznTGESO62AXL-GBcRLirZNyXae7grc9gPKmMDY02tHlPkkqDAWxDhYKSy6EXvT_l-ossu1KinvQ2_11iT0LL3LHubgRpt6BDX521devcE6JYQropqgBPr2chNDzSTvstuvskJr_X_4Ul8A2qB58tZh9_9MM-bnZMJxI-bWsayxXaz5m6iC3PQgaOExkImr6nJA-uWpDlv4tuWmbllWAS5Plz6Bw4h759rdpLPqxC5KbG8P1-5BqW8KQPmK2ZSeMrguFeSHsIabUfwmMWIUKk04CC5QqXIkaMkRf3mqGoUERbmUehyk19SJdMFtyUzfJQ3etae_CTFNLY8Mgi3DxvEeKskDz8uOXYWiXMiZ_AS51ni2TF_tON_x7KoOQPyj11DEjXB7q491qJ7mzpF-6dR0eJ3d1SL-E03iblAoVGbfunFDwWFxdct-zeIOKKI7Dv9KYnLFscJEzp2yQDbS0Zy_JZzijp90c53Xzn3vgttSAQD2cpvDCzgwjvZHZy18MZpUgyo";
        //Debug.Log(token);
        WWWForm form = new WWWForm();
        form.AddField("ticket_id", ApiValueStore.instance.selected_ticket_id);
        form.AddField("paid_from", ApiValueStore.instance.paid_From.ToString());
        form.AddField("wallet_money_used", "0");
        UnityWebRequest webRequest = UnityWebRequest.Post(path, form);
        webRequest.SetRequestHeader("Accept", "application/json");
       // webRequest.SetRequestHeader("Authorization", "Bearer Token" + '"'+ token + '"');
     //   webRequest.SetRequestHeader("Authorization", "Bearer " + token );
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
            if (jsonData["success"].ToString().ToLower() == "true")
            {
                callBack(jsonData, true);
            }
        }
    }

    private void SavePurchaseTicketsData(JsonData pass_jsonData)
    {
      StartCoroutine(SavePurchaseTicketSO((int)pass_jsonData["data"]["user_id"],
                                        (int)pass_jsonData["data"]["tournament_id"],
                                        (int)pass_jsonData["data"]["total_ticket"],
                                        (int)pass_jsonData["data"]["used_ticket"],
                                        (int)pass_jsonData["data"]["remain_ticket"],
                                        pass_jsonData["data"]["created_at"].ToString(),
                                        pass_jsonData["data"]["updated_at"].ToString()
                                        ));
    }

    private IEnumerator SavePurchaseTicketSO(int user_id, int tournament_id, int total_ticket,
        int used_ticket,int remain_ticket, string created_at, string updated_at)
    {
        _purchaseTicketsSO = ScriptableObject.CreateInstance<PurchaseTicketsSO>();
        PurchaseTicketsSO purchaseTicketsSO = new PurchaseTicketsSO(user_id, tournament_id, total_ticket,
            used_ticket, remain_ticket, created_at,updated_at);
        ApiValueStore.instance.remain_ticket = remain_ticket;
        _purchaseTicketsSO = purchaseTicketsSO;
        CanvasManager.instance.PaymentProgressPanel.SetActive(true);
        yield return null;
    }
}
