using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class BuildingType : ScriptableObject
{
    [SerializeField] private Vector2Int size;
    [SerializeField] private Transform buildingPrefab; 

    public BuildingType(int width, int height) {
        size = new Vector2Int(width, height);
    }

    public Vector2Int GetSize() {
        return size;
    }

    public int GetWidth() {
        return size.x;
    }

    public int GetHeight() {
        return size.y;
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition, Direction direction) {
        Vector2Int transformedSize = DirectionHelper.TransformSize(direction, size);
        float cellSize = GridManager.buildingGrid.GetCellSize();
        Vector3 position = new(
            gridPosition.x + transformedSize.x * cellSize * 0.5f,
            gridPosition.y + transformedSize.y * cellSize * 0.5f
            );
        return position;
    }

    public Transform CreateBuildingObject(Vector2Int cornerPosition, Direction direction) {
        Vector3 position = GetWorldPosition(cornerPosition, direction);
        Quaternion rotation = DirectionHelper.GetRotationQuaternion(direction);
        Transform buildingObject = Instantiate(buildingPrefab, position, rotation);
        return buildingObject;
    }
}
