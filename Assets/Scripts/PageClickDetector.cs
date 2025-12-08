using UnityEngine;
using UnityEngine.InputSystem;

// Dieses Skript liegt auf dem 3D-Seiten-Mesh (Collider) in der Main Scene
public class PageClickDetector : MonoBehaviour
{
    private Camera m_mainCamera;
    
    // Wichtig: Dieses Skript muss wissen, welchen Manager es im aktuell geladenen Level suchen soll
    public string targetManagerName = "CoverCanvas"; 

    void Start()
    {
        // Hauptkamera finden, die den Raycast steuert
        m_mainCamera = Camera.main; 
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        RaycastHit hit;
        Ray ray = m_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        // Prüfen, ob die 3D-Buchseite getroffen wird
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // 1. Hole die UV-Koordinate des Treffpunkts
                Vector2 uvHit = hit.textureCoord;
                
                // 2. Finde den Manager im additiv geladenen Level
                // Suche nach einem GameObject, das das Canvas steuert
                GameObject handlerObject = GameObject.Find(targetManagerName);

                if (handlerObject != null)
                {
                    // 3. Rufe die Klick-Funktion im Level auf und übergib die UV-Koordinate
                    CanvasClickHandler handler = handlerObject.GetComponent<CanvasClickHandler>();
                    if (handler != null)
                    {
                        // Der eigentliche Klick wird an das Skript in der Level-Szene delegiert
                        handler.HandlePageClick(uvHit);
                    }
                }
            }
        }
    }
}