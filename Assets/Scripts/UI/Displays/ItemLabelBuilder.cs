using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemLabelBuilder
{
    private Transform result;

    public ItemLabelBuilder()
    {
        Reset();
    }

    public void Reset()
    {
        result = new GameObject("Item Label", typeof(RectTransform)).transform;
    }

    public void AddButton(params UnityAction[] clickActions)
    {
        Button buttonTransform = Object.Instantiate(PrefabLoader.button, result).GetComponent<Button>();
        new List<UnityAction>(clickActions).ForEach(x => buttonTransform.onClick.AddListener(x));
    }

    public void AddPointerEnterAction(params UnityAction[] enterActions)
    {
        new List<UnityAction>(enterActions).ForEach(x => AddPointerAction(EventTriggerType.PointerEnter, x));
    }

    public void AddPointerExitAction(params UnityAction[] exitActions)
    {
        new List<UnityAction>(exitActions).ForEach(x => AddPointerAction(EventTriggerType.PointerExit, x));
    }

    public void AddImage(Sprite itemSprite)
    {
        Object.Instantiate(PrefabLoader.image, result).GetComponent<Image>().sprite = itemSprite;
    }

    public void AddCounter(int itemCount)
    {
        Object.Instantiate(PrefabLoader.counter, result).GetComponent<Text>().text = itemCount.ToString("N0");
    }

    public void SetTextColor(Color color) {
        result.GetComponentInChildren<Text>().color = color;
    }

    public Transform GetResult()
    {
        Transform result = this.result;
        Reset();
        return result;
    }

    private void AddPointerAction(EventTriggerType eventTriggerType, UnityAction action)
    {
        EventTrigger trigger = result.GetComponentInChildren<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventTriggerType };
        entry.callback.AddListener(x => { action.Invoke(); });
        trigger.triggers.Add(entry);
    }
}
