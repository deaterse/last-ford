using UnityEngine;

public class OnResourcesVisualized : ISignal
{
    public GameObject[,] _resourcesObj;

    public OnResourcesVisualized(GameObject[,] resourcesObj) => _resourcesObj = resourcesObj;
}
