using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SaveSystem : ScriptableObject
{
	
	public string saveFilename = "save.bellum";
	public string backupSaveFilename = "save.bellum.bak";
	public Save saveData = new Save();


	public bool LoadSaveDataFromDisk()
	{
		if (FileManager.LoadFromFile(saveFilename, out var json))
		{
			Debug.Log($"Load System {json}");
			saveData.LoadFromJson(json);
			return true;
		}

		return false;
	}

	public void WriteEmptySaveFile()
	{
		FileManager.WriteToFile(saveFilename, "");
	}
}
