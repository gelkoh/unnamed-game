using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : SingletonManager
{
    #region Fields
    
    public static SoundMixerManager Instance; 
    
    [SerializeField] private AudioMixer m_audioMixer;

	private PlayerPrefsManager m_playerPrefsManager;
    
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

	void Start()
	{
		m_playerPrefsManager = ManagersManager.Get<PlayerPrefsManager>();

		float lastMasterVolume = m_playerPrefsManager.GetMasterVolume();
		m_audioMixer.SetFloat("MasterVolume", LinearizeVolume(lastMasterVolume));

		float lastSFXVolume = m_playerPrefsManager.GetSFXVolume();
		m_audioMixer.SetFloat("SFXVolume", LinearizeVolume(lastSFXVolume));

		float lastMusicVolume = m_playerPrefsManager.GetMusicVolume();
		m_audioMixer.SetFloat("MusicVolume", LinearizeVolume(lastMusicVolume));
	}

    #region Volume setters
    public void SetMasterVolume(float volume)
    {
        m_audioMixer.SetFloat("MasterVolume", LinearizeVolume(volume));
        m_playerPrefsManager.SaveMasterVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        m_audioMixer.SetFloat("SFXVolume", LinearizeVolume(volume));
        m_playerPrefsManager.SaveSFXVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        m_audioMixer.SetFloat("MusicVolume", LinearizeVolume(volume));
        m_playerPrefsManager.SaveMusicVolume(volume);
    }
    #endregion

    private float LinearizeVolume(float volume)
    {
        return Mathf.Log10(volume) * 20f;
    }
}