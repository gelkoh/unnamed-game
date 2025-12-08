using UnityEngine;
using System.Collections;

public class Book : MonoBehaviour
{
	public static Book Instance;
	
    [SerializeField] private BookPage[] m_allPages;
    
    private Animator m_animator;

    //private int m_currentPageIndex = 0;
    public int PageCount { get; private set; } 

	[SerializeField] private AudioClip m_SFXClip;

	[SerializeField] private float m_moveAnimationSpeed;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}

		//Debug.Log("Childcount " + transform.childCount);
		PageCount = transform.childCount;
	}

    void OnEnable()
    {
        GameStateManager.OnNextPage += FlipPage;
		GameStateManager.OnStart += HandleStart;
    }

    void OnDisable()
    {
        GameStateManager.OnNextPage -= FlipPage;
		GameStateManager.OnStart -= HandleStart;
    }
    
    void Start()
    {
        m_animator = this.gameObject.GetComponent<Animator>();

        //LoadAndFlipToPage(4);
    }
    
    public void FlipPage()
    {
		SFXManager.Instance.PlaySFXClip(m_SFXClip, this.gameObject.transform, 1f);

		//m_animator.Play("FlipPage5", -1, 1.0f);
		
        if (GameStateManager.CurrentPageIndex < m_allPages.Length)
        {
			switch (GameStateManager.CurrentPageIndex)
			{
				case 0:
 					m_animator.SetTrigger("FlipPage1");
					break;
				case 1:
					m_animator.SetTrigger("FlipPage2");
					break;
				case 2:
					m_animator.SetTrigger("FlipPage3");
					break;
				case 3:
					m_animator.SetTrigger("FlipPage4");
					break;
				case 4:
					m_animator.SetTrigger("FlipPage5");
					break;
				case 5:
					m_animator.SetTrigger("FlipPage6");
					break;
			}
        }
    }

	private void HandleStart()
	{
		StartCoroutine(MoveBook());
	}

	private IEnumerator MoveBook()
	{
		while (this.gameObject.transform.position.x < 2.5f)
		{
			this.gameObject.transform.Translate(new Vector3(1f, 0f, 0f) * m_moveAnimationSpeed * Time.deltaTime);
			yield return new WaitForSeconds(1f/60f);
		}
	}
	
	
	
	
	
	
	/*[Header("Lade-Einstellungen")]
	[SerializeField] private float m_loadFlipOffsetTime = 0.05f; // 50 Millisekunden pro Seite
	
	public void LoadAndFlipToPage(int targetPageIndex) 
	{
		// Stoppt jede laufende Flipping-Sequenz, falls vorhanden
		StopAllCoroutines(); 
        
		// Starte die schnelle Blätter-Sequenz
		StartCoroutine(FastPageFlipRoutine(targetPageIndex));
	}

	private IEnumerator FastPageFlipRoutine(int targetIndex)
	{
		// Beginne bei der ersten Seite, die geblättert werden muss (Index 1 für FlipPage1)
		for (int i = 1; i <= targetIndex; i++)
		{
			string flipStateName = "FlipPage" + i;
            
			// 1. Starte die Animation direkt
			// -1: Standard Layer
			// 0f: Starte die Animation am Anfang (Frame 0)
			m_animator.Play(flipStateName, -1, 0f);

			// 2. Warte die konfigurierte Offset-Zeit
			yield return new WaitForSeconds(m_loadFlipOffsetTime);
		}
        
		// Optional: Hier kann die Logik erfolgen, die den Animator
		// in den Endzustand der letzten Seite bringt (um das Buch
		// im Endzustand zu halten, falls das Problem des "Zurückspringens"
		// in diesem Modus auftritt, kann dies mit m_animator.Play("FlipPage" + targetIndex, -1, 1.0f)
		// nach Beendigung des letzten Waits behoben werden).
	}*/
}
