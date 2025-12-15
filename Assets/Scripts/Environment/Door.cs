using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        Book.Instance.FlipPage();
        Destroy(gameObject);
    }
}