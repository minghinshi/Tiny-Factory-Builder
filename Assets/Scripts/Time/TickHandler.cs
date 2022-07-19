using UnityEngine;

public delegate void TickEventHandler();
public class TickHandler : MonoBehaviour
{
    public static TickHandler instance;

    private Timer conveyorTimer;

    public event TickEventHandler PretickConveyors;
    public event TickEventHandler TickConveyors;
    public event TickEventHandler TickTimers;
    public event TickEventHandler TickMachines;

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
        TickTimers?.Invoke();
        TickMachines?.Invoke();
    }

    private void OnConveyorTimerEnded()
    {
        PretickConveyors?.Invoke();
        TickConveyors?.Invoke();
        conveyorTimer.Reset();
    }
}
