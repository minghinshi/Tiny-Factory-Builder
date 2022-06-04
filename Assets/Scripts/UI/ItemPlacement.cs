using System;
using UnityEngine;

public class ItemPlacement : Placement
{
    private readonly ItemType itemType;

    public ItemPlacement(ItemType itemType)
    {
        this.itemType = itemType;
        previewTransform = itemType.GetNewItemTransform(GetMouseWorldPosition());
        spriteRenderer = previewTransform.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public override void Update()
    {
        RenderPreview();
        if (Input.GetMouseButtonDown(0) && IsMousePointingAtWorld()) Place();
    }

    public override ItemType GetItemType()
    {
        return itemType;
    }

    protected override void RenderPreview()
    {
        previewTransform.position = Vector3.Lerp(previewTransform.position, GetMouseWorldPosition(), Time.deltaTime * 20f);
    }

    protected override void Place()
    {
        Building building = Grids.buildingGrid.GetCellObjectAt(GetMouseWorldPosition());
        building?.Insert(new ItemStack(itemType, GetItemCount()));
    }

    private uint GetItemCount()
    {
        uint count = 1;
        if (Input.GetKey(KeyCode.LeftShift)) count *= 10;
        if (Input.GetKey(KeyCode.LeftControl)) count *= 100;
        return Math.Min(count, PlayerInventory.inventory.GetItemCount(itemType));
    }
}