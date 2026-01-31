using System.Collections.Generic;

[System.Serializable]
public struct JobReward
{
    public JobType jobType;
    public List<ResourceAmount> ResourceAmounts;
}