using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PagesUIInitializer : MonoBehaviour
{
    [SerializeField] private List<Camera> m_cameras;

    [SerializeField] private GameObject m_pagesUICanvas;
    
    [Header("Page Numbers")]
    [SerializeField] private bool m_showPageNumberLeft;
    [SerializeField] private bool m_showPageNumberRight;
    [SerializeField] private GameObject m_pageNumberLeftPrefab;
    [SerializeField] private GameObject m_pageNumberRightPrefab;

  
    [Header("Previous-/Next-Buttons")]
    [SerializeField] private bool m_showPreviousPageButton;
    [SerializeField] private bool m_showNextPageButton;
    [SerializeField] private GameObject m_previousPageButtonPrefab;
    [SerializeField] private GameObject m_nextPageButtonPrefab;

   

    void Start()
    {
        foreach (Camera cam in m_cameras)
        {
            InstantiateUICanvas(cam);
        }
    }

    private void InstantiateUICanvas(Camera targetCamera)
    {
        GameObject canvasInstance = Instantiate(m_pagesUICanvas);
        canvasInstance.name = $"UICanvas_{targetCamera.name}";
        
        Canvas canvas = canvasInstance.GetComponent<Canvas>();
        canvas.worldCamera = targetCamera;
        canvas.planeDistance = 10;

        canvasInstance.AddComponent<CanvasClickHandler>();

        if (m_showNextPageButton)
        {
            GameObject newButton = Instantiate(m_nextPageButtonPrefab, canvasInstance.gameObject.transform);
            newButton.transform.localPosition = newButton.transform.localPosition + new Vector3(860f, 0f, 0f);
        }
        
        if (m_showPreviousPageButton)
        {
            GameObject newButton = Instantiate(m_previousPageButtonPrefab, canvasInstance.gameObject.transform);
            newButton.transform.localPosition = newButton.transform.localPosition + new Vector3(-860f, 0f, 0f);
        }
        
        if (m_showPageNumberLeft)
        {
            GameObject pageNumber = Instantiate(m_pageNumberLeftPrefab, canvasInstance.gameObject.transform);
            pageNumber.transform.localPosition = pageNumber.transform.localPosition + new Vector3(-860f, -500f, 0f);
        }
        
        if (m_showPageNumberRight)
        {
            GameObject pageNumber = Instantiate(m_pageNumberRightPrefab, canvasInstance.gameObject.transform);
            pageNumber.transform.localPosition = pageNumber.transform.localPosition + new Vector3(860f, -500f, 0f);
        }
        
        SceneManager.MoveGameObjectToScene(canvasInstance, SceneManager.GetSceneByName("PagesScene"));
    }
}