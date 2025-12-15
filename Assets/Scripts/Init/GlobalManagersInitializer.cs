using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManagersInitializer
{
    public static GameObject Instance = null;

    private const string m_prefabPath = "Prefabs/GlobalManagersPrefab";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeGlobalManagers()
    {
        Debug.Log("Initializing Global Managers");
        
        if (Instance == null)
        {
            GameObject globalManagersPrefab = Resources.Load<GameObject>(m_prefabPath);

            if (globalManagersPrefab != null)
            {
                GameObject globalManagers = GameObject.Instantiate(globalManagersPrefab);
                Instance = globalManagers;
                GameObject.DontDestroyOnLoad(globalManagers);
            }
            else
            {
                Debug.LogError("Global Managers Prefab not found in Resource directory");
            }

			//SceneManager.LoadScene("Main", LoadSceneMode.Additive);
			SceneManager.LoadScene("PagesScene", LoadSceneMode.Additive);
            //SceneManager.LoadScene("GameplayScene", LoadSceneMode.Additive);
        }
    }
}