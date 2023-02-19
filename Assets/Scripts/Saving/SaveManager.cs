using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private JsonSerializerSettings serializerSettings;
    private const float SaveInterval = 30f;
    private bool inWebGL;

    private void Start()
    {
        inWebGL = Application.platform == RuntimePlatform.WebGLPlayer;
        serializerSettings = GetSerializerSettings();
        LoadGame();
        LoadGUI();
        InvokeRepeating(nameof(SaveGame), 0, SaveInterval);
        new ItemLabel.Builder().Build();
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
        return Application.isEditor ? Application.dataPath + "/TestSaves" : Application.persistentDataPath;
    }

    private void LoadGame()
    {
        if (HasSaveFile()) GetSaveFile().LoadFile();
        else SaveFile.LoadNewFile();
    }

    private void SaveGame()
    {
        string json = JsonConvert.SerializeObject(SaveFile.GetCurrentData(), serializerSettings);
        if (inWebGL) PlayerPrefs.SetString("Save", json);
        else File.WriteAllText(GetSaveFilePath(), json);
    }

    private SaveFile GetSaveFile()
    {
        return JsonConvert.DeserializeObject<SaveFile>(GetSavedJson(), serializerSettings);
    }

    private string GetSavedJson()
    {
        if (inWebGL) return PlayerPrefs.GetString("Save");
        return File.ReadAllText(GetSaveFilePath());
    }

    private bool HasSaveFile() {
        return (inWebGL && PlayerPrefs.HasKey("Save")) || (!inWebGL && File.Exists(GetSaveFilePath()));
    }

    private void LoadGUI()
    {
        PlayerInventoryDisplay.instance.Initialize();
        PlayerCraftingDisplay.instance.Initialize();
        RecipeSelection.instance.Initialize();
    }
}
