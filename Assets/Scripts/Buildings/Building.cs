using UnityEngine;
using Newtonsoft.Json;
using JsonSubTypes;

[JsonConverter(typeof(JsonSubtypes))]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(Conveyor), "conveyorType")]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(Gatherer), "gathererType")]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(Machine), "machineType")]
public abstract class Building
{
    [JsonProperty] protected Vector2Int gridPosition;
    [JsonProperty] protected Direction direction;
    protected Transform transform;

    public Building(Vector2Int gridPosition, Direction direction)
    {
        this.gridPosition = gridPosition;
        this.direction = direction;
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
        return GetBuildingType().GetTransformedSize(direction);
    }

    public abstract BuildingType GetBuildingType();

    public virtual void Initialize()
    {
        transform = GetBuildingType().GetNewBuildingTransform(gridPosition, direction);
        GridSystem.instance.AddBuilding(this);
        Debug.Log(GetBuildingType().GetName());
    }

    public virtual void Destroy()
    {
        Inventory.playerInventory.Store(GetBuildingType(), 1);
        Object.Destroy(transform.gameObject);
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
            Direction.East => Vector2Int.up * (GetBuildingType().GetSize().x - 1),
            Direction.South => GetBuildingType().GetSize() - new Vector2Int(1, 1),
            Direction.West => Vector2Int.right * (GetBuildingType().GetSize().y - 1),
            _ => throw new System.Exception("Invalid direction."),
        };
    }
}
