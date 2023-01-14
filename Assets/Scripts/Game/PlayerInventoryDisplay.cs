using UnityEngine;

[RequireComponent(typeof(InventoryDisplay))]
public class PlayerInventoryDisplay : MonoBehaviour
{
    public static PlayerInventoryDisplay instance;

    [SerializeField] private VisibilityHandler visibilityHandler;
    [SerializeField] private PanelSwitcher panelSwitcher;
    private InventoryDisplay inventoryDisplay;

    private void Awake()
    {
        instance = this;
    }

    public void Initialize()
    {
        inventoryDisplay = GetComponent<InventoryDisplay>();
        InitializeInventoryPanel();
    }

    public void ToggleDisplay()
    {
        panelSwitcher.TogglePanel(visibilityHandler);
    }

    private void InitializeInventoryPanel()
    {
        inventoryDisplay.SetTargetInventory(PlayerInventory.instance);
        inventoryDisplay.SetBuildFunc(BuildItemButton);
    }

    private ItemLabel BuildItemButton(ItemStack itemStack)
    {
        return InventoryButton.Create(itemStack).ItemLabel;
    }
}