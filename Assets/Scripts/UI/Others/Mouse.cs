using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour
{
    public static Mouse instance;
    private Tooltip tooltip;

    private Vector2 worldPosition;
    private Vector2Int gridPosition;
    private Building targetBuilding;
    private Item targetItem;
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

    public Vector2 GetWorldPosition()
    {
        return worldPosition;
    }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    public Building GetTargetBuilding()
    {
        return targetBuilding;
    }

    public Item GetTargetItem()
    {
        return targetItem;
    }

    public bool IsPointingAtWorld()
    {
        return !EventSystem.current.IsPointerOverGameObject();
    }

    public bool IsPointingAtBuilding()
    {
        return IsPointingAtWorld() && targetBuilding != null;
    }

    public bool IsPointingAtItem()
    {
        return IsPointingAtWorld() && targetItem != null;
    }

    public bool IsPointingAtSomething()
    {
        return IsPointingAtBuilding() || IsPointingAtItem();
    }

    private void UpdateProperties()
    {
        hasTargetChanged = false;
        UpdateWorldPosition();
        UpdateGridPosition();
        UpdateTargetBuilding();
        UpdateTargetItem();
    }

    private void UpdateWorldPosition()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void UpdateGridPosition()
    {
        Vector2Int newGridPosition = Grids.buildingGrid.GetGridPosition(worldPosition);
        if (!newGridPosition.Equals(gridPosition))
        {
            gridPosition = newGridPosition;
            hasTargetChanged = true;
        }
    }

    private void UpdateTargetBuilding()
    {
        Building newTarget = GetTarget(Grids.buildingGrid);
        if (newTarget != targetBuilding)
        {
            targetBuilding = newTarget;
            hasTargetChanged = true;
        }
    }

    private void UpdateTargetItem()
    {
        Item newTarget = GetTarget(Grids.itemGrid);
        if (newTarget != targetItem)
        {
            targetItem = newTarget;
            hasTargetChanged = true;
        }
    }

    private T GetTarget<T>(GridSystem<T> targetGrid) where T : CellObject
    {
        return targetGrid.GetCellObjectAt(gridPosition);
    }

    private void UpdateTooltip()
    {
        if (IsPointingAtSomething()) tooltip.ShowTooltip(BuildTooltip);
        else tooltip.HideTooltip();
    }

    private void BuildTooltip()
    {
        if (IsPointingAtBuilding()) tooltip.BuildBuildingTooltip(GetTargetBuilding());
        if (IsPointingAtItem()) tooltip.BuildItemTooltip(GetTargetItem());
    }
}
