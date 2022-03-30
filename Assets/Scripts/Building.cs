using UnityEngine;

public class Building : CellObject
{
    protected Direction direction;
    protected BuildingType buildingType;

    public Building(Cell primaryCell, Direction direction, BuildingType buildingType)
    {
        gridPosition = primaryCell.GetGridPosition();
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

    //Returns a grid position based on the position of the building, the offset from the building and the direction the building is facing.
    public Vector2Int GetGridPositionFromOffset(Vector2Int originalOffset)
    {
        Vector2Int actualOffset = direction.RotateVector(originalOffset);
        switch (direction)
        {
            case Direction.East:
                actualOffset.y += buildingType.GetSize().x - 1;
                break;
            case Direction.South:
                actualOffset += buildingType.GetSize() - new Vector2Int(1, 1);
                break;
            case Direction.West:
                actualOffset.x += buildingType.GetSize().y - 1;
                break;
        }
        return gridPosition + actualOffset;
    }

    public override void Destroy()
    {
        Object.Destroy(transform.gameObject);
    }
}
