using UnityEngine;

public class OnJobFailed: ISignal
{
    public Job _job;
    public Worker _worker;

    public OnJobFailed(Job job, Worker worker)
    {
        _job = job;
        _worker = worker;
    }
}