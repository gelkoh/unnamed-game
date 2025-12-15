using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class PageFader : MonoBehaviour
{
    private CanvasGroup m_canvasGroup;

    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
		Debug.Log("PAGE FADER START");
        Book.OnPageFlip += HandlePageFlip;
    }
    
    private void OnDisable()
    {
        Book.OnPageFlip -= HandlePageFlip;
    }

    private void HandlePageFlip(PageID pageID)
    {
		Debug.Log("SAME OR NOT?: " + (pageID == PageID.Chapter1Level1Gameplay));

        if (pageID == PageID.Chapter1Level1Gameplay)
			Debug.Log("Starting coroutine");
            StartCoroutine(FadeCoroutine());
    }
    
    private IEnumerator FadeCoroutine()
    {
        yield return new WaitForSeconds(10f);

        while (m_canvasGroup.alpha > 0f)
        {
            m_canvasGroup.alpha -= 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
