using UnityEngine;

public class BuildingType : ScriptableObject
{
    [SerializeField] private Vector2Int size;
    [SerializeField] private Transform buildingPrefab;

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

    public Transform GetBuildingPrefab()
    {
        return buildingPrefab;
    }

    public Vector2Int GetTransformedSize(Direction direction)
    {
        return direction.TransformSize(size);
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition, Direction direction)
    {
        return GridManager.buildingGrid.GetCentreWorldPosition(gridPosition, GetTransformedSize(direction));
    }

    //Creates a building in the world.
    public Transform CreateBuildingTransform(Vector2Int gridPosition, Direction direction)
    {
        Vector3 worldPosition = GetWorldPosition(gridPosition, direction);
        Quaternion rotationQuaternion = direction.GetRotationQuaternion();
        return Instantiate(buildingPrefab, worldPosition, rotationQuaternion);
    }

    //Creates a building object.
    public virtual Building CreateBuilding(Cell primaryCell, Direction direction)
    {
        return new Building(primaryCell, direction, this);
    }
}
