using UnityEngine;
using UnityEngine.UI; 

public class SFXVolumeSliderAdapter : MonoBehaviour
{
    private Slider m_slider;

    void Awake()
    {
        m_slider = GetComponent<Slider>();
        
        if (m_slider != null)
        {
            m_slider.onValueChanged.AddListener(HandleAudioLevelChanged);
        }
    }

    private void HandleAudioLevelChanged(float volume)
    {
        SoundMixerManager soundMixerManager = ManagersManager.Get<SoundMixerManager>();
        
        if (soundMixerManager != null)
        {
            soundMixerManager.SetSFXVolume(volume);
        }
        else
        {
            Debug.LogError("SoundMixerManager not found or not initialized!");
        }
    }
}