using UnityEngine;

public class ItemPlacement : Placement
{
    private readonly ItemType itemType;
    private readonly Transform previewTransform;
    private readonly SpriteRenderer spriteRenderer;

    public ItemPlacement(ItemType itemType)
    {
        this.itemType = itemType;
        previewTransform = itemType.GetNewItemTransform(Mouse.instance.GetWorldPosition());
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
        Object.Destroy(previewTransform.gameObject);
    }

    public override ItemType GetItemType()
    {
        return itemType;
    }

    protected override void CheckInputs()
    {
        if (Input.GetMouseButtonDown(0) && Mouse.instance.IsPointingAtWorld()) PlaceItem();
        base.CheckInputs();
    }

    private void RenderPreview()
    {
        previewTransform.position = Vector3.Lerp(previewTransform.position, Mouse.instance.GetWorldPosition(), Time.deltaTime * 20f);
    }

    private void PlaceItem()
    {
        Building target = Mouse.instance.GetTargetBuilding();
        if (target != null && target.CanInsertByPlayer())
        {
            ItemStack itemToPlace = new ItemStack(itemType, GetItemCount());
            target.Insert(itemToPlace);
            Inventory.playerInventory.RemoveCopyOf(itemToPlace);
        }
    }

    private int GetItemCount()
    {
        int count = 1;
        if (Input.GetKey(KeyCode.LeftShift)) count *= 10;
        if (Input.GetKey(KeyCode.LeftControl)) count *= 100;
        return Mathf.Min(count, Inventory.playerInventory.GetItemCount(itemType));
    }
}