using UnityEngine;

public struct WorkingData
{
    public float Time { get; }
    public JobType _jobType { get; }
    public Vector3Int _resourcePos {get; }
    public System.Action OnReached { get; }
    
    public WorkingData(float time, JobType jobType, Vector3Int resourcePos, System.Action onReached)
    {
        Time = time;
        _jobType = jobType;
        _resourcePos = resourcePos;
        OnReached = onReached;
    }
}