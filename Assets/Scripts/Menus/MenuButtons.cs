using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    [SerializeField]
    private List<MenuButton> m_buttons;
    
    public InputActionAsset InputActions;

    private InputAction m_navigateAction;
    
    void OnEnable()
    {
        InputActions.FindActionMap("UI").Enable();
        
        m_buttons[0].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
    }

    void OnDisable()
    {
        InputActions.FindActionMap("UI").Disable();
    }

    void Awake()
    {
        m_navigateAction = InputSystem.actions.FindAction("Navigate");
    }

    //void Update()
    //{
     //   Vector2 navigateValue = m_navigateAction.ReadValue<Vector2>();
        
     //   if (navigateValue.y > 0)
     //   {
      //      Debug.Log("UP event received!");
      //  }
      //  else if (navigateValue.y < 0)
      //  {
      //      Debug.Log("DOWN event received!");
      //  }
   // }
}
