using System.Collections.Generic;

public class SaveFile
{
    public Inventory PlayerInventory { get; set; }
    public List<Building> Buildings { get; set; }
    public List<Stage> UnlockedStages { get; set; }

    public static SaveFile GetCurrentData()
    {
        return new()
        {
            PlayerInventory = Inventory.playerInventory,
            Buildings = GridSystem.instance.GetBuildings(),
            UnlockedStages = UnlockHandler.instance.GetUnlockedStages()
        };
    }

    public void LoadFile()
    {
        Inventory.playerInventory = PlayerInventory;
        Buildings.ForEach(x => x.Initialize());
        UnlockedStages.ForEach(UnlockHandler.instance.UnlockStage);
    }
}
