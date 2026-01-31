using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "JobRewardConfig", menuName = "Scriptable Objects/Job System/JobReward Config")]
public class JobsRewardsConfig : ScriptableObject
{
    [SerializeField] private List<JobReward> _jobRewards;

    public List<ResourceAmount> GetRewards(JobType type)
    {
        foreach(JobReward jr in _jobRewards)
        {
            if(jr.jobType == type)
            {
                return jr.ResourceAmounts;
            }
        }
    
        return null;
    }
}