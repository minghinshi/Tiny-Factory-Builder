using UnityEngine;

public delegate void TickMachines();
public class TickHandler : MonoBehaviour
{
    public static TickHandler instance;

    public event TickMachines Tick;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        Tick?.Invoke();
    }
}
