using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingProduction : Building
{
    public override Job GetAvailableJob(Job lastJob = null)
    {
        if(!HaveJob)
        {
            return new Job(this, JobType.Production, buildingData.resourceType, new Vector3Int(GridPosition.x, GridPosition.y, 0));
        }
        return null;
    }
}
