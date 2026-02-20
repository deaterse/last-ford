using UnityEngine;

public class Job
{
    private Building _building;

    private Vector3Int _buildingPosition;
    private ResourceNeighbour _resourceNeighbour;

    private JobType _jobType;
    private ResourceType _resourceType;

    public JobType jobType => _jobType;
    public ResourceType resourceType => _resourceType;
    public Building Building => _building;

    public Vector3Int BuildingPos => _buildingPosition;
    public ResourceNeighbour resourceNeighbour => _resourceNeighbour;

    public Vector3Int ResourcePos => _resourceNeighbour.resourcePos;
    public Vector3Int JobPos => _resourceNeighbour.neighbourPos;

    public Job(Building building, JobType jobType, ResourceType resourceType, Vector3Int buildingPosition, ResourceNeighbour resourceNeighbour)
    {
        _building = building;
        _jobType = jobType;
        _resourceType = resourceType;
        _buildingPosition = buildingPosition;
        _resourceNeighbour = resourceNeighbour;
    }
}
