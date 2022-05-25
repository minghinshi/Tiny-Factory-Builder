using UnityEngine;

public abstract class BuildingType : ScriptableObject
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
        return Grids.buildingGrid.GetCentreWorldPosition(gridPosition, GetTransformedSize(direction));
    }

    //Creates a building in the world.
    public Transform CreateBuildingTransform(Vector2Int gridPosition, Direction direction)
    {
        Vector3 worldPosition = GetWorldPosition(gridPosition, direction);
        Quaternion rotationQuaternion = direction.GetRotationQuaternion();
        return Instantiate(buildingPrefab, worldPosition, rotationQuaternion);
    }

    //Creates a building object.
    public abstract Building CreateBuilding(Vector2Int gridPosition, Direction direction);

    public void PlaceBuilding(Vector2Int position, Direction direction)
    {
        if (Grids.buildingGrid.CanPlace(position, direction.TransformSize(GetSize())))
        {
            Building building = CreateBuilding(position, direction);
            Grids.buildingGrid.OccupyCells(building);
            AudioHandler.instance.PlayPlacement();
        }
    }
}
