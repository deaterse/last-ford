using UnityEngine;

public class Job
{
    private Building _building;
    private bool _isAssigned;
    private JobType _jobType;

    public JobType jobType => _jobType;
    public Building Building => _building;

    public Job(Building building, JobType jobType)
    {
        _building = building;
        _jobType = jobType;

        //add logic of setting a (1) variable (using jobtype)
    }
}
