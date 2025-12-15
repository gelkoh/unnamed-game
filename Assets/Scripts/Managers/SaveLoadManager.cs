using UnityEngine;
using System.IO;

public class SaveLoadManager : SingletonManager
{
    public static SaveLoadManager Instance;

	private string m_saveFile;

	private SaveData m_saveData = new SaveData();

	[System.Serializable]
	public struct SaveData
	{
		public PlayerSaveData PlayerData;
	}
    
    public override void InitializeManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;

		m_saveFile = Application.persistentDataPath + "/save.json";
    }

	public void Save()
	{
		HandleSaveData();
	}

	private void HandleSaveData()
	{
		 Player.Instance.Save(ref m_saveData.PlayerData);
	}
}