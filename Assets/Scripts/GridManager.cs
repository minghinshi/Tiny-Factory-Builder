public class GridManager
{
    public static GridManager instance;
    public static GridSystem buildingGrid = new GridSystem(1000, 1000, 1f);
    public static GridSystem itemGrid = new GridSystem(1000, 1000, 1f, true);
}
