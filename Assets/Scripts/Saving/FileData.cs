using System.Collections.Generic;

public class SaveFile
{
    public PlayerInventory PlayerInventory { get; set; }
    public List<Building> Buildings { get; set; }
    public List<Stage> UnlockedStages { get; set; }

    public static SaveFile GetCurrentData()
    {
        return new()
        {
            PlayerInventory = PlayerInventory.instance,
            Buildings = GridSystem.instance.GetBuildings(),
            UnlockedStages = UnlockHandler.instance.GetUnlockedStages()
        };
    }

    public void LoadFile()
    {
        PlayerInventory.instance = PlayerInventory;
        Buildings.ForEach(x => x.Initialize());
        UnlockedStages.ForEach(UnlockHandler.instance.AddStage);
    }

    public static void LoadNewFile()
    {
        PlayerInventory.instance = new(new ItemStack(GameData.starterMachine, 1));
        UnlockHandler.instance.AddStage(GameData.defaultStage);
    }
}
