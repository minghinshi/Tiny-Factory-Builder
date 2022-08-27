using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridSystem
{
    public static GridSystem instance = new GridSystem();

    private const float cellSize = 1f;
    private List<Building> buildings = new();
    private Dictionary<Vector2Int, Building> buildingPositions = new();

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

    public List<Vector2Int> GetCellsInBuilding(Building building)
    {
        List<Vector2Int> cells = new List<Vector2Int>();
        Vector2Int size = building.GetSize();
        Vector2Int gridPosition = building.GetGridPosition();
        for (int i = gridPosition.x; i < gridPosition.x + size.x; i++)
            for (int j = gridPosition.y; j < gridPosition.y + size.y; j++)
                cells.Add(new Vector2Int(i, j));
        return cells;
    }

    public bool IsPositionOccupied(Vector2Int position)
    {
        return buildingPositions.ContainsKey(position);
    }

    public bool CanPlace(Vector2Int position, Vector2Int size)
    {
        for (int i = position.x; i < position.x + size.x; i++)
            for (int j = position.y; j < position.y + size.y; j++)
                if (IsPositionOccupied(new Vector2Int(i, j))) return false;
        return true;
    }

    public void AddBuilding(Building building)
    {
        GetCellsInBuilding(building).ForEach(x => OccupyCell(building, x));
        buildings.Add(building);
    }

    public Building GetBuildingAt(Vector2Int gridPosition)
    {
        return IsPositionOccupied(gridPosition) ? buildingPositions[gridPosition] : null;
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

    public List<Building> GetBuildings()
    {
        return buildings;
    }

    private void DestroyBuilding(Building building)
    {
        foreach (Vector2Int position in GetCellsInBuilding(building)) buildingPositions.Remove(position);
        building.Destroy();
        buildings.Remove(building);
        AudioHandler.instance.PlayDestroy();
    }

    private void OccupyCell(Building building, Vector2Int position)
    {
        bool alreadyOccupied = !buildingPositions.TryAdd(position, building);
        if (alreadyOccupied) Debug.LogError("Cell has already been occupied!");
    }
}
