using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    public static readonly Inventory inventory = new Inventory();

    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private ItemStack[] initialItems;

    private InventoryDisplay inventoryDisplay;
    private VisibilityHandler visibilityHandler;

    private void Start()
    {   
        InitializeInventory();
        InitializeInventoryPanel();
    }

    private void InitializeInventory() {
        foreach (ItemStack stack in initialItems)
            inventory.StoreCopyOf(stack);
    }

    private void InitializeInventoryPanel() {
        inventoryDisplay = inventoryPanel.GetComponent<InventoryDisplay>();
        inventoryDisplay.SetTargetInventory(inventory);
        inventoryDisplay.ButtonPressed += OnItemButtonPressed;
        visibilityHandler = inventoryPanel.GetComponent<VisibilityHandler>();
    }

    private void OnItemButtonPressed(ItemType itemType)
    {
        if (itemType is BuildingType buildingType)
        {
            InputHandler.instance.SetBuildingType(buildingType);
            visibilityHandler.FadeOut();
        }
    }
}