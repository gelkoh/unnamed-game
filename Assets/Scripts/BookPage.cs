using UnityEngine;

public class BookPage : MonoBehaviour
{
    private Animator m_animator;

    void Awake()
    {
        m_animator = this.gameObject.GetComponent<Animator>();
    }

    public void FlipPageForward()
    {
        if (m_animator == null)
        {
            Debug.Log("Animator is null", this.gameObject);
            return;
        }

        m_animator.SetTrigger("FlipPage");
    }
}
