using UnityEngine;

public class GhostBuilding
{
    private float displayedRotation;
    private float actualRotation;

    private Vector3 displayedPosition;
    private Vector3 actualPosition;

    private Vector2Int gridPosition;
    private Direction direction;

    private BuildingType buildingType;

    private Transform buildingTransform;
    private SpriteRenderer spriteRenderer;

    private readonly Color canPlaceColor = new Color(46 / 255f, 204 / 255f, 113 / 255f, .5f);
    private readonly Color cannotPlaceColor = new Color(231 / 255f, 76 / 255f, 60 / 255f, .5f);
    private readonly Color invisibleColor = new Color(0f, 0f, 0f, 0f);

    public GhostBuilding(Vector2Int position, Direction direction)
    {
        gridPosition = position;
        displayedPosition = new Vector3(position.x, position.y);
        this.direction = direction;
        actualRotation = direction.GetRotationInDegrees();
        displayedRotation = actualRotation;
    }

    public void UpdateVisuals()
    {
        displayedPosition.x = Mathf.Lerp(displayedPosition.x, actualPosition.x, Time.deltaTime * 20f);
        displayedPosition.y = Mathf.Lerp(displayedPosition.y, actualPosition.y, Time.deltaTime * 20f);
        displayedRotation = Mathf.LerpAngle(displayedRotation, actualRotation, Time.deltaTime * 20f);
        buildingTransform.SetPositionAndRotation(displayedPosition, Quaternion.Euler(0, 0, displayedRotation));
    }

    public void SetPosition(Vector2Int position)
    {
        gridPosition = position;
        actualPosition = buildingType.GetWorldPosition(position, direction);
        SetColor();
    }

    public void SetDirection(Direction direction)
    {
        this.direction = direction;
        actualRotation = direction.GetRotationInDegrees();
        SetColor();
    }

    public void SetColor()
    {
        bool canPlace = Grids.buildingGrid.CanPlace(gridPosition, direction.TransformSize(buildingType.GetSize()));
        if (canPlace) SetCanPlaceColor();
        else SetCannotPlaceColor();
    }

    public void SetCanPlaceColor()
    {
        spriteRenderer.color = canPlaceColor;
    }

    public void SetCannotPlaceColor()
    {
        spriteRenderer.color = cannotPlaceColor;
    }

    public void SetInvisible()
    {
        spriteRenderer.color = invisibleColor;
    }

    public void ChangeBuildingType(BuildingType buildingType)
    {
        DestroyTransform();
        this.buildingType = buildingType;
        buildingTransform = buildingType.GetNewBuildingTransform(gridPosition, direction);
        spriteRenderer = buildingTransform.GetComponent<SpriteRenderer>();
        SetColor();
    }

    private void DestroyTransform()
    {
        if (buildingTransform != null)
            Object.Destroy(buildingTransform.gameObject);
    }
}
