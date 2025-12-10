using UnityEngine;

public class InputManager : SingletonManager
{
    public static InputManager Instance;
    
    public override void InitializeManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }
}