using UnityEngine;

public class WorkingData
{
    public float Time { get; }
    public System.Action OnReached { get; }
    
    public WorkingData(float time, System.Action onReached)
    {
        Time = time;
        OnReached = onReached;
    }
}