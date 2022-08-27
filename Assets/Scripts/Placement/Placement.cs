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
        GridSystem.instance.DestroyBuildingAt(Mouse.instance.GetGridPosition());
    }
}
