using UnityEngine;

public class SFXManager : SingletonManager
{
    public static SFXManager Instance;

    [SerializeField] private AudioSource m_SFXObject;

	public override void InitializeManager()
	{
		if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
	}

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(m_SFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}