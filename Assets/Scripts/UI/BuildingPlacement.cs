using UnityEngine;

public class BuildingPlacement : Placement
{
    private readonly BuildingType buildingType;

    private Vector2Int gridPosition;
    private Direction direction = Direction.North;

    private Vector3 previewTargetPosition;
    private Vector3 targetRotation;

    private readonly Color canPlaceColor = new Color(46 / 255f, 204 / 255f, 113 / 255f, .5f);
    private readonly Color cannotPlaceColor = new Color(231 / 255f, 76 / 255f, 60 / 255f, .5f);

    private bool placedBuildingHere;

    public BuildingPlacement(BuildingType buildingType)
    {
        this.buildingType = buildingType;
        previewTransform = buildingType.GetNewBuildingTransform(GetMouseGridPosition(), Direction.North);
        spriteRenderer = previewTransform.GetComponent<SpriteRenderer>();
        UpdateGridPosition(GetMouseGridPosition());
    }

    public override void Update()
    {
        RenderPreview();
        if (Input.GetKeyDown(KeyCode.R)) Rotate();
        if (Input.GetMouseButton(0) && IsMousePointingAtWorld()) Place();
    }

    public override ItemType GetItemType()
    {
        return buildingType;
    }

    protected override void RenderPreview()
    {
        Vector2Int pointerGridPosition = GetMouseGridPosition();
        if (!pointerGridPosition.Equals(gridPosition)) UpdateGridPosition(pointerGridPosition);
        LerpTransform();
    }

    protected override void Place()
    {
        if (placedBuildingHere) return;
        buildingType.PlaceBuilding(gridPosition, direction);
        placedBuildingHere = true;
    }

    private void Rotate()
    {
        direction = direction.RotateClockwise();
        targetRotation = direction.GetEulerAngles();
    }

    private void UpdateGridPosition(Vector2Int gridPosition)
    {
        this.gridPosition = gridPosition;
        previewTargetPosition = Grids.buildingGrid.GetCentreWorldPosition(gridPosition, buildingType.GetSize());
        placedBuildingHere = false;
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (Grids.buildingGrid.CanPlace(gridPosition, buildingType.GetTransformedSize(direction))) SetCanPlaceColor();
        else SetCannotPlaceColor();
    }

    private void SetCanPlaceColor()
    {
        spriteRenderer.color = canPlaceColor;
    }

    private void SetCannotPlaceColor()
    {
        spriteRenderer.color = cannotPlaceColor;
    }

    private void LerpTransform()
    {
        previewTransform.position = Vector3.Lerp(previewTransform.position, previewTargetPosition, Time.deltaTime * 20f);
        previewTransform.eulerAngles = Vector3.Lerp(previewTransform.eulerAngles, targetRotation, Time.deltaTime * 20f);
    }
}
