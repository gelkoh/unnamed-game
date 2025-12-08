using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Wichtig: Nutzen Sie die neuen URP-APIs
public class PaperLookRenderPass : ScriptableRenderPass
{
    private Material blitMaterial;
    
    // NEU: Verwenden Sie RTHandle für die temporäre Textur
    private RTHandle tempTexture; 

    // Konstruktor
    public PaperLookRenderPass(Material material)
    {
        this.blitMaterial = material;
        renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
        
        // Initialisiere RTHandle (wird später in Configure geladen)
        tempTexture = RTHandles.Alloc("_TempColorRT", name: "Temporary Color Texture");
    }

    // Die neue Methode, um die Quelle und das Ziel zu konfigurieren
    public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
    {
        // Konfiguriere das temporäre RTHandle basierend auf der Kameradeskriptor
        // Die Auflösung etc. wird von der Kamera übernommen
        ConfigureTarget(tempTexture);
    }
    
    // Die Execute-Methode (Die Logik ist jetzt innerhalb der CommandBuffer-Ausführung)
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (blitMaterial == null) return;

        // Hole die aktuelle Renderziel-Handle (das ist die Kamera-Farbpuffer)
        RTHandle cameraTargetHandle = renderingData.cameraData.renderer.cameraColorTargetHandle;
        
        CommandBuffer cmd = CommandBufferPool.Get("Paper Look Pass");

        // 1. Ersten Blit: Kopiere das Quellbild (cameraTargetHandle) in die temporäre Textur (tempTexture)
        // (Damit wir das Originalbild haben, bevor wir es mit dem Shader überschreiben)
        Blit(cmd, cameraTargetHandle, tempTexture, blitMaterial, 0); 
        
        // 2. Zweiten Blit: Wende den Shader an und schreibe das Ergebnis zurück ins Ziel (cameraTargetHandle)
        // Der zweite Blit ist optional, wenn der erste Blit schon den Shader anwendet.
        // Führen Sie diesen Schritt nur aus, wenn der Shader auf der ersten Stufe nur die Quelle kopiert hat.
        // Bei einem Post-Processing-Effekt, der das Originalbild filtern soll, führen wir das Blitting *zurück* ins Ziel durch:
        Blit(cmd, tempTexture, cameraTargetHandle); // Kopiert tempTexture (mit Filter) zurück ins Ziel

        // Führe die Befehle aus
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
    
    // NEU: Wir verwenden die Dispose-Methode, um das RTHandle freizugeben
    public void Dispose()
    {
        tempTexture?.Release();
    }
}