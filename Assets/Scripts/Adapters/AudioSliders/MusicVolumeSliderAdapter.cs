using UnityEngine;
using UnityEngine.UI; 

public class MusicVolumeSliderAdapter : MonoBehaviour
{
    private Slider m_slider;
    private SoundMixerManager m_soundMixerManager;
    private PlayerPrefsManager m_playerPrefsManager;

    void Awake()
    {
        m_slider = GetComponent<Slider>();
        
        m_soundMixerManager = ManagersManager.Get<SoundMixerManager>();
        m_playerPrefsManager = ManagersManager.Get<PlayerPrefsManager>();
		
        float storedMusicVolume = m_playerPrefsManager.GetMusicVolume();
        
        if (storedMusicVolume != 0.0f)
        {
            m_slider.value = storedMusicVolume;
        }
        
        m_slider.onValueChanged.AddListener((volume) => m_soundMixerManager.SetMusicVolume(volume));
    }
}