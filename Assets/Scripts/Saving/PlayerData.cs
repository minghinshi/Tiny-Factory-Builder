using System;

[Serializable]
public class PlayerData
{
    public GridSystem gridSystem;
    public Inventory inventory;

    public static PlayerData GetNew()
    {
        return new PlayerData()
        {
            gridSystem = new GridSystem(),
            inventory = new Inventory(DataLoader.starterMachines.ConvertAll(x => new ItemStack(x, 1)).ToArray())
        };
    }
}
