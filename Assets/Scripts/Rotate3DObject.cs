using UnityEngine;
using UnityEngine.InputSystem;

public class Rotate3DObject : MonoBehaviour
{
    private bool m_rotateAllowed;
    private Camera m_camera;

    [SerializeField] private float m_speed;
    [SerializeField] private bool m_inverted;

    private InputAction m_leftClickInputAction;
    private InputAction m_mouseLookInputAction;
    private Quaternion m_originalRotation;
    private bool m_isRotatingBack = false;

	private GameStateManager m_gameStateManager;

    private void Awake()
    {
        InputActionAsset inputActions = InputSystem.actions;

        m_leftClickInputAction = inputActions.FindAction("LeftClick");
        m_mouseLookInputAction = inputActions.FindAction("MouseLook");

        if (m_leftClickInputAction != null)
        {
            m_leftClickInputAction.started += OnLeftClickPressed;
            m_leftClickInputAction.performed += OnLeftClickPressed;
            m_leftClickInputAction.canceled += OnLeftClickPressed;
        }

        m_camera = Camera.main;
        m_originalRotation = this.gameObject.transform.localRotation;

		m_gameStateManager = ManagersManager.Get<GameStateManager>();
    }

    private void OnEnable()
    {
        m_leftClickInputAction.Enable();
        m_mouseLookInputAction.Enable();
    }

    private void OnDisable()
    {
        m_leftClickInputAction.Disable();
        m_mouseLookInputAction.Disable();
    }

	// Better use coroutines here i think
    private void Update()
    {
		// check when rotation is done
        if (m_isRotatingBack)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, m_originalRotation, m_speed * Time.deltaTime);
        }

		if (m_gameStateManager.GetState() != GameState.MainMenu)
		{
        	m_leftClickInputAction.Disable();
        	m_mouseLookInputAction.Disable();
			return;
		}

        
        if (!m_rotateAllowed)
            return;

        Vector2 mouseDelta = GetMouseLookInput();
        mouseDelta *= m_speed * Time.deltaTime;
        
        transform.Rotate(Vector3.up * (m_inverted ? 1 : -1), mouseDelta.x, Space.World);
        transform.Rotate(Vector3.right * (m_inverted ? -1 : 1), mouseDelta.y, Space.World);
        
        // Freeze z
        Vector3 euler = transform.rotation.eulerAngles;
        euler.z = 0; 
        transform.rotation = Quaternion.Euler(euler);
    }

    protected virtual void OnLeftClickPressed(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            m_rotateAllowed = true;
            ManagersManager.Get<CursorManager>().SetCursorState(CursorState.Grab);
        }
        else if (context.canceled)
        {
            m_rotateAllowed = false;
            ManagersManager.Get<CursorManager>().SetCursorState(CursorState.Default);
        }
    }

    protected virtual Vector2 GetMouseLookInput()
    {
        if (m_mouseLookInputAction != null)
            return m_mouseLookInputAction.ReadValue<Vector2>();

        return Vector2.zero;
    }

    public void RotateBack()
    {
        m_isRotatingBack = true;
        //this.gameObject.transform.localRotation = m_originalRotation;
    }
}
