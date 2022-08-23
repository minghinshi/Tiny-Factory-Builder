using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static PlayerData playerData;

    private const string fileName = "save.json";

    public static GridSystem BuildingGrid => playerData.gridSystem;
    public static Inventory PlayerInventory => playerData.inventory;

    private void Start()
    {
        /*if (File.Exists(GetSaveFilePath())) LoadFile();
        else*/ CreateNewGame();
    }

    private void OnApplicationQuit()
    {
        SaveFile();
    }

    private void SaveFile()
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(GetSaveFilePath(), json);
        Debug.Log("Saving game...");
    }

    private void LoadFile()
    {
        string json = File.ReadAllText(GetSaveFilePath());
        playerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("Loading game...");
    }

    private void CreateNewGame()
    {
        playerData = PlayerData.GetNew();
        Debug.Log("New game created.");
    }

    private string GetSaveFilePath()
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
