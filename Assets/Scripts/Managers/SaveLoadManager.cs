using UnityEngine;

public class SaveLoadManager : SingletonManager
{
    public static SaveLoadManager Instance;
    
    public override void InitializeManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    public void Load()
    {
        int loadedPageIndex = 4;
        Book.Instance.FlipToPage(loadedPageIndex);
        Debug.Log("Loading Game");
    }
}