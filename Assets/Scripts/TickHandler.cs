using UnityEngine;

public delegate void Tick();
public class TickHandler : MonoBehaviour
{
    public static TickHandler instance;

    public event Tick TickItems;
    public event Tick TickMachines;
    public event Tick TickConveyors;

    private int stepsLeftUntilConveyorTick = 5;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        TickItems?.Invoke();
        StepConveyors();
        TickMachines?.Invoke();
    }

    private void StepConveyors()
    {
        stepsLeftUntilConveyorTick--;
        if (stepsLeftUntilConveyorTick == 0)
        {
            stepsLeftUntilConveyorTick = 5;
            TickConveyors?.Invoke();
        }
    }
}
