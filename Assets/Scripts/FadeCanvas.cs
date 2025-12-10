using UnityEngine;
using System.Collections;

public class FadeCanvas : MonoBehaviour
{
    private CanvasGroup m_canvasGroup;

    /*void OnEnable()
    {
        GameStateManager.OnPage2 += HandlePage2;
    }

    void OnDisable()
    {
        GameStateManager.OnPage2 -= HandlePage2;
    }*/
    
    void Awake()
    {
        m_canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
    }

    /*private void HandlePage2()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(5f);
        
        while (m_canvasGroup.alpha > 0)
        {
            m_canvasGroup.alpha -= 0.01f;
            
            yield return new WaitForSeconds(0.02f);
        }
    }*/
}
