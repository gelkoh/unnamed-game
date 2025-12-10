using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : SingletonManager
{
    #region Fields
    
    public static SoundMixerManager Instance;
    
    [SerializeField] private AudioMixer m_audioMixer;
    
    #endregion
    
    public override void InitializeManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    #region Volume setters
    
    public void SetMasterVolume(float volume)
    {
        m_audioMixer.SetFloat("MasterVolume", LinearizeVolume(volume));
        PlayerPrefsManager.Instance.SaveMasterVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        m_audioMixer.SetFloat("SFXVolume", LinearizeVolume(volume));
        PlayerPrefsManager.Instance.SaveSFXVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        m_audioMixer.SetFloat("MusicVolume", LinearizeVolume(volume));
        PlayerPrefsManager.Instance.SaveMusicVolume(volume);
    }
    
    #endregion

    private float LinearizeVolume(float volume)
    {
        return Mathf.Log10(volume) * 20f;
    }
}