using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Extensions;
using AppleAuth.Interfaces;
using AppleAuth.Native;

using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AppleSignIn : MonoBehaviour
{
    private IAppleAuthManager appleAuthManager;

 
    public string AppleUserIdKey { get; private set; }


    void Start()
    {
        // If the current platform is supported
        if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            // Creates a default JSON deserializer, to transform JSON Native responses to C# instances
            var deserializer = new PayloadDeserializer();
            // Creates an Apple Authentication manager with the deserializer
            this.appleAuthManager = new AppleAuthManager(deserializer);
        }
    }

    void Update()
    {
        // Updates the AppleAuthManager instance to execute
        // pending callbacks inside Unity's execution loop
        if (this.appleAuthManager != null)
        {
            this.appleAuthManager.Update();
        }
    }

    public string strName;
    public string strEmail;
    public string userID;

    public Text userName;
    public Text Email;
    public bool isRegistered;
    public void SignInButton()
    {
        var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);

        this.appleAuthManager.LoginWithAppleId(
            loginArgs,
            credential =>
            {
                // Obtained credential, cast it to IAppleIDCredential
                var appleIdCredential = credential as IAppleIDCredential;
                if (appleIdCredential != null)
                {
                    // Apple User ID
                    // You should save the user ID somewhere in the device
                    var userId = appleIdCredential.User;
                    PlayerPrefs.SetString(AppleUserIdKey, userId);

                    // Email (Received ONLY in the first login)
                    var email = appleIdCredential.Email;

                    Debug.Log("Email - " + email);
                    // Full name (Received ONLY in the first login)
                    var fullName = appleIdCredential.FullName;

                    // Identity token
                    var identityToken = Encoding.UTF8.GetString(
                        appleIdCredential.IdentityToken,
                        0,
                        appleIdCredential.IdentityToken.Length);
                    Debug.Log("identityToken - " + identityToken);

                   // PreferenceManager.instance.SetToken(identityToken);
                    // Authorization code
                    var authorizationCode = Encoding.UTF8.GetString(
                        appleIdCredential.AuthorizationCode,
                        0,
                        appleIdCredential.AuthorizationCode.Length);


                    userID = userId;
                    if (fullName.Nickname == "")
                    {
                        strName = fullName.GivenName;
                    }
                    else if(fullName.GivenName == "")
                    {
                        strName = fullName.Nickname;
                    }
                    else
                    {
                        strName = fullName.MiddleName;
                    }
                    strEmail = email;
                    if (userName) userName.text = strName;
                    if(Email) Email.text = email;

                   // Debug.Log("userName " + userName.text + " \n Email" + Email.text);

                    // Set Login Status
                    PreferenceManager.instance.SetLonginStatus(1);

                    // after login jump to leaderboard panel
                    if(SceneManager.GetActiveScene().name == "GamePlayScene")
                    {
                        CanvasManager.instance.leaderBoardUI.gameObject.SetActive(true);
                    }
                    isRegistered = true;
                    
                   // Invoke(nameof(CallSocialLogin), 1f);

                }
            },
            error =>
            {
                // Something went wrong
                isRegistered = false;
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
            });
    }
   /* public void CallSocialLogin()
    {
        if (isRegistered)
        {
            socialLogin.CallSocialLogin(userID, "Apple", strName, strEmail);
        }
        else
        {
            MyGameManager.Instance.DisplayMsgPopUp("Something Went Wrong, Please try again..");
        }
    }*/
}
