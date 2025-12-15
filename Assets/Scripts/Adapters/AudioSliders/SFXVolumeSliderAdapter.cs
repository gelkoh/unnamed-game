using UnityEngine;
using UnityEngine.UI; 

public class SFXVolumeSliderAdapter : MonoBehaviour
{
 	private Slider m_slider;
	private SoundMixerManager m_soundMixerManager;
	private PlayerPrefsManager m_playerPrefsManager;

    void Awake()
    {
        m_slider = GetComponent<Slider>();
        
		m_soundMixerManager = ManagersManager.Get<SoundMixerManager>();
        m_playerPrefsManager = ManagersManager.Get<PlayerPrefsManager>();
		
        float storedSFXVolume = m_playerPrefsManager.GetSFXVolume();
        
        if (storedSFXVolume != 0.0f)
        {
            m_slider.value = storedSFXVolume;
        }
        
        m_slider.onValueChanged.AddListener((volume) => m_soundMixerManager.SetSFXVolume(volume));
    }
}