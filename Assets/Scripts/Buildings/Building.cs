using System.Collections.Generic;
using UnityEngine;

public abstract class Building
{
    protected Vector2Int gridPosition;
    protected Direction direction;
    protected Transform transform;
    protected BuildingType buildingType;

    public Building(Vector2Int gridPosition, Direction direction, BuildingType buildingType)
    {
        this.gridPosition = gridPosition;
        this.direction = direction;
        this.buildingType = buildingType;
        transform = buildingType.GetNewBuildingTransform(gridPosition, direction);
    }

    public abstract void OnClick();
    public abstract bool CanInsert();
    public virtual bool CanInsertByPlayer() => CanInsert();
    public abstract bool CanExtract();
    public abstract void Insert(ItemStack itemStack);
    public abstract ItemStack Extract();

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    public Vector2Int GetSize()
    {
        return buildingType.GetTransformedSize(direction);
    }

    public BuildingType GetBuildingType()
    {
        return buildingType;
    }

    public virtual void Destroy()
    {
        PlayerInventory.inventory.Store(buildingType, 1);
        Object.Destroy(transform.gameObject);
    }

    protected Cell RelativePositionToCell(Vector2Int relativePosition)
    {
        return Grids.grid.GetCellAt(RelativeToAbsolute(relativePosition));
    }

    protected List<Cell> RelativePositionsToCells(List<Vector2Int> relativePositions)
    {
        return relativePositions.ConvertAll(x => RelativePositionToCell(x));
    }

    private Vector2Int RelativeToAbsolute(Vector2Int relativePosition)
    {
        return gridPosition + direction.RotateVector(relativePosition) + GetDirectionalOffset();
    }

    private Vector2Int GetDirectionalOffset()
    {
        return direction switch
        {
            Direction.North => Vector2Int.zero,
            Direction.East => Vector2Int.up * (buildingType.GetSize().x - 1),
            Direction.South => buildingType.GetSize() - new Vector2Int(1, 1),
            Direction.West => Vector2Int.right * (buildingType.GetSize().y - 1),
            _ => throw new System.Exception("Invalid direction."),
        };
    }
}
