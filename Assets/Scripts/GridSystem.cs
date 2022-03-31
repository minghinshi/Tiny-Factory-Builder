using System.Collections.Generic;
using UnityEngine;
public abstract class GridSystem
{
    private Vector2Int size;
    private float cellSize;

    public GridSystem(int width, int height, float cellSize)
    {
        size = new Vector2Int(width, height);
        this.cellSize = cellSize;
    }

    public abstract Cell[,] GridOfCells { get; }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / cellSize);
        return new Vector2Int(x, y);
    }

    public Vector3 GetCornerWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x * cellSize, gridPosition.y * cellSize);
    }

    public Vector3 GetCentreWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3((gridPosition.x + 0.5f) * cellSize, (gridPosition.y + 0.5f) * cellSize);
    }

    public Vector3 GetCentreWorldPosition(Vector2Int gridPosition, Vector2Int size)
    {
        return new Vector3((gridPosition.x + 0.5f * size.x) * cellSize, (gridPosition.y + 0.5f * size.y) * cellSize);
    }

    public Cell GetCellAt(Vector2Int gridPosition)
    {
        return GridOfCells[gridPosition.x, gridPosition.y];
    }

    public List<Cell> GetCellsInCellObject(CellObject cellObject)
    {
        List<Cell> cells = new List<Cell>();
        Vector2Int size = cellObject.GetSize();
        Vector2Int gridPosition = cellObject.GetGridPosition();
        for (int i = gridPosition.x; i < gridPosition.x + size.x; i++)
            for (int j = gridPosition.y; j < gridPosition.y + size.y; j++)
                cells.Add(GridOfCells[i, j]);
        return cells;
    }

    public bool IsWithinBounds(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < size.x && y < size.y);
    }

    public bool IsWithinBounds(Vector2Int position)
    {
        return IsWithinBounds(position.x, position.y);
    }

    public bool IsPositionOccupied(int x, int y)
    {
        return GridOfCells[x, y].IsOccupied();
    }

    public bool IsPositionOccupied(Vector2Int position)
    {
        return IsPositionOccupied(position.x, position.y);
    }
}
