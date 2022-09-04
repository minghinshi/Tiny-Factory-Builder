using System.Collections.Generic;

public class FileData
{
    public Inventory PlayerInventory { get; set; }
    public List<Building> Buildings { get; set; }

    public static FileData GetCurrentData()
    {
        return new()
        {
            PlayerInventory = Inventory.playerInventory,
            Buildings = GridSystem.instance.GetBuildings()
        };
    }

    public void LoadFile()
    {
        Inventory.playerInventory = PlayerInventory;
        Buildings.ForEach(x => x.Initialize());
    }
}
