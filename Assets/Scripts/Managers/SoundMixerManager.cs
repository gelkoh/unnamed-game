using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    #region Fields
    
    public static SoundMixerManager Instance;
    
    [SerializeField] private AudioMixer m_audioMixer;
    
    #endregion

    void Awake()
    {
        Debug.Log("Awake");
        
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Debug.Log("Inside Start");
        
        float masterVolume = PlayerPrefsManager.GetMasterVolume();
        m_audioMixer.SetFloat("MasterVolume", LinearizeVolume(masterVolume));

        float x;
        m_audioMixer.GetFloat("MasterVolume", out x);
        Debug.Log("Master volume is " + x);
    }

    #region Volume setters
    
    public void SetMasterVolume(float volume)
    {
        m_audioMixer.SetFloat("MasterVolume", LinearizeVolume(volume));
        PlayerPrefsManager.SaveMasterVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        m_audioMixer.SetFloat("SFXVolume", LinearizeVolume(volume));
        PlayerPrefsManager.SaveSFXVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        m_audioMixer.SetFloat("MusicVolume", LinearizeVolume(volume));
        PlayerPrefsManager.SaveMusicVolume(volume);
    }
    
    #endregion

    private float LinearizeVolume(float volume)
    {
        return Mathf.Log10(volume) * 20f;
    }
}