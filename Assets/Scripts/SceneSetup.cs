using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetup : MonoBehaviour
{
    [SerializeField] private GameObject m_targetCanvas;
    [SerializeField] private GameObject m_nextPageButtonPrefab;
    [SerializeField] private GameObject m_pageNumberLeftPrefab;
    [SerializeField] private GameObject m_pageNumberRightPrefab;

    [SerializeField] private Vector3 m_nextPageButtonPosition;
    
    [SerializeField] private Vector3 m_pageNumberLeftPosition;
    [SerializeField] private Vector3 m_pageNumberRightPosition;

    [SerializeField] private bool m_pageNumberLeftVisible = true;
    [SerializeField] private bool m_pageNumberRightVisible = true;

    void Awake()
    {
        Scene currentScene = this.gameObject.scene;
        
        if (ShouldAddNextPageButton(currentScene.name))
        {
            InstantiateAndSetup(m_nextPageButtonPrefab, m_nextPageButtonPosition);
        }

        if (m_pageNumberLeftVisible)
        {
            InstantiateAndSetup(m_pageNumberLeftPrefab, m_pageNumberLeftPosition);
        }

        if (m_pageNumberRightVisible)
        {
            InstantiateAndSetup(m_pageNumberRightPrefab, m_pageNumberRightPosition);
        }
        /*if (ShouldAddPageNumber(currentScene.name))
        {
            InstantiateAndSetup(m_pageNumberLeftPrefab, m_pageNumberLeftPosition);
            InstantiateAndSetup(m_pageNumberRightPrefab, m_pageNumberRightPosition);
        }*/
    }
    
    private void InstantiateAndSetup(GameObject prefab, Vector3 position)
    {
        GameObject element = Instantiate(prefab, m_targetCanvas.transform);
        element.transform.localScale = new Vector3(1, 1, 1);
        element.GetComponent<RectTransform>().localPosition = position;
        element.layer = this.gameObject.scene.GetRootGameObjects()[0].layer;
        element.SetActive(element);
    }

    private bool ShouldAddNextPageButton(string sceneName)
    {
        return sceneName == "Endpaper" || sceneName == "FrontispieceAndTitlepage" || sceneName == "Page1";
    }

    private bool ShouldAddPageNumber(string sceneName)
    {
        if (sceneName != "Cover" && sceneName != "Endpaper" && sceneName != "FrontispieceAndTitlepage")
        {
            return true;
        }

        return false;
    }
}
