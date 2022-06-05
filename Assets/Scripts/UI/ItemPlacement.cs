using System;
using UnityEngine;

public class ItemPlacement : Placement
{
    private readonly ItemType itemType;
    private readonly Transform previewTransform;
    private readonly SpriteRenderer spriteRenderer;

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
        CheckInputs();
    }

    public override void Destroy()
    {
        UnityEngine.Object.Destroy(previewTransform.gameObject);
    }

    public override ItemType GetItemType()
    {
        return itemType;
    }

    protected override void CheckInputs()
    {
        if (Input.GetMouseButtonDown(0) && IsMousePointingAtWorld()) PlaceItem();
        base.CheckInputs();
    }

    private void RenderPreview()
    {
        previewTransform.position = Vector3.Lerp(previewTransform.position, GetMouseWorldPosition(), Time.deltaTime * 20f);
    }

    private void PlaceItem()
    {
        Grids.buildingGrid.GetCellObjectAt(GetMouseWorldPosition())?.Insert(new ItemStack(itemType, GetItemCount()));
    }

    private uint GetItemCount()
    {
        uint count = 1;
        if (Input.GetKey(KeyCode.LeftShift)) count *= 10;
        if (Input.GetKey(KeyCode.LeftControl)) count *= 100;
        return Math.Min(count, PlayerInventory.inventory.GetItemCount(itemType));
    }
}