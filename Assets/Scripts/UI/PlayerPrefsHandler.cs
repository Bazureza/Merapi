  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class playerPrefHandler
{


    /*
     * PlayerPref guide
     * 
     * PlayerPrefs float musicVolume, music volume throughout the game
     * PlayerPrefs float soundVolume, sound volume throughout the game
     * 
     * PlayerPrefsX int[] areaGame, order of area for the gamethrough. 0=kitchen,1=bedroom,2=bathroom
     * PlayerPrefs int area1Key, location of key in area 1. value= 0-*
     * PlayerPrefs int area2Key, location of key in area 2. value= 0-*
     * PlayerPrefs int area3Key, location of key in area 3. value= 0-*
     * 
     * PlayerPrefsX bool[] areaDone, list of completed area. 1 true can go to area2, 2 trues can go to area3, 3 trues you can exit
     * 
     * PlayerPrefs bool saved, check if there's any saved game. a finished game will turn saved to false
     * PlayerPrefs bool gameFinish, if true, change screen from Game to Main Menu will automatically enable Credit part
     */

    public static void applySetting(float musicVolume, float soundVolume)
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("soundVolume", soundVolume);

    }
    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("musicVolume", 1);
    }
    public static float GetSoundVolume()
    {
        return PlayerPrefs.GetFloat("soundVolume", 1);
    }
    
    public static bool GetIt()
    {
        return PlayerPrefsX.GetBool("GameFinish", false);
    }
    public static void finishIt()
    {
        PlayerPrefsX.SetBool("GameFinish", false);
    }

}

