using UnityEngine;
using UnityEngine.UI; 

public class PreviousPageButtonAdapter : MonoBehaviour
{
    private Button m_button;

    private void Awake()
    {
        m_button = GetComponent<Button>();
        
        if (m_button != null)
        {
            m_button.onClick.AddListener(HandlePreviousPage);
        }
    }

    private void HandlePreviousPage()
    {
        Book.Instance.FlipPageBackward();
    }
}