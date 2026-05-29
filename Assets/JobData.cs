using UnityEngine;

public struct JobData
{
    public JobType jobType;
    public ResourceType resourceType;
    public ResourceNeighbour resourceNeighbour; // это структура, она копируется

    public JobData(Job job)
    {
        jobType = job.jobType;
        resourceType = job.resourceType;
        resourceNeighbour = job.resourceNeighbour;
    }
}