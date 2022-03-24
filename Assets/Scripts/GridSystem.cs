using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GridSystem
{
    private Vector2Int size;
    private Cell[,] gridOfCells;
    private float cellSize;

    public GridSystem(int width, int height, float gridSize) {
        size = new Vector2Int(width, height);
        gridOfCells = new Cell[width, height];
        this.cellSize = gridSize;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                gridOfCells[i, j] = new Cell(i, j);
            }
        }
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition) {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / cellSize);
        return new Vector2Int(x, y);
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition) {
        return new Vector3(gridPosition.x * cellSize, gridPosition.y * cellSize);
    }


    public Cell GetCellAt(Vector2Int gridPosition) {
        return gridOfCells[gridPosition.x, gridPosition.y];
    }

    public List<Cell> GetCellsInCellObject(CellObject cellObject) {
        List<Cell> cells = new List<Cell>();
        Vector2Int size = cellObject.GetSize();
        Vector2Int gridPosition = cellObject.GetGridPosition();
        for (int i = gridPosition.x; i < gridPosition.x + size.x; i++)
            for (int j = gridPosition.y; j < gridPosition.y + size.y; j++)
                cells.Add(gridOfCells[i, j]);
        return cells;
    }


    public Vector3 SnapWorldPosition(Vector3 worldPosition) {
        return GetWorldPosition(GetGridPosition(worldPosition));
    }

    public bool IsWithinBounds(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < size.x && y < size.y);
    }

    public bool IsWithinBounds(Vector2Int position) {
        return IsWithinBounds(position.x, position.y);
    }

    public bool IsPositionOccupied(int x, int y)
    {
        return gridOfCells[x, y].isOccupied();
    }

    public bool IsPositionOccupied(Vector2Int position) {
        return IsPositionOccupied(position.x, position.y);
    }

    public bool CanPlace(Vector2Int position, Direction direction, BuildingType buildingType) {
        Vector2Int size = DirectionHelper.TransformSize(direction, buildingType.GetSize());
        for (int i = position.x; i < position.x + size.x; i++)
            for (int j = position.y; j < position.y + size.y; j++)
                if (!IsWithinBounds(i, j) || IsPositionOccupied(i, j))
                    return false;
        return true;
    }

    public void OccupyCells(Building building) {
        GetCellsInCellObject(building).ForEach(cell => cell.OccupyCell(building));
    }

    public void PlaceBuilding(Vector2Int position, Direction direction, BuildingType buildingType) {
        if (CanPlace(position, direction, buildingType))
        {
            Building building = buildingType.CreateBuilding(GetCellAt(position), direction);
            OccupyCells(building);
            
            AudioHandler.instance.PlayPlacement();
        }
    }

    public void DestroyBuilding(Vector2Int gridPosition) {
        if (IsWithinBounds(gridPosition)) {
            CellObject cellObject = GetCellAt(gridPosition).GetContainedObject();
            if (cellObject != null) {
                List<Cell> cells = GetCellsInCellObject(cellObject);
                foreach (Cell cell in cells)
                    cell.EmptyCell();
                cellObject.Destroy();
                AudioHandler.instance.PlayDestroy();
            }
        }
    }

    public float GetCellSize() {
        return cellSize;
    }
}
