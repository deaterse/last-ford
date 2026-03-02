using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingNonWorkable : Building
{
    public override Job GetAvailableJob(Job lastJob = null)
    {
        return null;
    }
}
