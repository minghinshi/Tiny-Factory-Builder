public class GridManager
{
    public static GridManager instance;
    public static GridSystem<Building> buildingGrid = new GridSystem<Building>(1000, 1000, 1f, false);
    public static GridSystem<Item> itemGrid = new GridSystem<Item>(1000, 1000, 1f, true);
}
