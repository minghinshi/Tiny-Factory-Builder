using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour
{
    public static Mouse instance;
    private Tooltip tooltip;

    private Vector2 worldPosition;
    private Vector2Int gridPosition;
    private Building targetBuilding;
    private bool hasTargetChanged;

    public delegate void TargetChangedHandler();
    public event TargetChangedHandler TargetChanged;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tooltip = Tooltip.instance;
        TargetChanged += UpdateTooltip;
    }

    private void Update()
    {
        UpdateProperties();
        if (hasTargetChanged && IsPointingAtWorld()) TargetChanged?.Invoke();
    }

    public Vector2 GetWorldPosition() => worldPosition;
    public Vector2Int GetGridPosition() => gridPosition;
    public Building GetTargetBuilding() => targetBuilding;
    public bool IsPointingAtWorld() => !EventSystem.current.IsPointerOverGameObject();
    public bool IsPointingAtBuilding() => IsPointingAtWorld() && targetBuilding != null;

    private void UpdateProperties()
    {
        hasTargetChanged = false;
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
        Vector2Int newGridPosition = Grids.grid.GetGridPosition(worldPosition);
        if (!newGridPosition.Equals(gridPosition))
        {
            gridPosition = newGridPosition;
            hasTargetChanged = true;
        }
    }

    private void UpdateTargetBuilding()
    {
        Building newTarget = Grids.grid.GetBuildingAt(gridPosition);
        if (newTarget != targetBuilding)
        {
            targetBuilding = newTarget;
            hasTargetChanged = true;
        }
    }

    private void UpdateTooltip()
    {
        if (IsPointingAtBuilding()) tooltip.ShowTooltip(new BuildingTooltipStrategy(targetBuilding));
        else tooltip.HideTooltip();
    }
}
