using System.Collections.Generic;
using UnityEngine;
public class GridSystem
{
    private Vector2Int size;
    private float cellSize;
    private Cell[,] gridOfCells;

    public GridSystem(int width, int height, float cellSize)
    {
        size = new Vector2Int(width, height);
        this.cellSize = cellSize;
        gridOfCells = new Cell[width, height];
        CreateGridOfCells(width, height);
    }

    private void CreateGridOfCells(int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2Int gridPosition = new Vector2Int(i, j);
                gridOfCells[i, j] = new Cell(gridPosition, GetCentreWorldPosition(gridPosition));
            }
        }
    }

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
        return gridOfCells[gridPosition.x, gridPosition.y];
    }

    public List<Cell> GetCellsInBuilding(Building building)
    {
        List<Cell> cells = new List<Cell>();
        Vector2Int size = building.GetSize();
        Vector2Int gridPosition = building.GetGridPosition();
        for (int i = gridPosition.x; i < gridPosition.x + size.x; i++)
            for (int j = gridPosition.y; j < gridPosition.y + size.y; j++)
                cells.Add(gridOfCells[i, j]);
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
        return gridOfCells[x, y].IsOccupied();
    }

    public bool IsPositionOccupied(Vector2Int position)
    {
        return IsPositionOccupied(position.x, position.y);
    }

    public bool CanPlace(Vector2Int position, Vector2Int size)
    {
        for (int i = position.x; i < position.x + size.x; i++)
            for (int j = position.y; j < position.y + size.y; j++)
                if (!IsWithinBounds(i, j) || IsPositionOccupied(i, j))
                    return false;
        return true;
    }

    public void OccupyCells(Building building)
    {
        GetCellsInBuilding(building).ForEach(cell => cell.OccupyWith(building));
    }

    public Building GetBuildingAt(Vector2Int gridPosition)
    {
        if (!IsWithinBounds(gridPosition)) return null;
        return GetCellAt(gridPosition).GetContainedBuilding();
    }

    public Building GetBuildingAt(Vector3 worldPosition)
    {
        return GetBuildingAt(GetGridPosition(worldPosition));
    }

    public void DestroyBuildingAt(Vector2Int gridPosition)
    {
        Building building = GetBuildingAt(gridPosition);
        if (building != null) DestroyBuilding(building);
    }

    private void DestroyBuilding(Building building)
    {
        foreach (Cell cell in GetCellsInBuilding(building)) cell.Empty();
        building.Destroy();
        AudioHandler.instance.PlayDestroy();
    }
}
