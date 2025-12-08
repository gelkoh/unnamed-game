using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraRendererFeature : ScriptableRendererFeature
{
    // Hält das Material mit Ihrem Paper-Look Shader
    public Material paperLookMaterial; 

    // Define an instance of the Scriptable Render Pass
    private PaperLookRenderPass paperLookRenderPass;
    
    // Die Create-Methode initialisiert das Material
    public override void Create()
    {
        // Prüfen, ob das Material zugewiesen ist
        if (paperLookMaterial == null) return; 

        // Erstellen Sie den Pass und übergeben Sie Ihr Material
        paperLookRenderPass = new PaperLookRenderPass(paperLookMaterial);

        // Hier können Sie bestimmen, WANN der Effekt im Render-Zyklus passiert
        //paperLookRenderPass.renderPassEvent = RenderPassEvent.AfterRenderingSkybox;
        paperLookRenderPass.renderPassEvent = RenderPassEvent.AfterRendering;
    }
    
    // Die AddRenderPasses-Methode fügt den Pass zur Warteschlange hinzu
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        // Stellen Sie sicher, dass das Material vorhanden ist
        if (paperLookRenderPass == null) return; 

        // Optionale Filterung (WICHTIG für Sie!):
        // Fügen Sie den Pass nur für Ihre Level-Kameras hinzu
        // Sie könnten hier z.B. einen Tag in den Kameras prüfen:
        // if (renderingData.cameraData.camera.tag == "LevelCamera")
        // {
        //     renderer.EnqueuePass(redTintRenderPass);
        // }
        
        // Fügt den Pass für alle Kameras hinzu (aktuell, basierend auf Ihrer Doku)
        renderer.EnqueuePass(paperLookRenderPass);
    }
    
    // In CameraRendererFeature.cs
    //public override void Dispose(bool disposing)
    //{
        // Rufe die neue Dispose-Methode des Passes auf, um das RTHandle freizugeben
      //  paperLookRenderPass?.Dispose();
   // }
}