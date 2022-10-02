using UnityEngine;

public class BuildingPlacement : Placement
{
    private readonly BuildingType buildingType;
    private Direction direction = Direction.North;
    private GhostVisual visual;
    private bool placedBuildingHere;

    public BuildingPlacement(BuildingType buildingType)
    {
        this.buildingType = buildingType;
        CreateVisual();
        Mouse.instance.GridPositionChanged += OnMousePositionChanged;
    }

    public override void Update()
    {
        CheckInputs();
    }

    public override void Destroy()
    {
        Mouse.instance.GridPositionChanged -= OnMousePositionChanged;
        visual.Destroy();
    }

    public override ItemType GetItemType()
    {
        return buildingType;
    }

    public Direction GetDirection()
    {
        return direction;
    }

    protected override void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.R)) RotateBuilding();
        if (Input.GetMouseButton(0) && Mouse.instance.IsPointingAtWorld()) PlaceBuilding();
        base.CheckInputs();
    }

    private void CreateVisual() {
        visual = GhostVisual.Create();
        visual.Initialize(this);
        UpdateVisual();
    }

    private void OnMousePositionChanged(Vector2Int _)
    {
        UpdateVisual();
        placedBuildingHere = false;
    }

    private void RotateBuilding()
    {
        direction = direction.RotateClockwise();
        UpdateVisual();
    }

    private void PlaceBuilding()
    {
        if (placedBuildingHere) return;
        buildingType.PlaceBuilding(Mouse.instance.GetGridPosition(), direction);
        placedBuildingHere = true;
    }

    protected override void DestroyBuilding()
    {
        base.DestroyBuilding();
        placedBuildingHere = false;
    }

    private void UpdateVisual()
    {
        visual.UpdateTarget(Mouse.instance.GetGridPosition(), direction);
        visual.SetColor(GridSystem.instance.CanPlace(Mouse.instance.GetGridPosition(), buildingType.GetTransformedSize(direction)));
    }
}
