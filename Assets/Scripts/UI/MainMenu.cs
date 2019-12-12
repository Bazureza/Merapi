using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*public GameObject windowsSetting;
    public GameObject windowsCredits;
    public Slider VolumeMusic;
    public Slider VolumeSound;
    public AudioSource MusicSource;
    public AudioSource SoundSource;
    public AudioClip[] SoundClips;*/



    private float musicvolume;
    private float soundvolume;
    // Use this for initialization
    void Start()
    {
        /*musicvolume = playerPrefHandler.GetMusicVolume();
        soundvolume = playerPrefHandler.GetSoundVolume();*/

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*public void changeMusic()
    {
        MusicSource.volume = VolumeMusic.value;
    }

    public void changeSound()
    {
        SoundSource.volume = VolumeSound.value;
    }
    public void closesetting(bool applied)
    {
        if (applied)

        {

            playerPrefHandler.applySetting(MusicSource.volume, SoundSource.volume);
            musicvolume = playerPrefHandler.GetMusicVolume();
            soundvolume = playerPrefHandler.GetSoundVolume();
        }
        SoundSource.volume = soundvolume;
        MusicSource.volume = musicvolume;
        windowsSetting.SetActive(false);

    }

    public void opensetting()
    {
        windowsSetting.SetActive(true);
        setSoundPrefs();
        Sound(4);
    }*/

    public void StartGame()
    {
       // Sound(0);

        SceneManager.LoadScene("Merapi_GAME");
    }
    /*void setSoundPrefs()
    {
        MusicSource.volume = playerPrefHandler.GetMusicVolume();
        SoundSource.volume = playerPrefHandler.GetSoundVolume();
        VolumeMusic.value = playerPrefHandler.GetMusicVolume();
        VolumeSound.value = playerPrefHandler.GetSoundVolume();

    }

    public void Sound(int sounds)
    {
        SoundSource.clip = SoundClips[sounds];
        SoundSource.Play();

    }
    public void closeCredits()
    {
        Sound(4);
        windowsCredits.SetActive(false);
    }*/
    public void quit()
    {
        //Debug.Log (123);
        //Sound(1);
        Application.Quit();

    }
}
