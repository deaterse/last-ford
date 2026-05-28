using UnityEngine;

public class OnFertilityMapGenerated : ISignal
{
    public FertilityMap _fertilityMap;
    public OnFertilityMapGenerated(FertilityMap fertilityMap) => _fertilityMap = fertilityMap;
}
