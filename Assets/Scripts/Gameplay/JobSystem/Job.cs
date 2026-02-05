using UnityEngine;

public class Job
{
    private Building _building;

    private Vector3Int _buildingPosition;
    private Vector3Int _jobPosition;
    private Vector3Int _resourcePosition;

    private JobType _jobType;

    public JobType jobType => _jobType;
    public Building Building => _building;

    public Vector3Int BuildingPos => _buildingPosition;
    public Vector3Int JobPos => _jobPosition;
    public Vector3Int ResourcePos => _resourcePosition;

    public Job(Building building, JobType jobType, Vector3Int buildingPosition, Vector3Int jobPosition, Vector3Int resourcePosition)
    {
        _building = building;
        _jobType = jobType;
        _buildingPosition = buildingPosition;
        _jobPosition = jobPosition;
        _resourcePosition = resourcePosition;
    }
}
