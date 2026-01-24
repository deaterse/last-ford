using UnityEngine;

public class OnJobFailed: ISignal
{
    public Job _job;
    public OnJobFailed(Job job) => _job = job;
}