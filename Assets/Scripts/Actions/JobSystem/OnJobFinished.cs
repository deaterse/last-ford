using UnityEngine;

public class OnJobFinished: ISignal
{
    public Job _job;
    public OnJobFinished(Job job) => _job = job;
}