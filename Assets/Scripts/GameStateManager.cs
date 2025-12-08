using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    InGameMenu,
    Playing
}

enum Page
{
    Page1,
    Page2,
    Page3,
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    private GameState CurrentGameState;

    private int m_page = (int)Page.Page1;

    public static Action OnNextPage;
	public static Action OnStart;
	public static Action OnPage2;

	public static int CurrentPageIndex = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CurrentGameState = GameState.MainMenu;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }
    
    public void StartGame()
    {
		Debug.Log("Started Game");

        CurrentGameState = GameState.Playing;
        

		NextPage();
    }

    public void NextPage()
    {
        Debug.Log("NEXT PAGE");

		if (CurrentPageIndex == 3)
		{
			OnPage2?.Invoke();
		}

		if (CurrentPageIndex == 2)
		{
			Debug.Log("Unloaded 'Enpaper' scene");
			SceneManager.UnloadSceneAsync("Endpaper");
		}
		

        
        //int page2Layer = LayerMask.NameToLayer("Page2Layer");
        //Player.Instance.gameObject.layer = page2Layer;
        //Player.Instance.SetPosition(-21.8f, -1.579f, 0f);
        OnStart?.Invoke();
        OnNextPage?.Invoke();
		CurrentPageIndex++;
    }

	public GameState GetState()
	{
		return CurrentGameState;
	}

	public void SetState(GameState newState)
	{
		CurrentGameState = newState;
	}

	public void Load()
	{
		
	}

	public void Quit()
	{
		#if UNITY_STANDALONE
			Application.Quit();
		#endif
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}