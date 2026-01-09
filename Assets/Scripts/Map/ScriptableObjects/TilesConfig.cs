using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TilesConfig", menuName = "Scriptable Objects/Tiles Config")]
public class TilesConfig : ScriptableObject
{
    public List<TileBase> _waterTiles;
    public List<TileBase> _sandTiles;
    public List<TileBase> _grassTiles;
    public List<TileBase> _mountainTiles;
}
