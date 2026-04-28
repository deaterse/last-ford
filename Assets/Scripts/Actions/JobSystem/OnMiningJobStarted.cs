using UnityEngine;

public class OnMiningJobStarted: ISignal
{
    public Job _job;
    public Worker _worker;
    public GameObject _particles;

    public OnMiningJobStarted(Job job, Worker worker, GameObject particles = null)
    {
        _job = job;
        _worker = worker;
        _particles = particles;
    }
}