using UnityEngine;
using UnityEngine.InputSystem;

public class MenuActivator : MonoBehaviour
{
    //[SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private GameObject m_menuPrefab;
    
    private InputAction m_menuAction;

    void Awake()
    {
		InputActionAsset inputActions = InputSystem.actions; 
     
     	m_menuAction = inputActions.FindAction("Menu");
    }
    
    private void OnEnable()
    {
        m_menuAction.Enable();
    }

    private void OnDisable()
    {
        m_menuAction.Disable();
    }
    
    void Update()
    {
        Debug.Log("MENNUU");
        if (m_menuAction.WasPressedThisFrame())
        {
            Debug.Log("Open menu");
            
            if (GameStateManager.Instance.GetState() == GameState.InGameMenu)
            {
                /*m_canvasGroup.alpha = 0;
                m_canvasGroup.interactable = false;
                m_canvasGroup.blocksRaycasts = false;*/
                Instantiate(m_menuPrefab);
                
                Time.timeScale = 1.0f;

                GameStateManager.Instance.SetState(GameState.Playing);
            }
            else
            {
                /*m_canvasGroup.alpha = 1;
                m_canvasGroup.interactable = true;
                m_canvasGroup.blocksRaycasts = true;
                
                Time.timeScale = 0.0f;*/

                GameStateManager.Instance.SetState(GameState.InGameMenu);
            }
        }
    }
}
