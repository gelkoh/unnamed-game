using UnityEngine;
using Unity.Cinemachine;

public class LevelCameraController : MonoBehaviour
{
    [Header("Camera Setup")]
    public Camera gameplayCamera;                // Die echte Kamera für die RenderTexture
    public CinemachineCamera cinemachineCamera;        // Die Cinemachine Kamera
    //public RenderTexture renderTexture;          // Seite im Buch

    [Header("Target Setup")]
    public Transform playerToFollow;

    public void Activate()
    {
        gameplayCamera.enabled = true;
        cinemachineCamera.enabled = true;
        cinemachineCamera.Follow = playerToFollow;

        //gameplayCamera.targetTexture = renderTexture;
    }

    public void Deactivate()
    {
        gameplayCamera.enabled = false;
        cinemachineCamera.enabled = false;
    }
}