using UnityEngine;

// Wichtig: Fügen Sie UnityEngine.UI hinzu, um mit Buttons zu arbeiten
using UnityEngine.UI; 

public class StartButtonAdapter : MonoBehaviour
{
    private Button m_button;

    void Awake()
    {
        m_button = GetComponent<Button>();
        
        if (m_button != null)
        {
            // Fügen Sie den Listener per Code hinzu, da die Manager nicht im Editor sichtbar sind.
            m_button.onClick.AddListener(HandleStartGame);
        }
    }

    private void HandleStartGame()
    {
        // 1. Hole die Instanz des GameStateManagers über den ManagersManager
        GameStateManager gameStateManager = ManagersManager.Get<GameStateManager>();
        
        if (gameStateManager != null)
        {
            // 2. Rufe die gewünschte Funktion auf der korrekten Instanz auf
            gameStateManager.StartGame();
        }
        else
        {
            Debug.LogError("GameStateManager not found or not initialized!");
        }
        
        // Optional: Deaktivieren Sie den Button nach dem Start
        m_button.interactable = false;
    }
}