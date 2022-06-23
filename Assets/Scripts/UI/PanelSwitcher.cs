using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] private VisibilityHandler current;

    public void TogglePanel(VisibilityHandler panel)
    {
        bool isShowing = current != null;
        bool isPanelDifferent = current != panel;
        if (isShowing) HidePanel(current);              //ShowPanel called when panel is not the same as current panel
        if (isPanelDifferent) ShowPanel(panel);         //HidePanel called when current panel is not null
    }

    public void SwitchPanel(VisibilityHandler panel)
    {
        if (current == panel) return;
        HidePanel(current);
        ShowPanel(panel);
    }

    private void ShowPanel(VisibilityHandler panel)
    {
        panel.SetVisibleImmediately();
        current = panel;
    }

    private void HidePanel(VisibilityHandler panel)
    {
        panel.SetInvisibleImmediately();
        current = null;
    }
}
