using UnityEngine;
using UnityEngine.Pool;

public class ItemLabelPool
{
    public static ObjectPool<ItemLabel> pool = new(Create, Get, Release, Destroy);

    public static ItemLabel Create()
    {
        return Object.Instantiate(Prefabs.itemLabel).GetComponent<ItemLabel>();
    }

    public static void Get(ItemLabel itemLabel)
    {
        itemLabel.gameObject.SetActive(true);
    }

    public static void Release(ItemLabel itemLabel)
    {
        itemLabel.gameObject.SetActive(false);
    }

    public static void Destroy(ItemLabel itemLabel)
    {
        Object.Destroy(itemLabel.gameObject);
    }
}