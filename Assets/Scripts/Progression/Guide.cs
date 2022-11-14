using UnityEngine;

[CreateAssetMenu(menuName = "Guide")]
public class Guide : ScriptableObject
{
    [SerializeField] private string displayedName;
    [SerializeField, TextArea] private string[] pages;

    public string Name => displayedName;
    public int TotalPages => pages.Length;

    public string GetPage(int page)
    {
        return pages[page];
    }
}
