using System.Collections.Generic;
using UnityEngine;
public class GridSystem<TCellObject> where TCellObject : CellObject
{
    private Vector2Int size;
    private float cellSize;
    private Cell<TCellObject>[,] gridOfCells;

    public GridSystem(int width, int height, float cellSize, bool startBlocked)
    {
        size = new Vector2Int(width, height);
        this.cellSize = cellSize;
        gridOfCells = new Cell<TCellObject>[width, height];
        CreateGridOfCells(width, height, startBlocked);
    }

    private void CreateGridOfCells(int width, int height, bool startBlocked)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2Int gridPosition = new Vector2Int(i, j);
                gridOfCells[i, j] = new Cell<TCellObject>(gridPosition, GetCentreWorldPosition(gridPosition), startBlocked);
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

    public Cell<TCellObject> GetCellAt(Vector2Int gridPosition)
    {
        return gridOfCells[gridPosition.x, gridPosition.y];
    }

    public List<Cell<TCellObject>> GetCellsInCellObject(TCellObject cellObject)
    {
        List<Cell<TCellObject>> cells = new List<Cell<TCellObject>>();
        Vector2Int size = cellObject.GetSize();
        Vector2Int gridPosition = cellObject.GetGridPosition();
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

    public void OccupyCells(TCellObject cellObject)
    {
        GetCellsInCellObject(cellObject).ForEach(cell => cell.TryOccupyCell(cellObject));
    }

    public TCellObject GetCellObjectAt(Vector2Int gridPosition)
    {
        if (!IsWithinBounds(gridPosition)) return null;
        return GetCellAt(gridPosition).GetContainedObject();
    }

    public void TryDestroyCellObject(Vector2Int gridPosition)
    {
        TCellObject cellObject = GetCellObjectAt(gridPosition);
        if (cellObject != null) DestroyCellObject(cellObject);
    }

    private void DestroyCellObject(TCellObject cellObject)
    {
        foreach (Cell<TCellObject> cell in GetCellsInCellObject(cellObject))
            cell.EmptyCell();
        cellObject.Destroy();
        AudioHandler.instance.PlayDestroy();
    }
}
