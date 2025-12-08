using UnityEngine;

public class CameraTest : MonoBehaviour
{
    //[SerializeField] private Player m_player;

    private Transform m_cameraTransform;
    private bool m_followPlayer = true;

    void Awake()
    {
        m_cameraTransform = this.gameObject.GetComponent<Transform>();
    }

    /*void OnTriggerEnter2D2(Collider2D other)
    {
        Debug.Log("Camera collision");
    }*/

    void LateUpdate()
    {
        if (m_followPlayer)
        {
            Vector2 playerPosition = Player.Instance.GetPosition();

            m_cameraTransform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
        }
        //Vector3 newPosition = new Vector3(m_player.);
        //m_cameraTransform.position = new Vector3(m_player.GetPosition().x, m_player.GetPosition().y, -10);

    
    }
}
