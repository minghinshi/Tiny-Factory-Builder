using UnityEngine;

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
        Grids.grid.DestroyBuildingAt(Mouse.instance.GetGridPosition());
    }
}
