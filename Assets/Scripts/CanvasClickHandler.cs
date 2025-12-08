using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System.Collections.Generic;

// Dieses Skript liegt auf dem Canvas (oder Manager) in der Level-Szene
public class CanvasClickHandler : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Vector2 referenceResolution;

    // Wird vom PageClickDetector in der Main Scene aufgerufen
    public void HandlePageClick(Vector2 uvHit)
    {
        Debug.Log("UV Hit: " + uvHit);
        if (canvas == null) return;
        
        // 1. UV-Koordinate in die Canvas-Pixel-Koordinate umwandeln
        float pixelX = uvHit.x * referenceResolution.x;
        float pixelY = uvHit.y * referenceResolution.y;

        Debug.Log(pixelX + ", " + pixelY);
        
        // !!! WICHTIG: Offset-Korrektur hier einfügen !!!
        // Wenn Ihr Level-Bild z.B. um 0.5 nach links verschoben ist:
        // pixelX -= (referenceResolution.x * 0.5f);
        
        // 2. Simulierten Klick ausführen
        SimulateUIClick(new Vector2(pixelX, pixelY));
    }

    private void SimulateUIClick(Vector2 pixelPosition)
    {
        if (EventSystem.current == null) return;

        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = pixelPosition; 

        GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
        if (raycaster == null) return;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        if (results.Count > 0)
        {
            GameObject clickedObject = results[0].gameObject;
            
            // --- NEUE LOGIK FÜR SLIDER START ---
            
            // A. Prüfe, ob das getroffene Element ein Slider ist
            UnityEngine.UI.Slider slider = clickedObject.GetComponentInParent<UnityEngine.UI.Slider>();
            
            if (slider != null)
            {
                // 1. Simuliere das PointerDown-Ereignis
                ExecuteEvents.Execute(clickedObject, pointerData, ExecuteEvents.pointerDownHandler);
                
                // 2. Simuliere das Drag-Ereignis
                // Da wir die Maus nicht ziehen, müssen wir dem Slider die gewünschte Position mitteilen.
                // Die "Drag"-Logik eines Sliders wird normalerweise im IInitializePotentialDragHandler ausgeführt.
                // Der einfachste Weg, den Slider zu setzen, ist die direkte API:
                
                // Konvertiere die Pixelposition in den lokalen Raum des Sliders
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    slider.handleRect.parent as RectTransform, 
                    pixelPosition, 
                    canvas.worldCamera, 
                    out Vector2 localPointerPosition
                );

                // Berechne den Wert basierend auf der Klickposition
                // Dies ist komplex, da es auf der Ausrichtung des Sliders basiert.
                // Die einfachste Lösung ist oft, ein temporäres Drag-Event zu injizieren:
                
                // --- Stattdessen den IPointerClickHandler nutzen, um den Wert direkt zu setzen ---
                // Viele Slider sind so konfiguriert, dass sie bei Klick direkt zur Position springen.

                // 3. Simuliere das Klick-Event, um den Wert zu springen
                // Wir verwenden hier das pointerClickHandler, da es oft das IPointerUp-Event triggert,
                // welches den Slider auf die Klickposition springen lässt.
                ExecuteEvents.Execute(clickedObject, pointerData, ExecuteEvents.pointerClickHandler);
                
                // Wichtig: Fügen Sie in Ihrem Slider-Skript eine Funktion hinzu,
                // die den Wert basierend auf der Klick-Position setzt, falls der Standard-Klick nicht ausreicht.
                return; 
            }

            // --- ZURÜCK ZUR ALTEN LOGIK FÜR KNÖPFE ---
            
            // Für normale Buttons (die den Slider-Check nicht bestanden haben)
            ExecuteEvents.Execute(clickedObject, pointerData, ExecuteEvents.pointerClickHandler);
        }
    }
}