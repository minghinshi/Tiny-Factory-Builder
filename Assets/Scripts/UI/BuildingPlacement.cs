using UnityEngine;

public class BuildingPlacement : Placement
{
    private BuildingType buildingType;
    protected Transform previewTransform;
    protected SpriteRenderer spriteRenderer;

    private Vector2Int gridPosition;
    private Direction direction = Direction.North;

    private Vector3 previewTargetPosition;
    private Vector3 targetRotation;

    private readonly Color canPlaceColor = new Color(46 / 255f, 204 / 255f, 113 / 255f, .5f);
    private readonly Color cannotPlaceColor = new Color(231 / 255f, 76 / 255f, 60 / 255f, .5f);

    private bool placedBuildingHere;

    public BuildingPlacement(BuildingType buildingType, Vector3 worldPosition)
    {
        this.buildingType = buildingType;
        gridPosition = Grids.buildingGrid.GetGridPosition(worldPosition);
        previewTransform = buildingType.GetNewBuildingTransform(gridPosition, Direction.North);
        spriteRenderer = previewTransform.GetComponent<SpriteRenderer>();
    }

    public void OnUpdate() {

    }

    public void RenderPreview(Vector3 worldPosition)
    {
        Vector2Int pointerGridPosition = Grids.buildingGrid.GetGridPosition(worldPosition);
        if (!pointerGridPosition.Equals(gridPosition))
        {
            UpdateGridPosition(pointerGridPosition);
            UpdateColor();
        }
        LerpTransform();
    }

    public void Place()
    {
        if (placedBuildingHere) return;
        buildingType.PlaceBuilding(gridPosition, direction);
        placedBuildingHere = true;
        if (!PlayerInventory.inventory.Contains(buildingType)) Terminate();
    }

    public void Rotate()
    {
        direction = direction.RotateClockwise();
        targetRotation = direction.GetEulerAngles();
    }

    public void Terminate()
    {
        Object.Destroy(previewTransform);
    }

    private void UpdateGridPosition(Vector2Int gridPosition)
    {
        this.gridPosition = gridPosition;
        previewTargetPosition = Grids.buildingGrid.GetCentreWorldPosition(gridPosition, buildingType.GetSize());
        placedBuildingHere = false;
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
