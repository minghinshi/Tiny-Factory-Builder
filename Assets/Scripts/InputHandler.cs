using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;

    private bool movedGhost = false;
    private bool placedBuilding = false;

    private GridSystem<Building> buildingGrid;
    [SerializeField]
    private BuildingType selectedBuildingType = null;
    private Vector2Int gridPosition = new Vector2Int();
    private Direction currentDirection = Direction.North;
    private GhostBuilding ghostBuilding;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        buildingGrid = Grids.buildingGrid;
        ghostBuilding = new GhostBuilding(gridPosition, currentDirection);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdatePointerPosition();
        DetectDestroy();
        if (selectedBuildingType != null)
            UpdateBuildingPlacement();
    }

    private void UpdateBuildingPlacement() {
        UpdateGhostPosition();
        DetectPlacement();
        DetectRotation();
        DetectExitPlacement();
        ghostBuilding.UpdateVisuals();
    }

    private void DetectDestroy()
    {
        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            buildingGrid.TryDestroyCellObject(gridPosition);
            placedBuilding = false;
        }
    }

    private void UpdateGhostPosition()
    {
        if (!movedGhost)
        {
            ghostBuilding.SetPosition(gridPosition);
            movedGhost = true;
        }
    }

    private void DetectPlacement()
    {
        if (Input.GetMouseButton(0) && !placedBuilding && !EventSystem.current.IsPointerOverGameObject())
            PlaceBuilding();
    }

    private void PlaceBuilding() {
        selectedBuildingType.PlaceBuilding(gridPosition, currentDirection);
        placedBuilding = true;
        if (PlayerInventory.inventory.GetItemCount(selectedBuildingType) == 0)
            StopPlacing();
    }

    private void DetectRotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentDirection = currentDirection.RotateClockwise();
            ghostBuilding.SetDirection(currentDirection);
        }
    }

    private void DetectExitPlacement()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StopPlacing();
        }
    }

    //Updates the position of the ghost building.
    public void UpdatePointerPosition()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int newGridPosition = buildingGrid.GetGridPosition(worldPoint);
        if (!newGridPosition.Equals(gridPosition))
        {
            gridPosition = newGridPosition;
            movedGhost = false;
            placedBuilding = false;
        }
    }


    //Change the building type that the player is going to place.
    public void SetBuildingType(BuildingType buildingType)
    {
        if (buildingType.Equals(selectedBuildingType)) return;
        ghostBuilding.ChangeBuildingType(buildingType);
        selectedBuildingType = buildingType;
    }

    //Stop placing buildings.
    public void StopPlacing()
    {
        selectedBuildingType = null;
        ghostBuilding.SetInvisible();
    }
}
