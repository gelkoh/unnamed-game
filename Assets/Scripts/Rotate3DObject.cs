using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Rotate3DObject : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_speed = 5f;
    [SerializeField] private float m_maxVerticalAngle = 80f;

    private bool m_rotateAllowed;
    private InputAction m_leftClickInputAction;
    private InputAction m_mouseLookInputAction;
    
    private float m_currentPitch = 0f; // Vertikal
    private float m_currentYaw = 0f;   // Horizontal
    
    private GameStateManager m_gameStateManager;
    private Camera m_mainCamera;

    private void Awake()
    {
        m_mainCamera = Camera.main;
        InputActionAsset inputActions = InputSystem.actions;
        m_leftClickInputAction = inputActions.FindAction("LeftClick");
        m_mouseLookInputAction = inputActions.FindAction("MouseLook");

        if (m_leftClickInputAction != null)
        {
            m_leftClickInputAction.started += ctx => SetRotateAllowed(true);
            m_leftClickInputAction.canceled += ctx => SetRotateAllowed(false);
        }

        m_gameStateManager = ManagersManager.Get<GameStateManager>();
    }

    private void OnEnable()
    {
        GameStateManager.OnStart += HandleStartGame;

        m_leftClickInputAction.Enable(); 
        m_mouseLookInputAction.Enable();
    }

    private void OnDisable()
    {
        GameStateManager.OnStart -= HandleStartGame;
        
        m_leftClickInputAction.Disable(); 
        m_mouseLookInputAction.Disable();
    }

    private void Update()
    {
        if (m_gameStateManager.GetState() != GameState.MainMenu) return;
        if (!m_rotateAllowed) return;

        Vector2 mouseDelta = m_mouseLookInputAction.ReadValue<Vector2>();

        // Horizontal: Wir drehen den Pivot um die Welt-Y-Achse
        m_currentYaw -= mouseDelta.x * m_speed * Time.deltaTime * 50f;

        // Vertikal: Wir drehen den Pivot um seine eigene "Rechts"-Achse (relativ zur Kamera)
        m_currentPitch += mouseDelta.y * m_speed * Time.deltaTime * 50f;
        m_currentPitch = Mathf.Clamp(m_currentPitch, -m_maxVerticalAngle, m_maxVerticalAngle);

        // Wir wenden die Rotation an: Erst Yaw (Welt), dann Pitch (Lokal)
        transform.rotation = Quaternion.Euler(m_currentPitch, m_currentYaw, 0f);
    }

    private void SetRotateAllowed(bool allowed)
    {
        m_rotateAllowed = allowed;
        ManagersManager.Get<CursorManager>().SetCursorState(allowed ? CursorState.Rotate : CursorState.Default);
    }

    public void RotateBack()
    {
        m_rotateAllowed = false;
        StopAllCoroutines();
        StartCoroutine(ResetPivotRoutine());
    }

    private IEnumerator ResetPivotRoutine()
    {
        float duration = 0.6f;
        float elapsed = 0f;
        
        Quaternion startRot = transform.rotation;
        // Da der Pivot bei 0,0,0 startet, ist das Ziel immer Identity
        Quaternion targetRot = Quaternion.identity; 

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);

            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }

        transform.rotation = targetRot;
        m_currentYaw = 0;
        m_currentPitch = 0;
    }

    private void HandleStartGame()
    {
        RotateBack();
        
        // It's not possible to rotate the book anymore now
        SetRotateAllowed(false);
    }
}