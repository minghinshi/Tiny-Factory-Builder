/*using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(RectTransform))]
public class BuildingVisuals : MonoBehaviour
{
    public static readonly Transform worldTransform = GameObject.Find("WorldCanvas").transform;

    private BuildingType buildingType;
    private Vector2Int gridPosition;
    private Direction direction;

    public static BuildingVisuals Create(BuildingType buildingType, Vector2Int gridPosition, Direction direction)
    {
        BuildingVisuals output = new GameObject().AddComponent<BuildingVisuals>();
        output.Initialize(buildingType, gridPosition, direction);
        return output;
    }

    private void Initialize(BuildingType buildingType, Vector2Int gridPosition, Direction direction)
    {
        this.buildingType = buildingType;
        this.gridPosition = gridPosition;
        this.direction = direction;
        InitializeSpriteRenderer();
        InitializeTransform();
    }

    private void InitializeSpriteRenderer()
    {
        GetComponent<SpriteRenderer>().sprite = buildingType.GetSprite();
    }

    private void InitializeTransform()
    {
        transform.SetPositionAndRotation(GetPosition(), GetRotation());
        transform.SetParent(worldTransform);
        transform.localScale = (Vector3)(Vector3Int)buildingType.GetSize();
    }

    private Vector3 GetPosition()
    {
        return GridSystem.instance.GetWorldPosition(gridPosition, buildingType.GetTransformedSize(direction));
    }

    private Quaternion GetRotation()
    {
        return direction.GetRotationQuaternion();
    }
}
*/