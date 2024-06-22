using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [SerializeField]
    private GameObject settingPanel, infoPannel, completeInPanel, crowdfundedPanel;
    public GameObject profileIcon;
    public GameObject amountObj;
    public GameObject ticketsObj;
    public GameObject SignInButton;
    public GameObject LogoutButton;

    public GameObject logoutConfirmPanel;

    [SerializeField]
    private Slider withdrawAmountSlider;

    [SerializeField]
    private Text withdrawAmountText;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        withdrawAmountSlider.onValueChanged.AddListener((v) => {

            withdrawAmountText.text = v.ToString("$0.00");
        });

        if (PlayerPrefs.HasKey(PreferenceManager.instance.isLogin))
        {
            Debug.Log(PreferenceManager.instance.GetLonginStatus());
            if(PreferenceManager.instance.GetLonginStatus() == 1)
            {
                profileIcon.SetActive(true);
                profileIcon.GetComponentInChildren<Text>().text = PreferenceManager.instance.GetUserName();
                CheckLoginButton(false);
                AutoLogin.instance.GetUserData();
            }
            else
            {
                profileIcon.SetActive(false);
                CheckLoginButton(true);
            }
        }
        else
        {
            profileIcon.SetActive(false);
        }
    }

    public void CheckLoginButton(bool isOn)
    {
        SignInButton.SetActive(isOn);
        LogoutButton.SetActive(!isOn);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SettingPanelOpen()
    {
        SoundManagerMenu.instance.allSounds[0].Play();
        settingPanel.SetActive(true);
    }
    public void SettingPanelClose() 
    {
        settingPanel.SetActive(false);
    }

    public void InfoPanelOpen() 
    {
        infoPannel.SetActive(true);
    }

    public void InfoPanelClose() 
    {
        infoPannel.SetActive(false);
    }

    public void CompleteInOpen() 
    {
        SoundManagerMenu.instance.allSounds[0].Play();
        completeInPanel.SetActive(true);
    }
    public void CompleteInClose()
    {
        completeInPanel.SetActive(false);
    }
    public void CrowdfundedOpen()
    {
       
        crowdfundedPanel.SetActive(true);
    }
    public void CrowdfundedClose()
    {
        crowdfundedPanel.SetActive(false);
    }

    public void PlayGame() 
    {
        SoundManagerMenu.instance.allSounds[0].Play();
        SceneManager.LoadScene("GamePlayScene");  
    }
    public void MenuSceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShareMenu()
    {
        StartCoroutine(LoadImageAndShare());
    }
    public void NextButton(GameObject nextPannel)
    {
        nextPannel.SetActive(true);
    }
    IEnumerator LoadImageAndShare()
    {
        Texture2D image = Resources.Load("image", typeof(Texture2D)) as Texture2D;
        yield return null;

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, image.EncodeToPNG());

        new NativeShare().AddFile(filePath).SetText("<b>Ball and Bricks Cash </b></n>I scored 1000 points in my game.").SetUrl("https://www.google.com").Share();
    }
}
