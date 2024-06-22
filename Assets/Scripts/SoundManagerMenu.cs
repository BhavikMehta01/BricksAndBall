using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagerMenu : MonoBehaviour
{
    public static SoundManagerMenu instance;

    [Header("UI Objects")]
    [SerializeField] private Image sound;
    [SerializeField] private Image music;

    [SerializeField] private Sprite[] SoundSprite;
    [SerializeField] private Sprite[] MusicSprite;

    [Header("AudioSource")]
    public AudioSource[] allSounds;
    public AudioSource bgMusic;


    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSoundSprites();
        ManageAudioVolume();
        SetMusicSprites();
        ManageBGVolume();
    }

    public void ChangeSoundPreference()
    {
        if (PreferenceManager.instance.GetSound() == 0)
        {
            PreferenceManager.instance.SetSound(1);
        }
        else
        {
            PreferenceManager.instance.SetSound(0);
        }
        SetSoundSprites();
        ManageAudioVolume();
    }


    public void SetSoundSprites()
    {
        sound.sprite = SoundSprite[PreferenceManager.instance.GetSound()];
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
    public void ChangeMusicPreference()
    {
        if (PreferenceManager.instance.GetMusic() == 0)
        {
            PreferenceManager.instance.SetMusic(1);
        }
        else
        {
            PreferenceManager.instance.SetMusic(0);
        }
        SetMusicSprites();
        ManageBGVolume();
    }
    public void SetMusicSprites()
    {
        music.sprite = MusicSprite[PreferenceManager.instance.GetMusic()];
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

}
