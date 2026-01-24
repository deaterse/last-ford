using UnityEngine;

public class OnJobCreated: ISignal
{
    public Job _job;
    public OnJobCreated(Job job) => _job = job;
}