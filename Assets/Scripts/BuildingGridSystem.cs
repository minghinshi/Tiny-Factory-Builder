using System.Collections.Generic;
using UnityEngine;

public class BuildingGridSystem : GridSystem
{
    private Cell[,] gridOfCells;

    public BuildingGridSystem(int width, int height, float cellSize) : base(width, height, cellSize)
    {
        gridOfCells = new Cell[width, height];
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                gridOfCells[i, j] = new Cell(new Vector2Int(i, j), this);
    }

    public override Cell[,] GridOfCells => gridOfCells;

    public bool CanPlace(Vector2Int position, Direction direction, BuildingType buildingType)
    {
        Vector2Int size = direction.TransformSize(buildingType.GetSize());
        for (int i = position.x; i < position.x + size.x; i++)
            for (int j = position.y; j < position.y + size.y; j++)
                if (!IsWithinBounds(i, j) || IsPositionOccupied(i, j))
                    return false;
        return true;
    }

    public void OccupyCells(Building building)
    {
        GetCellsInCellObject(building).ForEach(cell => cell.TryOccupyCell(building));
    }

    public void PlaceBuilding(Vector2Int position, Direction direction, BuildingType buildingType)
    {
        if (CanPlace(position, direction, buildingType))
        {
            buildingType.CreateBuilding(position, direction);
            AudioHandler.instance.PlayPlacement();
        }
    }

    public void DestroyBuilding(Vector2Int gridPosition)
    {
        if (IsWithinBounds(gridPosition))
        {
            CellObject cellObject = GetCellAt(gridPosition).GetContainedObject();
            Debug.Log(cellObject);
            if (cellObject != null)
            {
                List<Cell> cells = GetCellsInCellObject(cellObject);
                foreach (Cell cell in cells)
                    cell.EmptyCell();
                cellObject.Destroy();
                AudioHandler.instance.PlayDestroy();
            }
        }
    }
}
