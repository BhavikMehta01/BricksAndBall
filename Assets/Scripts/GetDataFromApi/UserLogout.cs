using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserLogout : MonoBehaviour
{
    private JsonData jsonData;
    private string jsonString;

   
    string userPath;

    public void Start()
    {
        //LogoutUser();
    }

    public void LogoutUser()
    {
        Debug.Log("Logout call");
        StartCoroutine(logout((pass_jsonData, getData) =>
        {
            if (getData)
            {
                PreferenceManager.instance.SetLonginStatus(0);
               // PreferenceManager.instance.SetUserEmail("");
               // PreferenceManager.instance.SetUserName("");
                MenuManager.instance.profileIcon.SetActive(false);
                MenuManager.instance.logoutConfirmPanel.SetActive(false);
                MenuManager.instance.CheckLoginButton(true);
            }
        }));
    }

    string score = 0.ToString();
    private IEnumerator logout(Action<JsonData, bool> callBack)
    {
        userPath = Api_Constant.API_PATH + Api_Constant.LOGOUT;
        Debug.Log(PreferenceManager.instance.GetToken());
        UnityWebRequest webRequest = UnityWebRequest.Post(userPath, PreferenceManager.instance.GetToken());
        webRequest.SetRequestHeader("Accept", "application/json");
        webRequest.SetRequestHeader("Authorization", "Bearer " + PreferenceManager.instance.GetToken());
       // webRequest.SetRequestHeader("Authorization", "Bearer " + "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiOGFiNDhjMmFlZDQ4YmRlYjE2NDBlNGM3MDk4M2UyNjdmMjI3MTY2N2ZkOGI5NzkzMjA4NDQxNGU1ODg1ZjI4N2IzMGJlNWFkN2Q1MDE5NWYiLCJpYXQiOjE2NTQxODIwNzAuMDM0ODM4LCJuYmYiOjE2NTQxODIwNzAuMDM0ODQzLCJleHAiOjE2ODU3MTgwNzAuMDMxNzM1LCJzdWIiOiIyNCIsInNjb3BlcyI6W119.vXo0hTQpHQlfUxPt0zXU1Afisq0U46b53G6VvTQPy51ndclmYBamdUiZv-Uc42Eju-gk-jXsjQP2n_F_eM7bQOtN8cEd7Wfnq1Kofy10nu7MkUxwNiR0u_w3Q7UPCjmYJQ5rQFIRbzEHcv5bEqRW7gPZUEXDGM8W17ZuJbcZqDGYI49LgWfPKb_ZSTma56DgnSBqZ2NgHDaMK2vG-8zr7pUQOJT3C6ToLwAS_AvoiE6vI963ejRh1JLcirdxZgnztFN1UXQvEGfqmWiUoMCyQQMClCAASYsoUjzwlqDBQOLen1qSiymQJlPtYlTwB3MHMWCSmB0FQuvwdghm_VaEM9n3Tk6abKhlsbKtkpzizn_nWJLgnEWTVjGvV1mFq4e3FhVA4n1vhOzjjwGeAgYhFHXySv44sjBr65tOypHG-dvbIaYogyYDUxH5esb8y76699zT9m3kQs4483GkjR3X0G2L6q5zLXl2qDxxg7I5CS0EviZbp0Kt5qQEGB1UE56XfOwiXMSx8LSmGb_SS2RAc-UfotpbCmDhaY5zMcvaE6e1c3HIh2HEpD2vvYNq2klmIXrwox-WntdBc9QSn_UTp4tTyxTkfX7-iBfeSZACk7Kkx_8ygj20H2LaebcSxZ-vOSTTfQ-Hi3Z_d2vSFYeZKMCIlbdYiTXsqdhFaPsf0vg");

        // webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
        {
            Debug.Log(": Error: " + webRequest.error);
        }
        else
        {
            jsonString = webRequest.downloadHandler.text;
            Debug.Log("Logout GetData: " + jsonString);
            jsonData = JsonMapper.ToObject(jsonString);
            if ((int)jsonData["status"] == 1)
            {
                callBack(jsonData, true);
            }
        }
    }
}
