using UnityEngine;

public class ConveyorVisual : PlacedBuildingVisual
{
    private SpriteRenderer itemRenderer;

    public static ConveyorVisual Create(Building building)
    {
        ConveyorVisual visual = CreateGameObject().AddComponent<ConveyorVisual>();
        visual.Initialize(building);
        return visual;
    }

    public void RenderItem(ItemType itemType)
    {
        itemRenderer.sprite = itemType ? itemType.GetSprite() : null;
    }

    private new void Initialize(Building building)
    {
        base.Initialize(building);
        CreateItemRenderer();
    }

    private void CreateItemRenderer()
    {
        Transform transform = new GameObject().transform;
        transform.SetParent(this.transform);
        transform.position = this.transform.position;
        itemRenderer = transform.gameObject.AddComponent<SpriteRenderer>();
        itemRenderer.sortingOrder = 1;
    }
}