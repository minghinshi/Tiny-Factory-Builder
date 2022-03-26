using UnityEngine;

public class Building : CellObject
{
    protected Direction direction;
    protected BuildingType buildingType;

    public Building(Cell primaryCell, Direction direction, BuildingType buildingType)
    {
        this.primaryCell = primaryCell;
        this.direction = direction;
        this.buildingType = buildingType;
        transform = buildingType.CreateBuildingTransform(primaryCell.GetGridPosition(), direction);

        GridManager.buildingGrid.OccupyCells(this);
    }

    public Direction GetDirection()
    {
        return direction;
    }

    public override Vector2Int GetSize()
    {
        return buildingType.GetTransformedSize(direction);
    }

    public override void Destroy()
    {
        Object.Destroy(transform.gameObject);
    }
}
