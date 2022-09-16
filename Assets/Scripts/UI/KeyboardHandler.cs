using UnityEngine;

public class KeyboardHandler : MonoBehaviour
{
    public static KeyboardHandler instance;

    public delegate void KeyPressHandler();
    public event KeyPressHandler ShiftPressed;
    public event KeyPressHandler ShiftReleased;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) ShiftPressed.Invoke();
        if (Input.GetKeyUp(KeyCode.LeftShift)) ShiftReleased.Invoke();
    }
}
