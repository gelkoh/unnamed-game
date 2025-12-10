using UnityEngine;
using UnityEngine.SceneManagement;

public class Init
{
    // Diese Methode wird zuerst aufgerufen und lädt die Basis-Szene.
    /*[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void OnBeforeSplashScreen() 
    {
        // Lädt die Szene, die die Hauptkamera und das EventSystem enthält
        SceneManager.LoadScene("Main");
    }

    // Diese Methode wird NACH dem Laden der ersten Szene (Main) aufgerufen.
    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        Debug.Log("Initialize and setup Managers");

        GameObject gameStateManagerObj = new GameObject("GameStateManager");
        gameStateManagerObj.AddComponent<GameStateManager>();

        GameObject soundMixerManager = new GameObject("SoundMixerManager");
        soundMixerManager.AddComponent<SoundMixerManager>();*/
        
        // --- Verschieben der Manager in die Main Scene ---
        
        // Die Main Szene MUSS jetzt gültig sein, da sie in OnBeforeSplashScreen geladen wurde.
        /*Scene mainScene = SceneManager.GetSceneByName("Main");
    
        if (mainScene.IsValid())
        {
            // Verschiebe die Manager sofort in die Main Scene, damit sie im Start-Zyklus der Main Scene landen.
            SceneManager.MoveGameObjectToScene(soundMixerManager, mainScene);
            SceneManager.MoveGameObjectToScene(gameStateManagerObj, mainScene);
            Debug.Log("Managers successfully moved to Main Scene.");
        } 
        else
        {
             Debug.LogError("FATAL ERROR: 'Main' scene is not valid for moving Managers.");
             // Die Manager bleiben in DDOL, sind aber funktionsfähig
        }*/

        // --- Lade alle additiven Szenen, nachdem die Manager erstellt wurden ---
        /*SceneManager.LoadScene("Cover", LoadSceneMode.Additive);
        SceneManager.LoadScene("FrontispieceAndTitlepage", LoadSceneMode.Additive);
        SceneManager.LoadScene("Endpaper", LoadSceneMode.Additive);
        SceneManager.LoadScene("Page1", LoadSceneMode.Additive);
        SceneManager.LoadScene("Page2", LoadSceneMode.Additive);
        SceneManager.LoadScene("Page3", LoadSceneMode.Additive);
     	SceneManager.LoadScene("GameplayScene", LoadSceneMode.Additive);
    }*/
}