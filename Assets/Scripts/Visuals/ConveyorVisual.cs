using UnityEngine;

public class ConveyorVisual : PlacedVisual
{
    private SpriteRenderer itemRenderer;

    public static ConveyorVisual Create()
    {
        return Instantiate(Prefabs.conveyorVisuals, Transforms.worldTransform).GetComponent<ConveyorVisual>();
    }

    public void Initialize(Conveyor conveyor)
    {
        base.Initialize(conveyor);
        itemRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void RenderItem(ItemType itemType)
    {
        itemRenderer.sprite = itemType ? itemType.GetSprite() : null;
    }
}