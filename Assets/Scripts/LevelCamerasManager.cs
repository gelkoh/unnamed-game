using UnityEngine;
using System.Collections.Generic;

public class LevelCamerasManager : MonoBehaviour
{
    public List<LevelCameraController> levels;

    private int m_levelCameraIndex = 0;

    private void OnEnable()
    {
        Book.OnPageFlip += HandlePageFlip;
    }
    
    private void OnDisable()
    {
        Book.OnPageFlip -= HandlePageFlip;
    }

    public void HandlePageFlip(PageID pageID)
    {
        Debug.Log("PAGEID: " + pageID);
        Debug.Log("pageID == PageID.Chapter1Level1Gameplay: " + (pageID == PageID.Chapter1Level1Gameplay));
        Debug.Log("pageID == PageID.Chapter1Level2Gameplay: " + (pageID == PageID.Chapter1Level2Gameplay));
        Debug.Log("--------------");

        if (pageID == PageID.Chapter1Level1Gameplay)
            return;
        
        if (pageID != PageID.Chapter1Level2Gameplay)
            return;
        
        Debug.Log("Deactivating camera at m_levelCameraIndex: " + m_levelCameraIndex);
        levels[m_levelCameraIndex].Deactivate();
        m_levelCameraIndex++;

        levels[m_levelCameraIndex].Activate();
    }
}
