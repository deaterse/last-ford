using UnityEngine;

public class Job
{
    private Vector3 _targetPosition;
    private bool _isAssigned;
    private JobType _jobType;
    //add variable which is contains a resource which player will get (1)

    public bool IsAssigned => _isAssigned;
    public JobType jobType => _jobType;
    public Vector3 TargetPos => _targetPosition;

    public Job(Vector3 targetPos, JobType jobType, bool value = true)
    {
        _targetPosition = targetPos;
        _jobType = jobType;
        _isAssigned = value;

        //add logic of setting a (1) variable (using jobtype)
    }

    public void AssignJob(bool value)
    {
        _isAssigned = value;
    }
}
