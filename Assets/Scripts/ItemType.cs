using UnityEngine;

public enum ItemTag { }

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemType : ScriptableObject
{
    [SerializeField] private Transform itemPrefab;
    [SerializeField] private ItemTag[] itemTags;

    public Transform GetItemTransform(Vector2Int gridPosition)
    {
        return Instantiate(itemPrefab, GridManager.itemGrid.GetCentreWorldPosition(gridPosition), Quaternion.identity);
    }
}
