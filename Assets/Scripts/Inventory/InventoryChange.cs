public struct InventoryChange
{
    private readonly ItemType itemType;
    private readonly int delta;

    public ItemType ItemType => itemType;
    public int Delta => delta;

    public InventoryChange(ItemType itemType, int delta)
    {
        this.delta = delta;
        this.itemType = itemType;
    }

    public bool IsStoring()
    {
        return delta > 0;
    }

    public bool IsRemoving()
    {
        return delta < 0;
    }

    public override string ToString()
    {
        return string.Format("{0:+#;-#;0} ({1})", delta, PlayerInventory.instance.GetItemCount(ItemType));
    }
}
