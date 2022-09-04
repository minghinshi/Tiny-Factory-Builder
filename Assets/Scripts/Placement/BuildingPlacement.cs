using UnityEngine;

public class BuildingPlacement : Placement
{
    private readonly BuildingType buildingType;
    private readonly Transform previewTransform;
    private readonly SpriteRenderer spriteRenderer;

    private Direction direction = Direction.North;

    private Vector3 previewTargetPosition;
    private float targetRotation;

    private readonly Color canPlaceColor = new Color(130 / 255f, 224 / 255f, 170 / 255f, .75f);
    private readonly Color cannotPlaceColor = new Color(241 / 255f, 148 / 255f, 138 / 255f, .75f);

    private bool placedBuildingHere;

    public BuildingPlacement(BuildingType buildingType)
    {
        this.buildingType = buildingType;
        previewTransform = buildingType.GetNewBuildingTransform(Mouse.instance.GetGridPosition(), Direction.North);
        spriteRenderer = previewTransform.GetComponent<SpriteRenderer>();

        UpdatePreview();
        Mouse.instance.GridPositionChanged += OnMousePositionChanged;
    }

    public override void Update()
    {
        RenderPreview();
        CheckInputs();
    }

    public override void Destroy()
    {
        Mouse.instance.GridPositionChanged -= OnMousePositionChanged;
        Object.Destroy(previewTransform.gameObject);
    }

    public override ItemType GetItemType()
    {
        return buildingType;
    }

    protected override void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.R)) RotateBuilding();
        if (Input.GetMouseButton(0) && Mouse.instance.IsPointingAtWorld()) PlaceBuilding();
        base.CheckInputs();
    }

    protected override void DestroyBuilding()
    {
        base.DestroyBuilding();
        placedBuildingHere = false;
    }

    private void PlaceBuilding()
    {
        if (placedBuildingHere) return;
        buildingType.PlaceBuilding(Mouse.instance.GetGridPosition(), direction);
        placedBuildingHere = true;
    }

    private void RotateBuilding()
    {
        direction = direction.RotateClockwise();
        targetRotation = direction.GetRotationInDegrees();
    }

    private void OnMousePositionChanged(Vector2Int gridPosition)
    {
        UpdatePreview();
        placedBuildingHere = false;
    }

    private void UpdatePreview()
    {
        UpdateTargetPosition();
        UpdateColor();
    }

    private void UpdateTargetPosition()
    {
        previewTargetPosition = GridSystem.instance.GetCentreWorldPosition(Mouse.instance.GetGridPosition(), buildingType.GetSize());
    }

    private void UpdateColor()
    {
        if (GridSystem.instance.CanPlace(Mouse.instance.GetGridPosition(), buildingType.GetTransformedSize(direction))) SetCanPlaceColor();
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

    private void RenderPreview()
    {
        previewTransform.position = Vector3.Lerp(previewTransform.position, previewTargetPosition, Time.deltaTime * 20f);
        previewTransform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(previewTransform.eulerAngles.z, targetRotation, Time.deltaTime * 20f));
    }
}
