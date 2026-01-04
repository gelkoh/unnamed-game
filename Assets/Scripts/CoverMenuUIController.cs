using UnityEngine;

public class CoverMenuUIController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject m_mainButtonsPanel;
    [SerializeField] private GameObject m_loadPanel;
    [SerializeField] private GameObject m_settingsPanel;

    private void OnEnable()
    {
        // Wir abonnieren das Event des GameStateManagers
        GameStateManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState newState)
    {
        // Zuerst alle Panels verstecken (Reset-Zustand)
        m_mainButtonsPanel.SetActive(false);
        m_settingsPanel.SetActive(false);
        m_loadPanel.SetActive(false);

        // Nur das Panel zeigen, das zum neuen Status passt
        switch (newState)
        {
            case GameState.MainMenu: // Standard-Zustand des Covers
                m_mainButtonsPanel.SetActive(true);
                break;

            case GameState.MainMenuSettings:
                m_settingsPanel.SetActive(true);
                break;

            case GameState.MainMenuLoad:
                m_loadPanel.SetActive(true);
                break;
                
            // Wenn das Spiel startet, verstecken wir alles auf dem Cover
            case GameState.Playing:
                // Alle bleiben false
                break;
        }
    }
}