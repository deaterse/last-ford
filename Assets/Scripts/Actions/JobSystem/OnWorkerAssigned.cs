using UnityEngine;

public class OnWorkerAssigned : ISignal
{
    public Building _building;
    public OnWorkerAssigned(Building building) => _building = building;
}
