using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemType : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;

    public Transform GetNewItemTransform(Vector2Int gridPosition)
    {
        Transform transform = CreateTransform();
        InitializeSpriteRenderer(transform.GetComponent<SpriteRenderer>(), 1);
        transform.position = Grids.itemGrid.GetCentreWorldPosition(gridPosition);
        return transform;
    }

    public Sprite GetSprite() {
        return itemSprite;
    }

    protected Transform CreateTransform() {
        return new GameObject(itemName, typeof(SpriteRenderer)).transform;
    }

    protected void InitializeSpriteRenderer(SpriteRenderer spriteRenderer, int sortingOrder) {
        spriteRenderer.sprite = itemSprite;
        spriteRenderer.sortingOrder = sortingOrder;
    }
}
