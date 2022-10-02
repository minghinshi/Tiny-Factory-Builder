using JsonSubTypes;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[JsonConverter(typeof(JsonSubtypes))]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(Conveyor), "conveyorType")]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(Gatherer), "gathererType")]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(Machine), "machineType")]
public abstract class Building
{
    [JsonProperty] protected Vector2Int gridPosition;
    [JsonProperty] protected Direction direction;

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
    public abstract BuildingType GetBuildingType();

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    public Direction GetDirection()
    {
        return direction;
    }

    public Vector2Int GetSize()
    {
        return GetBuildingType().GetTransformedSize(direction);
    }

    public List<Vector2Int> GetContainedCells()
    {
        List<Vector2Int> cells = new();
        Vector2Int size = GetSize();
        for (int i = gridPosition.x; i < gridPosition.x + size.x; i++)
            for (int j = gridPosition.y; j < gridPosition.y + size.y; j++)
                cells.Add(new Vector2Int(i, j));
        return cells;
    }

    public void Initialize() {
        InitializeData();
        CreateVisuals();
    }

    public virtual void Destroy()
    {
        Inventory.playerInventory.Store(GetBuildingType(), 1);
        GetVisuals().Destroy();
    }

    protected abstract void CreateVisuals();
    protected abstract BuildingVisual GetVisuals();

    protected virtual void InitializeData()
    {
        GridSystem.instance.AddBuilding(this);
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
