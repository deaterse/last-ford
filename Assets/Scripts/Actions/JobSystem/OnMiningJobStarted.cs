using UnityEngine;

public class OnMiningJobStarted: ISignal
{
    public Job _job;

    public OnMiningJobStarted(Job job)
    {
        _job = job;
    }
}