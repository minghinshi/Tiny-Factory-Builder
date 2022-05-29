using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemButtonMaker : MonoBehaviour
{
    public static ItemButtonMaker instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Transform itemButton;
    [SerializeField] private Transform itemButtonWithCounter;

    private Transform CreateItemButton(Transform prefab, Transform parent, ItemType itemType, UnityAction buttonAction)
    {
        Transform buttonTransform = Instantiate(prefab, parent);
        buttonTransform.GetChild(0).GetComponent<Image>().sprite = itemType.GetSprite();
        buttonTransform.GetComponent<Button>().onClick.AddListener(buttonAction);
        return buttonTransform;
    }

    public Transform CreateItemButton(Transform parent, ItemType itemType, UnityAction buttonAction)
    {
        return CreateItemButton(itemButton, parent, itemType, buttonAction);
    }

    public Transform CreateItemButtonWithCounter(Transform parent, ItemStack itemStack, UnityAction buttonAction)
    {
        Transform transform = CreateItemButton(itemButtonWithCounter, parent, itemStack.GetItemType(), buttonAction);
        transform.GetChild(1).GetComponent<Text>().text = itemStack.GetCount().ToString();
        return transform;
    }
}
