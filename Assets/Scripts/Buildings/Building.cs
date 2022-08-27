using UnityEngine;

public abstract class Building
{
    [SerializeField] protected Vector2Int gridPosition;
    [SerializeField] protected Direction direction;
    [SerializeField] protected BuildingType buildingType;

    protected Transform transform;

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
        Inventory.playerInventory.Store(buildingType, 1);
        UnityEngine.Object.Destroy(transform.gameObject);
    }

    protected Vector2Int RelativeToAbsolute(Vector2Int relativePosition)
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
