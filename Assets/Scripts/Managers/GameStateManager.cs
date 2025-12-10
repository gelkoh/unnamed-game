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

public class GameStateManager : SingletonManager
{
	public static GameStateManager Instance;

	private GameState CurrentGameState;
	public static Action OnStart;
	/*private int m_page = (int)Page.Page1;

	public static Action OnNextPage;
	
	public static Action OnPage2;
	public static Action OnPage3;

	public static int CurrentPageIndex = 0;*/


	private Checkpoint m_lastCheckpoint = null;


	public override void InitializeManager()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}
	
	public void StartGame()
	{
		Debug.Log("Started Game");

		CurrentGameState = GameState.Playing;
		OnStart?.Invoke();
	}

	/*public void NextPage()
	{
		Debug.Log("NEXT PAGE");

		if (CurrentPageIndex == 3)
		{
			SceneManager.LoadScene("GameplayScene", LoadSceneMode.Additive);

			OnPage2?.Invoke();
		}

		if (CurrentPageIndex == 4)
		{
			OnPage3?.Invoke();
		}


		/*if (CurrentPageIndex == 2)
		{
			Debug.Log("Unloaded 'Enpaper' scene");
			SceneManager.UnloadSceneAsync("Endpaper");
		}*/



		//int page2Layer = LayerMask.NameToLayer("Page2Layer");
		//Player.Instance.gameObject.layer = page2Layer;
		//Player.Instance.SetPosition(-21.8f, -1.579f, 0f);
	/*	OnStart?.Invoke();
		OnNextPage?.Invoke();
		CurrentPageIndex++;
	}*/

	public GameState GetState()
	{
		return CurrentGameState;
	}

	public void SetState(GameState newState)
	{
		CurrentGameState = newState;
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

	public void SetCheckpoint(Checkpoint checkpoint)
	{
		Debug.Log("Checkpoint set");
		m_lastCheckpoint = checkpoint;
	}

	public Checkpoint GetCheckpoint()
	{
		return m_lastCheckpoint;
	}
}