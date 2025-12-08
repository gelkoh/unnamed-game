using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_canvasGroup;

    public InputActionAsset InputActions;

    private InputAction m_menuAction;

    void Awake()
    {
        m_menuAction = InputSystem.actions.FindAction("Menu");
    }

    private void OnEnable()
    {
        InputActions.FindActionMap("UI").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("UI").Disable();
    }

    void Update()
    {
        if (m_menuAction.triggered)
        {
            if (GameStateManager.Instance.GetState() == GameState.InGameMenu)
            {
                m_canvasGroup.alpha = 0;
                m_canvasGroup.interactable = false;
                m_canvasGroup.blocksRaycasts = false;
                
                Time.timeScale = 1.0f;

                GameStateManager.Instance.SetState(GameState.Playing);
            }
            else
            {
                m_canvasGroup.alpha = 1;
                m_canvasGroup.interactable = true;
                m_canvasGroup.blocksRaycasts = true;
                
                Time.timeScale = 0.0f;

                GameStateManager.Instance.SetState(GameState.InGameMenu);
            }

            //OnMenuActivation?.Invoke();
        }
    }
}
