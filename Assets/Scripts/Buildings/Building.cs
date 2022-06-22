using System.Collections.Generic;
using UnityEngine;

public abstract class Building : CellObject
{
    protected Direction direction;
    protected BuildingType buildingType;

    public Building(Vector2Int gridPosition, Direction direction, BuildingType buildingType)
    {
        this.gridPosition = gridPosition;
        this.direction = direction;
        this.buildingType = buildingType;
        transform = buildingType.GetNewBuildingTransform(gridPosition, direction);
    }

    public abstract void OnClick();
    public abstract void Insert(ItemStack itemStack);

    public override Vector2Int GetSize()
    {
        return buildingType.GetTransformedSize(direction);
    }

    public override void Destroy()
    {
        PlayerInventory.inventory.Store(buildingType, 1);
        Object.Destroy(transform.gameObject);
    }

    public BuildingType GetBuildingType()
    {
        return buildingType;
    }

    protected Cell<Item> RelativePositionToCell(Vector2Int relativePosition)
    {
        return Grids.itemGrid.GetCellAt(RelativeToAbsolute(relativePosition));
    }

    protected List<Cell<Item>> RelativePositionsToCells(List<Vector2Int> relativePositions)
    {
        return relativePositions.ConvertAll(x => RelativePositionToCell(x));
    }

    private Vector2Int RelativeToAbsolute(Vector2Int relativePosition)
    {
        Vector2Int actualOffset = direction.RotateVector(relativePosition);
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
}
