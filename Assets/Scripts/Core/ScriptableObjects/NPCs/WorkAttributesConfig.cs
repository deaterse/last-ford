using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WorkAttributes", menuName = "Scriptable Objects/NPCs/WorkAttributesConfig")]
public class WorkAttributesConfig : ScriptableObject
{
    [SerializeField] private List<JobAttribute> _attributesList = new();
    
    public List<JobAttribute> AttributesList => _attributesList;
}
