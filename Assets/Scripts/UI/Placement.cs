using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Placement
{
    protected Transform previewTransform;
    protected SpriteRenderer spriteRenderer;

    public abstract void Update();
    public abstract ItemType GetItemType();

    public virtual void Destroy()
    {
        Debug.Log("Ending");
        Object.Destroy(previewTransform.gameObject);
    }

    protected abstract void RenderPreview();
    protected abstract void Place();

    protected Vector3 GetMouseWorldPosition()
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
