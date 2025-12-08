using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    [SerializeField]
    private Material m_material;
    
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Debug.Log("ON RENDER IMAGE");

        
        if (m_material == null)
        {
            Graphics.Blit(src, dest);
            return;
        }


        Graphics.Blit(src, dest, m_material);
    }
}