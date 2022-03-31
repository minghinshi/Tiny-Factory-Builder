using UnityEngine;

public class ItemGridSystem : GridSystem
{
    private ItemCell[,] gridOfCells;

    public ItemGridSystem(int width, int height, float cellSize) : base(width, height, cellSize)
    {
        gridOfCells = new ItemCell[width, height];
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                gridOfCells[i, j] = new ItemCell(new Vector2Int(i, j), this);
    }

    public override Cell[,] GridOfCells => gridOfCells;

    public new ItemCell GetCellAt(Vector2Int gridPosition)
    {
        return gridOfCells[gridPosition.x, gridPosition.y];
    }
}
