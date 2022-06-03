using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;

    private bool movedGhost = false;
    private bool placedBuilding = false;

    private BuildingType selectedBuildingType = null;
    private Vector2Int pointerGridPosition = new Vector2Int();
    private Direction currentDirection = Direction.North;

    private GridSystem<Building> buildingGrid;
    private GhostBuilding ghostBuilding;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        buildingGrid = Grids.buildingGrid;
        ghostBuilding = new GhostBuilding(pointerGridPosition, currentDirection);
    }

    private void Update()
    {
        UpdatePointerPosition();
        DetectDestroy();
        if (selectedBuildingType != null) UpdateBuildingPlacement();
        else DetectClick();
    }

    public void SetBuildingType(BuildingType buildingType)
    {
        if (buildingType.Equals(selectedBuildingType)) return;
        ghostBuilding.ChangeBuildingType(buildingType);
        selectedBuildingType = buildingType;
    }

    private void UpdateBuildingPlacement()
    {
        UpdateGhostPosition();
        DetectPlacement();
        DetectRotation();
        DetectExitPlacement();
        ghostBuilding.UpdateVisuals();
    }

    private void DetectDestroy()
    {
        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject()) DestroyBuilding();
    }

    private void DestroyBuilding()
    {
        buildingGrid.TryDestroyCellObject(pointerGridPosition);
        placedBuilding = false;
    }

    private void UpdateGhostPosition()
    {
        if (!movedGhost)
        {
            ghostBuilding.SetPosition(pointerGridPosition);
            movedGhost = true;
        }
    }

    private void DetectPlacement()
    {
        if (Input.GetMouseButton(0) && !placedBuilding && !EventSystem.current.IsPointerOverGameObject()) PlaceBuilding();
    }

    private void PlaceBuilding()
    {
        selectedBuildingType.PlaceBuilding(pointerGridPosition, currentDirection);
        placedBuilding = true;
        if (PlayerInventory.inventory.GetItemCount(selectedBuildingType) == 0)
            StopPlacing();
    }

    private void DetectRotation()
    {
        if (Input.GetKeyDown(KeyCode.R)) RotateBuilding();
    }

    private void RotateBuilding()
    {
        currentDirection = currentDirection.RotateClockwise();
        ghostBuilding.SetDirection(currentDirection);
    }

    private void DetectExitPlacement()
    {
        if (Input.GetKeyDown(KeyCode.Q)) StopPlacing();
    }

    private void UpdatePointerPosition()
    {
        if (!GetPointerGridPosition().Equals(pointerGridPosition)) SetPointerGridPosition();
    }

    private Vector2Int GetPointerGridPosition()
    {
        return buildingGrid.GetGridPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void SetPointerGridPosition()
    {
        pointerGridPosition = GetPointerGridPosition();
        movedGhost = false;
        placedBuilding = false;
    }

    private void StopPlacing()
    {
        selectedBuildingType = null;
        ghostBuilding.SetInvisible();
    }

    private void DetectClick()
    {
        if (Input.GetMouseButtonDown(0)) ClickOnBuilding();
    }

    private void ClickOnBuilding()
    {
        Building building = buildingGrid.GetCellObjectAt(pointerGridPosition);
        if (building != null) building.OnClick();
    }
}
