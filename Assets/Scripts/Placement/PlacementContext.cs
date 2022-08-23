using UnityEngine;

public class PlacementContext : MonoBehaviour
{
    public static PlacementContext instance;
    private Placement placement = new NullPlacement();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SaveManager.PlayerInventory.OutOfStock += OnInventoryItemOutOfStock;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) EndPlacement();
        else placement.Update();
    }

    public void SetPlacement(Placement newPlacement)
    {
        placement.Destroy();
        placement = newPlacement;
    }

    private void OnInventoryItemOutOfStock(ItemType itemType)
    {
        if (itemType.Equals(placement.GetItemType())) EndPlacement();
    }

    private void EndPlacement()
    {
        SetPlacement(new NullPlacement());
    }
}