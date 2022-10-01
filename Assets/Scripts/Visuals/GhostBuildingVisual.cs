using UnityEngine;

public class GhostBuildingVisual : BuildingVisual
{
    private BuildingPlacement placement;

    private Vector3 targetPosition;
    private float targetRotation;

    private readonly Color canPlaceColor = new(130 / 255f, 224 / 255f, 170 / 255f, .75f);
    private readonly Color cannotPlaceColor = new(241 / 255f, 148 / 255f, 138 / 255f, .75f);

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

    public static GhostBuildingVisual Create(BuildingPlacement placement)
    {
        GhostBuildingVisual visual = CreateGameObject().AddComponent<GhostBuildingVisual>();
        visual.Initialize(placement);
        return visual;
    }

    private void Initialize(BuildingPlacement placement)
    {
        this.placement = placement;
        Initialize();
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
