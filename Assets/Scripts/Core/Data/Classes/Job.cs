using System;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class Job
{
    private Building _building;

    private Vector3Int _buildingPosition;
    private Vector3Int _storagePosition;
    private ResourceNeighbour _resourceNeighbour;

    private float _jobTime;

    private JobType _jobType;
    private ResourceType _resourceType;
    private Vector3Int _zeroVector = new Vector3Int(0,0,0);

    public JobType jobType => _jobType;
    public ResourceType resourceType => _resourceType;
    public Building Building => _building;

    public Vector3Int BuildingPos => _buildingPosition;
    public Vector3Int StoragePos => _storagePosition;
    public ResourceNeighbour resourceNeighbour => _resourceNeighbour;

    public Vector3Int ResourcePos => _resourceNeighbour.resourcePos;
    public Vector3Int JobPos => _resourceNeighbour.neighbourPos;

    public float JobTime => _jobTime;

    public Job() {}

    public void SetData(Building building, JobType jobType, ResourceType resourceType, Vector3Int buildingPosition, float jobTime, ResourceNeighbour? resourceNeighbour = null, Vector3Int storage = default(Vector3Int))
    {
        _building = building;
        _jobType = jobType;
        _resourceType = resourceType;
        _buildingPosition = buildingPosition;
        _jobTime = jobTime;

        if(jobType == JobType.Mining)
        {
            if(resourceNeighbour != null)
            {
                _resourceNeighbour = (ResourceNeighbour) resourceNeighbour;
            }
            else
            {
                Debug.LogError("You are trying to make a Job class without ResourceNeighbour");
            }
        }
        else if(jobType == JobType.Production)
        {
            _storagePosition = storage;
        }
    }

    public void Reset()
    {
        _building = null;
        _jobType = JobType.None;
        _resourceType = ResourceType.None;
        _buildingPosition = default(Vector3Int);
        _jobTime = 0;
        _resourceNeighbour = ResourceNeighbour.None;
        _storagePosition = default(Vector3Int);
    }

    public string JobString()
    {
        return $"JobType: {_jobType}, Res: {_resourceType}, JobTime: {_jobTime}, Buildingpos = {_buildingPosition}";
    }
}
