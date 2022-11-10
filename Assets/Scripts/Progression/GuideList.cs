using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuideList : MonoBehaviour
{
    public static GuideList instance;

    private void Awake()
    {
        instance = this;
    }

    public void UnlockGuide(Guide guide)
    {
        Transform guideButton = Instantiate(Prefabs.guideButton, transform);
        guideButton.GetComponentInChildren<TMP_Text>().text = guide.Name;
        guideButton.GetComponent<Button>().onClick.AddListener(guide.Display);
    }
}
