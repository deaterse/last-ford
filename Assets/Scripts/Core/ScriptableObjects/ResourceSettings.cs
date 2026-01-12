using UnityEngine;

[CreateAssetMenu(fileName = "ResourceSpawnConfig", menuName = "Scriptable Objects/ResourceSpawn Config")]
public class ResourceSettings : ScriptableObject
{
    [SerializeField] private ResourceType _resourceType;

    public ResourceType resourceType => _resourceType;

    [SerializeField] [Range(0, 1f)] private float _resourceDensity = 0.1f;
    [SerializeField] [Min(1)] private int _minClusterCount;
    [SerializeField] [Min(1)] private int _maxClusterCount;

    public float ResourceDensity => _resourceDensity;
    public int MinClusterCount => _minClusterCount;
    public int MaxClusterCount => _maxClusterCount;
}