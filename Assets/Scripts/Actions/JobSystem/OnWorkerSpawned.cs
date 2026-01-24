using UnityEngine;

public class OnWorkerSpawned: ISignal
{
    public Worker _worker;
    public OnWorkerSpawned(Worker worker) => _worker = worker;
}