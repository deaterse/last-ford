using UnityEngine;

public struct WorkingData
{
    public float Time { get; }
    public JobType _jobType { get; }
    public System.Action OnReached { get; }
    
    public WorkingData(float time, JobType jobType, System.Action onReached)
    {
        Time = time;
        _jobType = jobType;
        OnReached = onReached;
    }
}