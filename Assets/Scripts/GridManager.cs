public class GridManager
{
    public static GridManager instance;
    public static BuildingGridSystem buildingGrid = new BuildingGridSystem(1000, 1000, 1f);
    public static ItemGridSystem itemGrid = new ItemGridSystem(1000, 1000, 1f);
}
