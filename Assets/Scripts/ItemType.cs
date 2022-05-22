using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemType : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;

    public Transform GetNewItemTransform(Vector2Int gridPosition)
    {
        Transform itemTransform = new GameObject(itemName, typeof(SpriteRenderer)).transform;
        itemTransform.position = AllGrids.itemGrid.GetCentreWorldPosition(gridPosition);
        InitializeSpriteRenderer(itemTransform.GetComponent<SpriteRenderer>());
        return itemTransform;
    }

    private void InitializeSpriteRenderer(SpriteRenderer spriteRenderer) {
        spriteRenderer.sortingOrder = 1;
        spriteRenderer.sprite = itemSprite;
    }
}
