using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class Book : MonoBehaviour
{
	public static Book Instance;
    
	[SerializeField] private BookSettings m_bookSettings;

    private int m_currentPageIndex = 0;

	[SerializeField] private Material m_gameplayLeftMaterial;
	[SerializeField] private Material m_gameplayRightMaterial;

	[SerializeField] private Material m_otherLeftMaterial;
	[SerializeField] private Material m_otherRightMaterial;

	public static Action<PageID> OnPageFlip;

	private PageID m_currentPage = PageID.Cover;

	public PageID CurrentPage
	{
		get => m_currentPage;
		set => m_currentPage = value;
	}

	private MusicManager m_musicManager;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}

		m_musicManager = ManagersManager.Get<MusicManager>();
	}

    void OnEnable()
    {
		GameStateManager.OnStart += HandleStartGame;
		GameStateManager.OnGameStateChanged += HandleGameStateChanged;
    }

    void OnDisable()
    {
		GameStateManager.OnStart -= HandleStartGame;
		GameStateManager.OnGameStateChanged -= HandleGameStateChanged;
    }
    
    void Start()
    {
		int counter = 0;		

		foreach (Transform child in this.gameObject.transform)
		{
			child.gameObject.AddComponent<PageFlipper>();

			if (counter > 4 && counter < gameObject.transform.childCount - 1)
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
  
	private void HandleGameStateChanged(GameState gameState)
	{
		// TODO: FIX PROBLEM
		/*if (gameState == GameState.Playing)
		{		
			this.gameObject.GetComponent<Rotate3DObject>().RotateBack();
			StartCoroutine(MoveBook());
			FlipPage();
		}*/
	}

	private void HandleStartGame()
	{
		//this.gameObject.GetComponent<Rotate3DObject>().RotateBack();
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

		m_currentPage = (PageID)m_currentPageIndex;

		OnPageFlip?.Invoke(m_currentPage);

		if (m_currentPage == PageID.Chapter1Introduction)
		{
			SceneManager.LoadScene("Chapter1GameplayScene", LoadSceneMode.Additive);
			SceneManager.LoadScene("Chapter2GameplayScene", LoadSceneMode.Additive);
		}	
    }
    
    public void FlipPageBackward()
    {
	    SFXManager.Instance.PlaySFXClip(m_bookSettings.PageFlipAudioClip, this.gameObject.transform, 1f);

	    this.gameObject.transform.GetChild(m_currentPageIndex-1).GetComponent<PageFlipper>().FlipBackward();
	    m_currentPageIndex--;

    	m_currentPage = (PageID)m_currentPageIndex;

		OnPageFlip?.Invoke(m_currentPage);
    }

	public void FlipToPage(int index)
	{
		StartCoroutine(MoveBook());
		
		StartCoroutine(FlipToPageRoutine(index));

		//OnPageFlip?.Invoke(m_currentPageIndex);
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
