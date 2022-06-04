using UnityEngine;

public class NewInputHandler : MonoBehaviour
{
    public static NewInputHandler instance;
    private Placement placement;

    public void SetPlacement(Placement newPlacement)
    {
        placement?.Destroy();
        placement = newPlacement;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayerInventory.inventory.OutOfStock += OnInventoryItemOutOfStock;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) DestroyPlacement();
        placement?.Update();
    }

    private void OnInventoryItemOutOfStock(ItemType itemType)
    {
        if (placement != null && placement.GetItemType().Equals(itemType)) DestroyPlacement();
    }

    private void DestroyPlacement() {
        placement.Destroy();
        placement = null;
    }
}