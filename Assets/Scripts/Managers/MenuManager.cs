using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : SingletonManager
{
    public static MenuManager Instance;
    
    [SerializeField] private GameObject m_menuPrefab;
    private GameObject m_menu;
    
    private InputManager m_inputManager;
    private GameStateManager m_gameStateManager;

	private CanvasGroup m_menuCanvasGroup;

    public override void InitializeManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        
        Instance = this;
    }
    
    private void Awake()
    {
        m_menu = Instantiate(m_menuPrefab);
        m_menuCanvasGroup = m_menu.GetComponent<CanvasGroup>();

		HideMenu();
    }

    private void Start()
    {
        //SceneManager.MoveGameObjectToScene(m_menu, SceneManager.GetSceneByName("PagesScene"));
        
        m_inputManager = ManagersManager.Get<InputManager>();
        m_gameStateManager = ManagersManager.Get<GameStateManager>();
            
        m_inputManager.OnMenuActionPressed += HandleMenuActionPressed;
    }

    private void OnDisable()
    {
        m_inputManager.OnMenuActionPressed -= HandleMenuActionPressed;
    }

    private void HandleMenuActionPressed()
    {
        if (m_gameStateManager.CurrentGameState == GameState.MainMenu)
            return;
        
        if (m_gameStateManager.CurrentGameState == GameState.IngameMenu)
        {
            m_gameStateManager.SetState(GameState.Playing);
			
			HideMenu();
        }
        else
        {
            m_gameStateManager.SetState(GameState.IngameMenu);

			ShowMenu();
        }
    }

	private void HideMenu()
	{
		m_menuCanvasGroup.alpha = 0;
		m_menuCanvasGroup.interactable = false;
		m_menuCanvasGroup.blocksRaycasts = false;
		Time.timeScale = 1.0f;
	}

	private void ShowMenu()
	{
		m_menuCanvasGroup.alpha = 1;
		m_menuCanvasGroup.interactable = true;
		m_menuCanvasGroup.blocksRaycasts = true;
   		Time.timeScale = 0.0f;
	}
}