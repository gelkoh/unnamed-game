using UnityEngine;

public class MusicManager : SingletonManager
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource m_musicObject;

	[SerializeField] private MusicLibrary m_musicLibrary;

    public override void InitializeManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

	private void Start()
	{
		Play(MusicContext.Chapter1);
	}

	public void Play(MusicContext context)
	{
		AudioSource audioSource = Instantiate(m_musicObject);

		MusicTrack musicTrack = m_musicLibrary.GetMusicTrackForContext(context);

		audioSource.clip = musicTrack.audioClip;
		audioSource.volume = musicTrack.defaultVolume;
		
		audioSource.Play();
	}
}