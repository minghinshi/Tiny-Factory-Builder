public class Grids
{
    public static readonly GridSystem<Building> buildingGrid = new GridSystem<Building>(1000, 1000, 1f, false);
    public static readonly GridSystem<Item> itemGrid = new GridSystem<Item>(1000, 1000, 1f, true);
}