using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "JobRewardConfig", menuName = "Scriptable Objects/Job System/JobReward Config")]
public class JobsRewardsConfig : ScriptableObject
{
    [SerializeField] private List<JobReward> _jobRewards;

    public List<ResourceAmount> GetRewards(JobType jobType, ResourceType resourceType)
    {
        foreach(JobReward jr in _jobRewards)
        {
            if(jr.jobType == jobType && jr.resourceType == resourceType)
            {
                return jr.ResourceAmounts;
            }
        }
    
        return null;
    }
}