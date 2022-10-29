using UnityEngine;

[CreateAssetMenu(menuName = "Guide")]
public class Guide : ScriptableObject
{
    [SerializeField] private string displayedName;
    [SerializeField, TextArea] private string[] pages;
}
