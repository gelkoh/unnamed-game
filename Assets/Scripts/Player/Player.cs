using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
	[SerializeField] public PlayerStats m_playerStats;

    private void Awake()
    {
	    if (Instance != null && Instance != this)
	    {
		    Destroy(gameObject);
		    return;
	    }

	    Instance = this;
    }
    
	public void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Collided with " + other.tag);

		if (other.tag == "Water")
		{
			Die();
		}
	}

	private void Die()
	{
		Debug.Log("Player died");

        Checkpoint lastCheckpoint = GameStateManager.Instance.GetCheckpoint();
        this.transform.position = lastCheckpoint.transform.position;
    }
}