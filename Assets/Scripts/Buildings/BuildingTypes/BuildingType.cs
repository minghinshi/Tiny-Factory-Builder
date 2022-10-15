using UnityEngine;

public abstract class BuildingType : ItemType
{
    [SerializeField] private Vector2Int size;

    public Vector2Int GetSize()
    {
        return size;
    }

    public int GetWidth()
    {
        return size.x;
    }

    public int GetHeight()
    {
        return size.y;
    }

    public Vector2Int GetTransformedSize(Direction direction)
    {
        return direction.RotateSize(size);
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition, Direction direction)
    {
        return GridSystem.instance.GetWorldPosition(gridPosition, GetTransformedSize(direction));
    }

    //Creates a building object.
    public abstract Building CreateBuilding(Vector2Int gridPosition, Direction direction);

    public void PlaceBuilding(Vector2Int position, Direction direction)
    {
        if (GridSystem.instance.CanPlace(position, GetTransformedSize(direction)))
        {
            CreateBuilding(position, direction).Initialize();
            AudioHandler.instance.PlaySound(AudioHandler.instance.placementSound);
            Inventory.playerInventory.RemoveSingle(this);
        }
    }
}
