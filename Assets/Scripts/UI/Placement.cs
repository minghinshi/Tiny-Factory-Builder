using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Placement
{
    public abstract void Update();
    public abstract ItemType GetItemType();
    public virtual void Destroy() { }

    protected virtual void CheckInputs()
    {
        if (Input.GetMouseButton(1)) DestroyBuilding();
    }

    protected virtual void DestroyBuilding()
    {
        Grids.buildingGrid.TryDestroyCellObject(GetMouseGridPosition());
    }

    protected Vector2 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected Vector2Int GetMouseGridPosition()
    {
        return Grids.buildingGrid.GetGridPosition(GetMouseWorldPosition());
    }

    protected bool IsMousePointingAtWorld()
    {
        return !EventSystem.current.IsPointerOverGameObject();
    }
}
