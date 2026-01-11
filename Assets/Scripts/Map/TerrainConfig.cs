using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TerrainConfig", menuName = "Scriptable Objects/Terrain Config")]
public class TerrainConfig : ScriptableObject
{
    [SerializeField] private TerrainType _terrainType;
    [SerializeField] private List<TileBase> _terrainTiles;

    public TerrainType terrainType => _terrainType;
    public List<TileBase> terrainTiles => _terrainTiles;
}
