using UnityEngine;
using UnityEngine.UI; 

public class NextPageButtonAdapter : MonoBehaviour
{
    private Button m_button;

    private void Awake()
    {
        m_button = GetComponent<Button>();
        
        if (m_button != null)
        {
            m_button.onClick.AddListener(HandleNextPage);
        }
    }

    private void HandleNextPage()
    {
        Book.Instance.FlipPage();
    }
}