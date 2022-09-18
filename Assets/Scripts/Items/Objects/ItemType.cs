using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemType : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField, TextArea] private string itemDescription;
    [SerializeField] private Sprite itemSprite;

    public Transform GetNewItemTransform(Vector3 worldPosition)
    {
        Transform transform = CreateTransform();
        transform.position = worldPosition;
        InitializeSpriteRenderer(transform.GetComponent<SpriteRenderer>(), 1);
        return transform;
    }

    public Sprite GetSprite()
    {
        return itemSprite;
    }

    public string GetName()
    {
        return itemName;
    }

    public string GetDescription()
    {
        return itemDescription;
    }

    protected Transform CreateTransform()
    {
        return new GameObject(itemName, typeof(SpriteRenderer), typeof(RectTransform)).transform;
    }

    protected void InitializeSpriteRenderer(SpriteRenderer spriteRenderer, int sortingOrder)
    {
        spriteRenderer.sprite = itemSprite;
        spriteRenderer.sortingOrder = sortingOrder;
    }
}
