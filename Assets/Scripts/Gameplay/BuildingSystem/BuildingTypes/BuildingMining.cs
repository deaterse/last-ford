using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingMining : Building
{

    public override Job GetAvailableJob(Job lastJob = null)
    {
        if(!_dontHaveJob)
        {
            if(lastJob != null && lastJob.jobType == _buildingData.jobType && lastJob.resourceType == _buildingData.resourceType)
            {
                Vector3Int resPos = lastJob.resourceNeighbour.resourcePos;
                if(ServiceLocator.GetService<TerrainMapManager>().IsResource(resPos))
                {
                    ResourceNeighbour currentResNeighbour = lastJob.resourceNeighbour;
                    
                    return new Job(this, buildingData.jobType, buildingData.resourceType, new Vector3Int(GridPosition.x, GridPosition.y, 0), buildingData.WorkingTime, currentResNeighbour);
                }
            }

            ResourceNeighbour positionData = ResourcePosition();

            if(!IsNoneResource(positionData))
            {
                return new Job(this, buildingData.jobType, buildingData.resourceType, new Vector3Int(GridPosition.x, GridPosition.y, 0), buildingData.WorkingTime, positionData);
            }

            _dontHaveJob = true;
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
