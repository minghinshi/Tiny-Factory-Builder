using UnityEngine;

public class GhostVisual : BuildingVisual
{
    private BuildingPlacement placement;

    private Vector3 targetPosition;
    private float targetRotation;

    private readonly Color canPlaceColor = new(130 / 255f, 224 / 255f, 170 / 255f, .75f);
    private readonly Color cannotPlaceColor = new(241 / 255f, 148 / 255f, 138 / 255f, .75f);

    public static GhostVisual Create()
    {
        return Instantiate(Prefabs.ghostVisuals, Transforms.worldTransform).GetComponent<GhostVisual>();
    }

    public void Initialize(BuildingPlacement placement)
    {
        this.placement = placement;
        Initialize();
        spriteRenderer.sortingOrder = 1;
    }

    protected override BuildingType GetBuildingType()
    {
        return (BuildingType)placement.GetItemType();
    }

    protected override Direction GetDirection()
    {
        return placement.GetDirection();
    }

    protected override Vector2Int GetGridPosition()
    {
        return Mouse.instance.GetGridPosition();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 20f);
        transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(transform.eulerAngles.z, targetRotation, Time.deltaTime * 20f));
    }

    public void SetColor(bool canPlace)
    {
        spriteRenderer.color = canPlace ? canPlaceColor : cannotPlaceColor;
    }

    public void UpdateTarget(Vector2Int gridPosition, Direction direction)
    {
        targetPosition = GetBuildingType().GetWorldPosition(gridPosition, direction);
        targetRotation = direction.GetRotationInDegrees();
    }
}
