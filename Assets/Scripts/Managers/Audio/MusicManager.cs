using UnityEngine;

public class MusicManager : SingletonManager
{
    public static MusicManager Instance;

    private AudioSource m_musicSource;

    public override void InitializeManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        m_musicSource = this.gameObject.GetComponent<AudioSource>();
    }

    void Awake()
    {
        Debug.Log("AWAKE!");
    }
    
    void Start()
    {
        // 1. Prüfen, ob die Quelle vorhanden ist
        if (m_musicSource == null)
        {
            Debug.LogError("MusicManager: AudioSource reference is missing!");
            return;
        }

        // 2. Manuelle Zuweisung des Clips (falls nicht schon im Editor geschehen)
        // m_musicSource.clip = someMusicClip; 

        // 3. EXPLIZITES Starten der Musikwiedergabe
        m_musicSource.Play();
        Debug.Log("Music started manually.");
    }
}