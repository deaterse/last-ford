using UnityEngine;

public class OnJobFinished: ISignal
{
    public Job _job;
    public Worker _worker;

    public OnJobFinished(Job job, Worker worker)
    {
        _job = job;
        _worker = worker;
    }
}