using UnityEngine;

public class PlayerInventoryHandler : MonoBehaviour {
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private ItemStack[] initialItems;

    private Inventory playerInventory;
    private InventoryDisplay inventoryDisplay;
    private VisibilityHandler visibilityHandler;

    private void Start()
    {   
        InitializeInventory();
        InitializeInventoryPanel();
    }

    private void InitializeInventory() {
        playerInventory = new Inventory();
        foreach (ItemStack stack in initialItems)
            playerInventory.StoreCopyOf(stack);
    }

    private void InitializeInventoryPanel() {
        inventoryDisplay = inventoryPanel.GetComponent<InventoryDisplay>();
        inventoryDisplay.SetTargetInventory(playerInventory);
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