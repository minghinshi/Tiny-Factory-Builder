using UnityEngine;
using UnityEngine.Pool;

public class ItemLabelPool
{
    public static ObjectPool<ItemLabel> pool = new(Create, Get, Release, Destroy);

    private static ItemLabel Create()
    {
        return Object.Instantiate(Prefabs.itemLabel).GetComponent<ItemLabel>();
    }

    private static void Get(ItemLabel itemLabel)
    {
        itemLabel.gameObject.SetActive(true);
    }

    private static void Release(ItemLabel itemLabel)
    {
        itemLabel.transform.SetParent(null);
        itemLabel.gameObject.SetActive(false);
    }

    private static void Destroy(ItemLabel itemLabel)
    {
        Object.Destroy(itemLabel.gameObject);
    }
}