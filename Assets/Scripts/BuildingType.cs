using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class BuildingType : ScriptableObject
{
    [SerializeField] private Vector2Int size;
    [SerializeField] private Transform buildingPrefab;

    public Vector2Int GetSize() {
        return size;
    }

    public int GetWidth() {
        return size.x;
    }

    public int GetHeight() {
        return size.y;
    }

    public Transform GetBuildingPrefab() {
        return buildingPrefab;
    }

    public Vector2Int GetTransformedSize(Direction direction) {
        return direction.TransformSize(size);
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition, Direction direction)
    {
        Vector2Int transformedSize = GetTransformedSize(direction);
        float cellSize = GridManager.buildingGrid.GetCellSize();
        Vector3 position = new Vector3(
            gridPosition.x + transformedSize.x * cellSize * 0.5f,
            gridPosition.y + transformedSize.y * cellSize * 0.5f
            );
        return position;
    }

    public Transform CreateBuildingTransform(Vector2Int gridPosition, Direction direction) {
        Vector3 worldPosition = GetWorldPosition(gridPosition, direction);
        Quaternion rotationQuaternion = direction.GetRotationQuaternion();
        return Instantiate(buildingPrefab, worldPosition, rotationQuaternion);
    }

    public virtual Building CreateBuilding(Cell primaryCell, Direction direction) {
        return new Building(primaryCell, direction, this);
    }
}
