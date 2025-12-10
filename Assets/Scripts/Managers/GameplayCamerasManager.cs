using UnityEngine;

public class GameplayCamerasManager : MonoBehaviour
{
    [SerializeField] private GameObject m_cameraLevel1;
    [SerializeField] private GameObject m_cmCameraLevel1;
    
    
    [SerializeField] private GameObject m_cameraLevel2;
    [SerializeField] private GameObject m_cmCameraLevel2;

    /*void OnEnable()
    {
        GameStateManager.OnPage3 += HandlePage3;
    }
    
    void OnDisable()
    {
        GameStateManager.OnPage3 -= HandlePage3;
    }

    private void HandlePage3()
    {
        m_cameraLevel1.SetActive(false);
        m_cmCameraLevel1.SetActive(false);
        
        m_cameraLevel2.SetActive(true);
        m_cmCameraLevel2.SetActive(true);
    }*/
}