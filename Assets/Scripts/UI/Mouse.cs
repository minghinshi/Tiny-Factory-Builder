using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour
{
    public static Mouse instance;
    private Tooltip tooltip;

    private Vector2 worldPosition;
    private Vector2Int gridPosition;
    private Building targetBuilding;

    public ItemLabel TargetItemLabel { get; set; }

    public delegate void ValueChangedHandler<T>(T newValue);
    public event ValueChangedHandler<Building> BuildingChanged;
    public event ValueChangedHandler<Vector2Int> GridPositionChanged;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tooltip = Tooltip.instance;
        BuildingChanged += UpdateTooltip;
    }

    private void Update()
    {
        UpdateProperties();
    }

    public Vector2 GetWorldPosition() => worldPosition;
    public Vector2Int GetGridPosition() => gridPosition;
    public Building GetTargetBuilding() => targetBuilding;
    public bool IsPointingAtWorld() => !EventSystem.current.IsPointerOverGameObject();
    public bool IsPointingAtBuilding() => IsPointingAtWorld() && targetBuilding != null;

    private void UpdateProperties()
    {
        UpdateWorldPosition();
        UpdateGridPosition();
        UpdateTargetBuilding();
    }

    private void UpdateWorldPosition()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void UpdateGridPosition()
    {
        Vector2Int newGridPosition = GridSystem.instance.GetGridPosition(worldPosition);
        if (!newGridPosition.Equals(gridPosition))
        {
            gridPosition = newGridPosition;
            GridPositionChanged?.Invoke(newGridPosition);
        }
    }

    private void UpdateTargetBuilding()
    {
        Building newTarget = IsPointingAtWorld() ? GridSystem.instance.GetBuildingAt(gridPosition) : null;
        if (newTarget != targetBuilding)
        {
            targetBuilding = newTarget;
            BuildingChanged?.Invoke(newTarget);
        }
    }

    private void UpdateTooltip(Building building)
    {
        if (IsPointingAtBuilding()) Tooltip.instance.Show(() => TooltipBuilder.instance.BuildBuildingTooltip(building));
        else tooltip.Hide();
    }
}
