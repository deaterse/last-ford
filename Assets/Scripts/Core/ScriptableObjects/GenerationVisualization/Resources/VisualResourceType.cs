using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "VisualResType", menuName = "Scriptable Objects/GenerationSystem/VisualRes Type")]
public class VisualResourceType : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private List<GameObject> _resourceObjs;

    public string Name => _name;
    public List<GameObject> ResourceObjs => _resourceObjs;
}
