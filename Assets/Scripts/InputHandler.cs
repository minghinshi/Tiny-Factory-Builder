using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    private GridSystem buildingGrid;
    [SerializeField]
    private BuildingType selectedBuildingType = null;
    private Vector2Int currentGridPosition = new Vector2Int();
    private Direction currentDirection = Direction.north;
    private GhostBuilding ghostBuilding;
    private bool moved = true;

    // Start is called before the first frame update
    void Start()
    {
        buildingGrid = GridManager.buildingGrid;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedBuildingType != null) {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridPosition = buildingGrid.GetGridPosition(worldPoint);
            if (!gridPosition.Equals(currentGridPosition)) {
                currentGridPosition = gridPosition;
                ghostBuilding.SetPosition(gridPosition);
                moved = true;
            }
            if (Input.GetMouseButton(0) && moved && !EventSystem.current.IsPointerOverGameObject())
            {
                buildingGrid.PlaceBuilding(gridPosition, currentDirection, selectedBuildingType);
                moved = false;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                currentDirection = DirectionHelper.RotateClockwise(currentDirection);
                ghostBuilding.SetDirection(currentDirection);
            }
            ghostBuilding.UpdateVisuals();
        }
    }

    public void ChangeBuildingType(BuildingType buildingType) {
        if (buildingType.Equals(selectedBuildingType)) return;
        if (selectedBuildingType == null)
            ghostBuilding = new GhostBuilding(currentGridPosition, currentDirection, buildingType);
        else
            ghostBuilding.ChangeBuildingType(buildingType);
        selectedBuildingType = buildingType;
    }
}
