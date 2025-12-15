using UnityEngine;
using UnityEngine.UI; 

public class MasterVolumeSliderAdapter : MonoBehaviour
{
    private Slider m_slider;
	private SoundMixerManager m_soundMixerManager;
	private PlayerPrefsManager m_playerPrefsManager;

    void Awake()
    {
        m_slider = GetComponent<Slider>();
        
		m_soundMixerManager = ManagersManager.Get<SoundMixerManager>();
        m_playerPrefsManager = ManagersManager.Get<PlayerPrefsManager>();
		
        float storedMasterVolume = m_playerPrefsManager.GetMasterVolume();
        
        if (storedMasterVolume != 0.0f)
        {
            m_slider.value = storedMasterVolume;
        }
        
        m_slider.onValueChanged.AddListener((volume) => m_soundMixerManager.SetMasterVolume(volume));
    }
}