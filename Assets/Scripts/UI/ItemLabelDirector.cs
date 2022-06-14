using UnityEngine;
using UnityEngine.Events;

public class ItemLabelDirector
{
    private static readonly ItemLabelBuilder builder = new ItemLabelBuilder();

    public static Transform BuildItemButton(ItemType itemType, params UnityAction[] clickActions)
    {
        builder.AddButton(clickActions);
        builder.AddImage(itemType.GetSprite());
        return builder.GetResult();
    }

    public static Transform BuildItemButton(ItemStack itemStack, params UnityAction[] clickActions)
    {
        builder.AddButton(clickActions);
        builder.AddImage(itemStack.GetItemType().GetSprite());
        builder.AddCounter(itemStack.GetCount());
        return builder.GetResult();
    }

    public static Transform BuildItemLabel(ItemStack itemStack)
    {
        builder.AddImage(itemStack.GetItemType().GetSprite());
        builder.AddCounter(itemStack.GetCount());
        return builder.GetResult();
    }
}
