using UnityEngine;

public class OnResourcesTakenFromStorage: ISignal
{
    public Job _job;
    public Worker _worker;

    public OnResourcesTakenFromStorage(Job job, Worker worker)
    {
        _job = job;
        _worker = worker;
    }
}