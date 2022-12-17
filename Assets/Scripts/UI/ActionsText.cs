using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionsText : MonoBehaviour
{
    public static ActionsText instance;

    public string[] basicActions;

    public string takeOutputs;
    public string takeAllItems;
    public string removeBuilding;

    public string[] placeBuilding;
    public string[] placeItem;
    public string[] craftItem;

    public string[] placingBuildingActions;
    public string[] placingItemActions;

    private TMP_Text text;

    private void Awake()
    {
        instance = this;
        text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        text.text = string.Join("\n", GetActions());
    }

    private List<string> GetActions()
    {
        List<string> actions = new();
        actions.AddRange(basicActions);
        return actions;
    }
}
