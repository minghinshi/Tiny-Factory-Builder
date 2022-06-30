using UnityEngine;

public delegate void Tick();
public class TickHandler : MonoBehaviour
{
    public static TickHandler instance;

    private Timer conveyorTimer;

    public event Tick TickItems;
    public event Tick TickConveyors;
    public event Tick TickTimers;
    public event Tick TickMachines;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        conveyorTimer = new Timer(20, true);
        conveyorTimer.TimerEnded += OnConveyorTimerEnded;
    }

    private void FixedUpdate()
    {
        TickItems?.Invoke();
        TickTimers?.Invoke();
        TickMachines?.Invoke();
    }

    private void OnConveyorTimerEnded()
    {
        TickConveyors?.Invoke();
        conveyorTimer.Reset();
    }
}
