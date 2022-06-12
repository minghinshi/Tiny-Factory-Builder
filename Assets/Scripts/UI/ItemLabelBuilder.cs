using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemLabelBuilder : MonoBehaviour
{
    [SerializeField]
    private Transform buttonPrefab, imagePrefab, counterPrefab;
    private Transform result;

    private void Start()
    {
        ResetLabel();
    }

    public void ResetLabel()
    {
        result = new GameObject("Item Label", typeof(RectTransform)).transform;
    }

    public void AddButton(params UnityAction[] clickActions)
    {
        Button buttonTransform = Instantiate(buttonPrefab, result).GetComponent<Button>();
        new List<UnityAction>(clickActions).ForEach(x => buttonTransform.onClick.AddListener(x));
    }

    public void AddImage(Sprite itemSprite)
    {
        Instantiate(imagePrefab, result).GetComponent<Image>().sprite = itemSprite;
    }

    public void AddCounter(uint itemCount)
    {
        Instantiate(counterPrefab, result).GetComponent<Text>().text = itemCount.ToString("N0");
    }

    public Transform GetResult()
    {
        Transform result = this.result;
        ResetLabel();
        return result;
    }
}
