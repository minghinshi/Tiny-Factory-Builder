using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : CellObject
{
    private Direction direction;
    private BuildingType buildingType;

    public Building(Cell primaryCell, Direction direction, BuildingType buildingType)
    {
        this.primaryCell = primaryCell;
        this.direction = direction;
        this.buildingType = buildingType;
        transform = buildingType.CreateBuildingTransform(primaryCell.GetGridPosition(), direction);
    }

    public Direction GetDirection() {
        return direction;
    }

    public override Vector2Int GetSize() {
        return buildingType.GetTransformedSize(direction);
    }

    public override void Destroy()
    {
        Object.Destroy(transform.gameObject);
    }
}
