using UnityEngine;
using System.Collections;

public class Book : MonoBehaviour
{
	public static Book Instance;
    
	[SerializeField] private BookSettings m_bookSettings;

    private int m_currentPageIndex = 0;
    public int PageCount { get; private set; } 

	[SerializeField] private Material m_gameplayLeftMaterial;
	[SerializeField] private Material m_gameplayRightMaterial;

	[SerializeField] private Material m_otherLeftMaterial;
	[SerializeField] private Material m_otherRightMaterial;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}

		PageCount = transform.childCount;
	}

    void OnEnable()
    {
		GameStateManager.OnStart += HandleStart;
    }

    void OnDisable()
    {
		GameStateManager.OnStart -= HandleStart;
    }
    
    void Start()
    {
		int counter = 0;		

		foreach (Transform child in this.gameObject.transform)
		{
			child.gameObject.AddComponent<PageFlipper>();

			if (counter > 4)
			{
				MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();

				Material[] materials = meshRenderer.materials;

				if (counter % 2 != 0)
				{
					materials[1] = m_gameplayLeftMaterial;
					materials[2] = m_otherRightMaterial;
				}
				else
				{
					materials[1] = m_otherLeftMaterial;
					materials[2] = m_gameplayRightMaterial;
				}

				meshRenderer.materials = materials;
			}

			counter++;
		}
    }
  
	private void HandleStart()
	{
		StartCoroutine(MoveBook());
		FlipPage();
	}

	private IEnumerator MoveBook()
	{
		while (this.gameObject.transform.position.x < 2.5f)
		{
			this.gameObject.transform.Translate(new Vector3(1f, 0f, 0f) * m_bookSettings.MoveAnimationSpeed * Time.deltaTime);
			yield return new WaitForSeconds(1f/60f);
		}
	}

    public void FlipPage()
    {
		SFXManager.Instance.PlaySFXClip(m_bookSettings.PageFlipAudioClip, this.gameObject.transform, 1f);

		this.gameObject.transform.GetChild(m_currentPageIndex).GetComponent<PageFlipper>().FlipForward();
		m_currentPageIndex++;
    }
    
    public void FlipPageBackward()
    {
	    SFXManager.Instance.PlaySFXClip(m_bookSettings.PageFlipAudioClip, this.gameObject.transform, 1f);

	    this.gameObject.transform.GetChild(m_currentPageIndex-1).GetComponent<PageFlipper>().FlipBackward();
	    m_currentPageIndex--;
    }

	public void FlipToPage(int index)
	{
		StartCoroutine(MoveBook());
		
		StartCoroutine(FlipToPageRoutine(index));
	}

	private IEnumerator FlipToPageRoutine(int index)
	{
		int i = 0;
		
		while (i < index)
		{
			FlipPage();
			i++;
			yield return new WaitForSeconds(0.2f);
		}
	}
}
