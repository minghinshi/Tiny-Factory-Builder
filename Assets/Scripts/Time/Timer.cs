public class Timer
{
    private int length;
    private int ticksLeft;
    private bool isEnabled;

    public delegate void TimerEndedHandler();
    public event TimerEndedHandler TimerEnded;

    public Timer(int length, bool isEnabled)
    {
        this.length = length;
        ticksLeft = length;
        this.isEnabled = isEnabled;

        TickHandler.instance.TickTimers += OnTick;
    }

    public void Resume()
    {
        isEnabled = true;
    }

    public void Pause()
    {
        isEnabled = false;
    }

    public void Reset()
    {
        ticksLeft = length;
    }

    public void Destroy()
    {
        TickHandler.instance.TickTimers -= OnTick;
    }

    public bool IsEnabled() {
        return isEnabled;
    }

    private void OnTick()
    {
        ticksLeft -= isEnabled ? 1 : 0;
        if (TimerReachedZero())
            TimerEnded?.Invoke();
    }

    private bool TimerReachedZero()
    {
        return ticksLeft <= 0;
    }
}
