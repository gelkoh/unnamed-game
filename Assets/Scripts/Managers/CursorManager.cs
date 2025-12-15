using UnityEngine;

public enum CursorState
{
    Default,
    Grab
}

public class CursorManager : SingletonManager
{
    public static CursorManager Instance;

    [SerializeField] private Texture2D m_defaultCursorTexture;
    [SerializeField] private Texture2D m_grabCursorTexture;
    
    private CursorMode m_cursorMode = CursorMode.Auto;
    private Vector2 m_hotSpot = Vector2.zero;
    
    private CursorState m_cursorState = CursorState.Default;
    
    public override void InitializeManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        SetCursorState(m_cursorState);
    }

    public void SetCursorState(CursorState cursorState)
    {        
        switch (cursorState)
        {
            case CursorState.Grab:
                Cursor.SetCursor(m_grabCursorTexture, m_hotSpot, m_cursorMode);
                break;
            default:
                Cursor.SetCursor(m_defaultCursorTexture, m_hotSpot, m_cursorMode);
                break;
        }
    }
}
