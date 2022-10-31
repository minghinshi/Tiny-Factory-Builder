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
        if (File.Exists(GetSaveFilePath())) GetSaveFile().LoadFile();
        else SaveFile.LoadNewFile();
    }

    private void SaveGame()
    {
        string json = JsonConvert.SerializeObject(SaveFile.GetCurrentData(), serializerSettings);
        File.WriteAllText(GetSaveFilePath(), json);
    }

    private SaveFile GetSaveFile()
    {
        return JsonConvert.DeserializeObject<SaveFile>(File.ReadAllText(GetSaveFilePath()), serializerSettings);
    }

    private void LoadGUI()
    {
        PlayerInventoryDisplay.instance.Initialize();
        PlayerCraftingDisplay.instance.Initialize();
        RecipeSelection.instance.Initialize();
    }
}
