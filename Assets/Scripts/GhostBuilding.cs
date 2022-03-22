using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuilding
{
    private Vector3 displayedPosition;
    private float displayedRotation;
    private Vector2Int actualPosition;
    private Vector3 targetPosition;
    private Direction direction;
    private float actualRotation;
    private BuildingType buildingType;

    private Transform buildingTransform;
    private SpriteRenderer spriteRenderer;

    private Color canPlaceColor = new Color(46 / 255f, 204 / 255f, 113 / 255f, .5f);
    private Color cannotPlaceColor = new Color(231 / 255f, 76 / 255f, 60 / 255f, .5f);

    public GhostBuilding(Vector2Int position, Direction direction, BuildingType buildingType) {
        actualPosition = position;
        displayedPosition = new Vector3(position.x, position.y);
        this.direction = direction;
        actualRotation = DirectionHelper.GetRotationInDegrees(direction);
        displayedRotation = actualRotation;
        this.buildingType = buildingType;

        buildingTransform = buildingType.CreateBuildingObject(position, direction);
        spriteRenderer = buildingTransform.GetComponent<SpriteRenderer>();
        SetCanPlaceColor();
    }

    public void UpdateVisuals() {
        displayedPosition.x = Mathf.Lerp(displayedPosition.x, targetPosition.x, Time.deltaTime * 20f);
        displayedPosition.y = Mathf.Lerp(displayedPosition.y, targetPosition.y, Time.deltaTime * 20f);
        displayedRotation = Mathf.LerpAngle(displayedRotation, actualRotation, Time.deltaTime * 20f);

        buildingTransform.SetPositionAndRotation(displayedPosition, Quaternion.Euler(0, 0, displayedRotation));
    }

    public void SetPosition(Vector2Int position) {
        actualPosition = position;
        targetPosition = buildingType.GetWorldPosition(position, direction);
        SetColor();
    }

    public void SetDirection(Direction direction) {
        this.direction = direction;
        actualRotation = DirectionHelper.GetRotationInDegrees(direction);
        SetColor();
    }

    public void SetCanPlaceColor() {
        spriteRenderer.color = canPlaceColor;
    }

    public void SetCannotPlaceColor() {
        spriteRenderer.color = cannotPlaceColor;
    }

    public void SetColor() {
        bool canPlace = GridManager.buildingGrid.CanPlace(actualPosition, direction, buildingType);
        if (canPlace) SetCanPlaceColor();
        else SetCannotPlaceColor();
    }

    public void ChangeBuildingType(BuildingType buildingType) {
        Object.Destroy(buildingTransform.gameObject);

        this.buildingType = buildingType;
        buildingTransform = buildingType.CreateBuildingObject(actualPosition, direction);
        spriteRenderer = buildingTransform.GetComponent<SpriteRenderer>();
        SetColor();
    }
}
