using UnityEngine;

public abstract class Placement
{
    /*protected Transform previewTransform;
    protected SpriteRenderer spriteRenderer;

    public delegate void PlacementEndedHandler();
    public event PlacementEndedHandler PlacementEnded;

    public abstract void OnUpdate();
    public abstract void RenderPreview(Vector3 worldPosition);
    public abstract void Place();
    protected abstract ItemType GetItemType();

    public void Exit()
    {
        Object.Destroy(previewTransform);
        PlacementEnded?.Invoke();
    }

    protected void CheckForItemsLeft() {
        if (!PlayerInventory.inventory.Contains(GetItemType())) Exit();
    }*/
}
