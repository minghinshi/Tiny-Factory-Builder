using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    private BuildingGridSystem buildingGrid;
    [SerializeField]
    private BuildingType selectedBuildingType = null;
    private Vector2Int gridPosition = new Vector2Int();
    private Direction currentDirection = Direction.North;
    private GhostBuilding ghostBuilding;
    private bool movedGhost = false;
    private bool placedBuilding = false;

    // Start is called before the first frame update
    void Start()
    {
        buildingGrid = GridManager.buildingGrid;
        ghostBuilding = new GhostBuilding(gridPosition, currentDirection);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGhostPosition();
        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            buildingGrid.DestroyBuilding(gridPosition);
            placedBuilding = false;
        }
        if (selectedBuildingType != null)
        {
            if (!movedGhost)
            {
                ghostBuilding.SetPosition(gridPosition);
                movedGhost = true;
            }
            if (Input.GetMouseButton(0) && !placedBuilding && !EventSystem.current.IsPointerOverGameObject())
            {
                buildingGrid.PlaceBuilding(gridPosition, currentDirection, selectedBuildingType);
                placedBuilding = true;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentDirection = currentDirection.RotateClockwise();
                ghostBuilding.SetDirection(currentDirection);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StopPlacing();
            }
            ghostBuilding.UpdateVisuals();
        }
    }

    //Updates the position of the ghost building.
    public void UpdateGhostPosition()
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
    public void ChangeBuildingType(BuildingType buildingType)
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
