using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingMining : Building
{
    private List<Vector3Int> _assignedResources = new();

    public List<Vector3Int> AssignedResources => _assignedResources;

    public override Job GetAvailableJob(Job lastJob = null)
    {
        if(!HaveJob)
        {
            if(lastJob != null)
            {
                Vector3Int resPos = lastJob.resourceNeighbour.resourcePos;
                if(ServiceLocator.GetService<TerrainMapManager>().IsResource(resPos))
                {
                    ResourceNeighbour currentResNeighbour = lastJob.resourceNeighbour;

                    return new Job(this, buildingData.jobType, new Vector3Int(GridPosition.x, GridPosition.y, 0), currentResNeighbour);
                }
            }

            ResourceNeighbour positionData = ResourcePosition();

            if(!IsNoneResource(positionData))
            {
                return new Job(this, buildingData.jobType, new Vector3Int(GridPosition.x, GridPosition.y, 0), positionData);
            }

            _haveJob = true;
        }
        return null;
    }

    private bool IsNoneResource(ResourceNeighbour rn)
    {
        return rn.resourceType == ResourceType.None;
    }

    private ResourceNeighbour ResourcePosition()
    {
        //refactor
        if(buildingData.jobType == JobType.Mining)
        {
            ResourceLocator rl = ServiceLocator.GetService<ResourceLocator>();
            ResourceNeighbour resPos = rl.GetCellNearResource(GridPosition, buildingData.resourceType, buildingData.MiningRadius);

            return resPos;
        }

        return ResourceNeighbour.None;
    }
}
