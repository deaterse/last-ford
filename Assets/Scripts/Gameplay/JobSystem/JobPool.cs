using System.Collections.Generic;
using UnityEngine;

public class JobPool: IService
{
    private static Queue<Job> _available = new();

    public JobPool()
    {
        _available.Clear();
    }
    
    public Job Get()
    {
        return _available.Count > 0 ? _available.Dequeue() : new Job();
    }
    
    public static void Return(Job job)
    {
        job.Reset();
        _available.Enqueue(job);
    }
}
