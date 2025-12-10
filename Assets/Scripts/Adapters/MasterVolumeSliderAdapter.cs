using UnityEngine;
using UnityEngine.UI; 

public class MasterVolumeSliderAdapter : MonoBehaviour
{
    private Slider m_slider;

    void Awake()
    {
        m_slider = GetComponent<Slider>();
        
        PlayerPrefsManager playerPrefsManager = ManagersManager.Get<PlayerPrefsManager>();
        float storedMasterVolume = playerPrefsManager.GetMasterVolume();

        Debug.Log("Stored master vol " + storedMasterVolume);
        
        if (storedMasterVolume != 0.0f)
        {
            m_slider.value = storedMasterVolume;
            Debug.Log("slider val " + m_slider.value);
            HandleAudioLevelChanged(storedMasterVolume);
        }
        
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
            soundMixerManager.SetMasterVolume(volume);
        }
        else
        {
            Debug.LogError("SoundMixerManager not found or not initialized!");
        }
    }
}