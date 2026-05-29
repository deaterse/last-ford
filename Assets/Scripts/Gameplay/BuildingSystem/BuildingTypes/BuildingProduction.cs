using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingProduction : Building
{
    public override Job GetAvailableJob(JobData? lastJob = null)
    {
        if(!DontHaveJob && IsResourcesEnough())
        {
            Vector3Int nearestStorage = ServiceLocator.GetService<BuildingManager>().GetNearestStorage(new Vector3Int(GridPosition.x, GridPosition.y, 0));
            if(nearestStorage.x != -1)
            {
                Job job = ServiceLocator.GetService<JobPool>().Get();
                job.SetData(this, JobType.Production, buildingData.resourceType, new Vector3Int(GridPosition.x, GridPosition.y, 0), buildingData.WorkingTime, storage: nearestStorage);
                
                return job;
            }
        }
        return null;
    }

    private bool IsResourcesEnough()
    {
        return ServiceLocator.GetService<JobManager>().CheckResources(buildingData);
    }
}
