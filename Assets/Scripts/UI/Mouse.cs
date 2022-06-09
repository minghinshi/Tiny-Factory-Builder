using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour
{
    public static Mouse instance;

    private Vector2 worldPosition;
    private Vector2Int gridPosition;
    private Building targetBuilding;
    private Item targetItem;
    private bool hasTargetChanged;

    public delegate void MouseTargetChangedHandler();
    public event MouseTargetChangedHandler MouseTargetChanged;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        UpdateProperties();
        if (hasTargetChanged) MouseTargetChanged?.Invoke();
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
}
