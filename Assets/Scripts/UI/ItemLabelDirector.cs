using UnityEngine;
using UnityEngine.Events;

public class ItemLabelDirector
{
    private static readonly ItemLabelBuilder builder = Object.FindObjectOfType<ItemLabelBuilder>();

    public static Transform CreateItemButton(ItemType itemType, params UnityAction[] clickActions)
    {
        builder.AddButton(clickActions);
        builder.AddImage(itemType.GetSprite());
        return builder.GetResult();
    }

    public static Transform CreateItemButton(ItemStack itemStack, params UnityAction[] clickActions)
    {
        builder.AddButton(clickActions);
        builder.AddImage(itemStack.GetItemType().GetSprite());
        builder.AddCounter(itemStack.GetCount());
        return builder.GetResult();
    }

    public static Transform CreateItemLabel(ItemStack itemStack)
    {
        builder.AddImage(itemStack.GetItemType().GetSprite());
        builder.AddCounter(itemStack.GetCount());
        return builder.GetResult();
    }
}
