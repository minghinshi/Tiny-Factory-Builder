using System;
using UnityEngine;

public class ItemPlacement : Placement
{
    private ItemType itemType;
    protected Transform previewTransform;
    protected SpriteRenderer spriteRenderer;

    public ItemPlacement(ItemType itemType)
    {
        this.itemType = itemType;
        previewTransform = itemType.GetNewItemTransform(GetMousePosition());
        spriteRenderer = previewTransform.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void OnUpdate()
    {
        RenderPreview();
        if (Input.GetMouseButtonDown(0)) Place();
        if (Input.GetKeyDown(KeyCode.Q)) Terminate();
    }

    private void RenderPreview()
    {
        previewTransform.position = Vector3.Lerp(previewTransform.position, GetMousePosition(), Time.deltaTime * 20f);
    }

    private void Place()
    {
        Building building = Grids.buildingGrid.GetCellObjectAt(GetMousePosition());
        building?.Insert(new ItemStack(itemType, GetItemCount()));
        if (!PlayerInventory.inventory.Contains(itemType)) Terminate();
    }

    private void Terminate()
    {
        UnityEngine.Object.Destroy(previewTransform);
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private uint GetItemCount()
    {
        uint count = 1;
        if (Input.GetKey(KeyCode.LeftShift)) count *= 10;
        if (Input.GetKey(KeyCode.LeftControl)) count *= 100;
        return Math.Min(count, PlayerInventory.inventory.GetItemCount(itemType));
    }
}