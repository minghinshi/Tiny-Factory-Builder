using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private JsonSerializerSettings serializerSettings;

    private void Start()
    {
        serializerSettings = GetSerializerSettings();
        LoadGame();
        LoadGUI();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private JsonSerializerSettings GetSerializerSettings()
    {
        JsonSerializerSettings settings = new();
        settings.Converters.Add(new ScriptableObjectConverter());
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
        GetSaveFile().LoadFile();
    }

    private void SaveGame()
    {
        string json = JsonConvert.SerializeObject(SaveFile.GetCurrentData(), serializerSettings);
        File.WriteAllText(GetSaveFilePath(), json);
    }

    private SaveFile GetSaveFile()
    {
        return File.Exists(GetSaveFilePath())
            ? JsonConvert.DeserializeObject<SaveFile>(File.ReadAllText(GetSaveFilePath()), serializerSettings)
            : SaveFile.GetNewData();
    }

    private void LoadGUI()
    {
        PlayerInventory.instance.Initialize();
        PlayerCrafting.instance.Initialize();
        RecipeSelection.instance.Initialize();
    }
}
