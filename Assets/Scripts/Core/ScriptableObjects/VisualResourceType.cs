using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

//ITS LIKE A OAK/PINE/OLD

[CreateAssetMenu(fileName = "VisualResType", menuName = "Scriptable Objects/VisualRes Type")]
public class VisualResourceType : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private List<TileBase> _resourceTiles;

    public string Name => _name;
    public List<TileBase> ResourceTiles => _resourceTiles;
}
