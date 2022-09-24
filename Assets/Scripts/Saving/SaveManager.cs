using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private JsonSerializerSettings serializerSettings;

    private void Start()
    {
        serializerSettings = GetSerializerSettings();
        if (File.Exists(GetSaveFilePath())) LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private JsonSerializerSettings GetSerializerSettings()
    {
        JsonSerializerSettings settings = new();
        settings.Converters.Add(new ItemTypeConverter());
        settings.Converters.Add(new Vector2IntConverter());
        return settings;
    }

    private string GetSaveFilePath()
    {
        return GetDataPath() + "/save.json";
    }

    private string GetDataPath()
    {
        return Application.isEditor ? Application.dataPath + "/Tests" : Application.persistentDataPath;
    }

    private void LoadGame()
    {
        string json = File.ReadAllText(GetSaveFilePath());
        JsonConvert.DeserializeObject<SaveFile>(json, serializerSettings).LoadFile();
        Debug.Log("Loading game...");
    }

    private void SaveGame()
    {
        string json = JsonConvert.SerializeObject(SaveFile.GetCurrentData(), serializerSettings);
        File.WriteAllText(GetSaveFilePath(), json);
        Debug.Log("Saving game...");
    }
}
