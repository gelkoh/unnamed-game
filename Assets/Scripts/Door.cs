using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        GameStateManager.Instance.NextPage();
    }
}
