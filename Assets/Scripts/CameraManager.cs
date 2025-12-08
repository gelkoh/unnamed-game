using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject m_initialCamera;
	[SerializeField] private GameObject m_secondCamera;
	[SerializeField] private GameObject m_thirdCamera;

    void OnEnable()
    {
        GameStateManager.OnStart += HandleStart;
		GameStateManager.OnNextPage += HandleNextPage;
    }
    
    void OnDisable()
    {
        GameStateManager.OnStart -= HandleStart;
		GameStateManager.OnNextPage -= HandleNextPage;
    }

    private void HandleStart()
    {
        m_initialCamera.SetActive(false);
    }

	private void HandleNextPage()
	{
		//m_secondCamera.SetActive(false);
		//m_thirdCamera.SetActive(true);

		StartCoroutine(ZoomOut());
	}

    private IEnumerator ZoomOut()
    {
		m_thirdCamera.SetActive(true);
		m_secondCamera.SetActive(false);

        yield return new WaitForSeconds(1f);

        m_thirdCamera.SetActive(false);
        m_secondCamera.SetActive(true);
    }
}
