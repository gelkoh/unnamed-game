using UnityEngine;

public static class PlayerPrefsManager
{
    public static void SaveMasterVolume(float level)
    {
        PlayerPrefs.SetFloat("MasterVolume", level);
        PlayerPrefs.Save();
    }
    
    public static void SaveSFXVolume(float level)
    {
        PlayerPrefs.SetFloat("SFXVolume", level);
        PlayerPrefs.Save();
    }

    public static void SaveMusicVolume(float level)
    {
        PlayerPrefs.SetFloat("MusicVolume", level);
        PlayerPrefs.Save();
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat("MasterVolume");
    }
    
    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat("SFXVolume");
    }
    
    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume");
    }
}