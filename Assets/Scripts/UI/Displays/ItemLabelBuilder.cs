using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemLabelBuilder
{
    //Prefabs
    private readonly Transform buttonPrefab = ((GameObject)Resources.Load("Prefabs/ItemLabel/Button")).transform;
    private readonly Transform imagePrefab = ((GameObject)Resources.Load("Prefabs/ItemLabel/Image")).transform;
    private readonly Transform counterPrefab = ((GameObject)Resources.Load("Prefabs/ItemLabel/Counter")).transform;

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
        Button buttonTransform = Object.Instantiate(buttonPrefab, result).GetComponent<Button>();
        new List<UnityAction>(clickActions).ForEach(x => buttonTransform.onClick.AddListener(x));
    }

    public void AddImage(Sprite itemSprite)
    {
        Object.Instantiate(imagePrefab, result).GetComponent<Image>().sprite = itemSprite;
    }

    public void AddCounter(int itemCount)
    {
        Object.Instantiate(counterPrefab, result).GetComponent<Text>().text = itemCount.ToString("N0");
    }

    public Transform GetResult()
    {
        Transform result = this.result;
        Reset();
        return result;
    }
}
