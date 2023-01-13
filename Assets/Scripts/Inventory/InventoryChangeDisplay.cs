using System.Collections.Generic;
using UnityEngine;

public class InventoryChangeDisplay : ItemLabelDisplay
{
    private const float TimeToLive = 1f;
    private static InventoryChangeDisplay currentDisplay;

    private float timeElapsed;
    private CanvasGroup canvasGroup;
    private readonly List<InventoryChange> changes = new();

    public static void DisplayChange(InventoryChange inventoryChange)
    {
        GetCurrentDisplay().AddChange(inventoryChange);
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        SetBuildFunc(BuildLabels);
        currentDisplay = null;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= TimeToLive) Destroy(gameObject);
        else canvasGroup.alpha = 1 - Mathf.Pow(timeElapsed / TimeToLive, 2);
    }

    private static InventoryChangeDisplay GetCurrentDisplay()
    {
        if (currentDisplay == null) Create();
        return currentDisplay;
    }

    private static void Create()
    {
        Transform displayTransform = Instantiate(Prefabs.changeDisplay, Transforms.UITransform);
        displayTransform.position = Input.mousePosition;
        currentDisplay = displayTransform.GetComponent<InventoryChangeDisplay>();
    }

    private void AddChange(InventoryChange inventoryChange)
    {
        changes.Add(inventoryChange);
    }

    private List<ItemLabel> BuildLabels()
    {
        return changes.ConvertAll(BuildLabel);
    }

    private ItemLabel BuildLabel(InventoryChange inventoryChange)
    {
        return new ItemLabel.Builder().BuildChangeDisplayLabel(inventoryChange).Build();
    }
}
