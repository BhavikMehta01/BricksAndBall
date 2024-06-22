using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManagerGame : MonoBehaviour
{
    public static SoundManagerGame instance;

    [Header("AudioSource")]
    public AudioSource[] allSounds;

    public AudioSource bgMusic;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ManageAudioVolume();
        ManageBGVolume();
    }

    public void ManageAudioVolume()
    {
        if (PreferenceManager.instance.GetSound() == 0)
        {
            foreach (AudioSource _AS in allSounds)
            {
                _AS.mute = false;
            }
        }
        else if (PreferenceManager.instance.GetSound() == 1)
        {
            foreach (AudioSource _AS in allSounds)
            {
                _AS.mute = true;
            }
        }
    }
    public void ManageBGVolume()
    {
        if (PreferenceManager.instance.GetMusic() == 0)
        {
            bgMusic.mute = false;
        }
        else if (PreferenceManager.instance.GetMusic() == 1)
        {
            bgMusic.mute = true;
        }
    }

    public void RestartGame() {
        Time.timeScale = 1;
        BoxManager.instance.gameEnd = false;
        SceneManager.LoadScene("GamePlayScene");
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        BoxManager.instance.gameEnd = false;
        SceneManager.LoadScene("MenuScene");
    }
}
