using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StartResourcesConfig", menuName = "Scriptable Objects/StartRecources Config")]
public class StartResourcesConfig : ScriptableObject
{
    [SerializeField] private List<ResourceType> _resourcesStartTypes = new();
    [SerializeField] private List<int> _resourcesStartValues = new();

    public List<ResourceType> ResourcesStartTypes => _resourcesStartTypes;
    public List<int> ResourcesStartValues => _resourcesStartValues;
}
