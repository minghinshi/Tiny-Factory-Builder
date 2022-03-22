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

    public void OccupyCells(Vector2Int position, Direction direction, BuildingType buildingType) {
        Vector2Int size = DirectionHelper.TransformSize(direction, buildingType.GetSize());
        for (int i = position.x; i < position.x + size.x; i++)
            for (int j = position.y; j < position.y + size.y; j++)
                gridOfCells[i, j].OccupyCell();
    }

    public void PlaceBuilding(Vector2Int position, Direction direction, BuildingType buildingType) {
        if (CanPlace(position, direction, buildingType))
        {
            OccupyCells(position, direction, buildingType);
            buildingType.CreateBuildingObject(position, direction);
            Debug.Log("Placed building at " + position.ToString() + " with size " + buildingType.GetSize().ToString());
        }
        else {
            Debug.Log("Cannot place building!");
        }
    }

    public float GetCellSize() {
        return cellSize;
    }
}
