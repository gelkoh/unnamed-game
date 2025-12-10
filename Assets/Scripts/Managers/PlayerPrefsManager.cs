using UnityEngine;

public class PlayerPrefsManager : SingletonManager
{
    public static PlayerPrefsManager Instance;
    
    public override void InitializeManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }
    
    public void SaveMasterVolume(float level)
    {
        PlayerPrefs.SetFloat("MasterVolume", level);
        PlayerPrefs.Save();
    }
    
    public void SaveSFXVolume(float level)
    {
        PlayerPrefs.SetFloat("SFXVolume", level);
        PlayerPrefs.Save();
    }

    public void SaveMusicVolume(float level)
    {
        PlayerPrefs.SetFloat("MusicVolume", level);
        PlayerPrefs.Save();
    }

    public float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat("MasterVolume");
    }
    
    public float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat("SFXVolume");
    }
    
    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume");
    }
}