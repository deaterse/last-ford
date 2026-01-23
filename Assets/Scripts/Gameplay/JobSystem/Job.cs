using UnityEngine;

public class Job
{
    private Vector3Int _targetPosition;
    private bool _isAssigned;
    private JobType _jobType;
    private ResourceType _resourceType;

    public bool IsAssigned => _isAssigned;
    public JobType jobType => _jobType;
    public Vector3Int TargetPos => _targetPosition;

    public Job(Vector3Int targetPos, JobType jobType, ResourceType resourceType, bool value = true)
    {
        _targetPosition = targetPos;
        _jobType = jobType;
        _resourceType = resourceType;
        _isAssigned = value;

        //add logic of setting a (1) variable (using jobtype)
    }

    public void AssignJob(bool value)
    {
        _isAssigned = value;
    }
}
