using UnityEngine;

public class OnMiningJobStarted: ISignal
{
    public Job _job;
    public Worker _worker;

    public OnMiningJobStarted(Job job, Worker worker)
    {
        _job = job;
        _worker = worker;
    }
}