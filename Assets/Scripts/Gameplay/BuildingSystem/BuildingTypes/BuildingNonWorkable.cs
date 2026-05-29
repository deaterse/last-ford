using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingNonWorkable : Building
{
    public override Job GetAvailableJob(JobData? lastJob = null)
    {
        return null;
    }
}
