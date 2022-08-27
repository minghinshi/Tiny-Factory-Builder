using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string fileName = "save.json";

    private void Start()
    {
        if (File.Exists(GetSaveFilePath())) LoadGame();
    }

    private void OnApplicationQuit()
    {
        //SaveGame();
    }

    private void SaveGame()
    {
        string json = JsonUtility.ToJson(SaveData.GetCurrentData());
        File.WriteAllText(GetSaveFilePath(), json);
        Debug.Log("Saving game...");
    }

    private void LoadGame()
    {
        string json = File.ReadAllText(GetSaveFilePath());
        SaveData save = JsonUtility.FromJson<SaveData>(json);
        Debug.Log("Loading game...");
    }

    private string GetSaveFilePath()
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
