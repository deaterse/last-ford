using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct JobReward
{
    [Header("Job Parameters")]
    public JobType jobType;
    public ResourceType resourceType;

    [Header("Reward Parameters")]
    public List<ResourceAmount> ResourceAmounts;
}