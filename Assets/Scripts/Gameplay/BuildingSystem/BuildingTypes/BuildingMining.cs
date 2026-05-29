using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingMining : Building
{
    public override Job GetAvailableJob(JobData? lastJob = null)
    {
        if(!_dontHaveJob)
        {
            if(lastJob != null && lastJob.Value.jobType == _buildingData.jobType && lastJob.Value.resourceType == _buildingData.resourceType)
            {
                Vector3Int resPos = lastJob.Value.resourceNeighbour.resourcePos;
                if(ServiceLocator.GetService<TerrainMapManager>().IsResource(resPos))
                {
                    ResourceNeighbour currentResNeighbour = lastJob.Value.resourceNeighbour;

                    Job job = ServiceLocator.GetService<JobPool>().Get();
                    job.SetData(this, buildingData.jobType, buildingData.resourceType, new Vector3Int(GridPosition.x, GridPosition.y, 0), buildingData.WorkingTime, currentResNeighbour);
                    
                    return job;
                }
            }

            ResourceNeighbour positionData = ResourcePosition();

            if(!IsNoneResource(positionData))
            {
                Job job = ServiceLocator.GetService<JobPool>().Get();
                job.SetData(this, buildingData.jobType, buildingData.resourceType, new Vector3Int(GridPosition.x, GridPosition.y, 0), buildingData.WorkingTime, positionData);

                return job;
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
