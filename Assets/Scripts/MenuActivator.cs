using UnityEngine;
using UnityEngine.InputSystem;

public class MenuActivator : MonoBehaviour
{
    public InputActionAsset InputActions;
    
    private InputAction m_menuAction;

    //public static Action OnMenuActivation;

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
    
    //void Update()
    //{
     //   if (m_menuAction.triggered)
     //   {
     //       Debug.Log("MENU@!!");
            //m_menu.SetActive(!m_menu.activeSelf);

           // OnMenuActivation?.Invoke();
      //  }
    //}
}
