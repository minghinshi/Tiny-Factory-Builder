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
        return GridSystem.instance.GetCentreWorldPosition(gridPosition, GetTransformedSize(direction));
    }

    //Creates a building object.
    public abstract Building CreateBuilding(Vector2Int gridPosition, Direction direction);

    public void PlaceBuilding(Vector2Int position, Direction direction)
    {
        if (GridSystem.instance.CanPlace(position, GetTransformedSize(direction)))
        {
            Building building = CreateBuilding(position, direction);
            GridSystem.instance.AddBuilding(building);
            AudioHandler.instance.PlayPlacement();
            Inventory.playerInventory.Remove(this, 1);
        }
    }

    public Transform GetNewBuildingTransform(Vector2Int gridPosition, Direction direction)
    {
        Transform transform = CreateTransform();
        InitializeSpriteRenderer(transform.GetComponent<SpriteRenderer>(), 0);
        transform.SetPositionAndRotation(GetWorldPosition(gridPosition, direction), direction.GetRotationQuaternion());
        transform.SetParent(ObjectFinder.worldTransform);
        transform.localScale = new Vector3(size.x, size.y, 1);
        return transform;
    }
}
