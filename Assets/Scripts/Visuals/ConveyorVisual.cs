using UnityEngine;

public class ConveyorVisual : PlacedVisual
{
    private SpriteRenderer itemRenderer;

    public static ConveyorVisual Create()
    {
        return Instantiate(PrefabLoader.conveyorVisuals, TransformFinder.worldTransform).GetComponent<ConveyorVisual>();
    }

    public void Initialize(Conveyor conveyor)
    {
        base.Initialize(conveyor);
        itemRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void RenderItem(ItemType itemType)
    {
        itemRenderer.sprite = itemType ? itemType.GetSprite() : null;
    }
}