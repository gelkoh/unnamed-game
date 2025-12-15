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

	public void Save(ref PlayerSaveData playerSaveData)
	{
		playerSaveData.Position = transform.position;
	}

	public void Load(PlayerSaveData playerSaveData)
	{
	
		transform.position = playerSaveData.Position;
	}
}

[System.Serializable]
public struct PlayerSaveData
{
	public Vector3 Position;
}