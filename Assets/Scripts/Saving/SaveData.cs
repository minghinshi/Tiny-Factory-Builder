public class SaveData
{
    public Inventory playerInventory;
    public BuildingData buildingData;

    private SaveData(Inventory playerInventory, BuildingData buildingData) {
        this.playerInventory = playerInventory;
        this.buildingData = buildingData;
        UnityEngine.Debug.Log("Copy created.");
    }

    public static SaveData GetCurrentData() {
        UnityEngine.Debug.Log("New SaveData created.");
        return new SaveData(Inventory.playerInventory, new BuildingData(GridSystem.instance.GetBuildings()));
    }
}
