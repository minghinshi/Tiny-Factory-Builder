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

    public static SaveFile GetNewData()
    {
        return new()
        {
            PlayerInventory = new(GameData.starterMachines.ConvertAll(x => new ItemStack(x, 1)).ToArray()),
            Buildings = new(),
            UnlockedStages = new List<Stage>() { GameData.defaultStage }
        };
    }

    public void LoadFile()
    {
        Inventory.playerInventory = PlayerInventory;
        Buildings.ForEach(x => x.Initialize());
        UnlockedStages.ForEach(UnlockHandler.instance.UnlockStage);
    }
}
