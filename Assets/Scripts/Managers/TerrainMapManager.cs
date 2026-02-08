using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainMapManager: MonoBehaviour, IService
{
    private TerrainMap _terrainMap;

    [Header("Tilemaps")]
    [SerializeField] private Tilemap _resourceMap;
    [SerializeField] private Tilemap _terrainTilemap;

    public TerrainMap terrainMap => _terrainMap;

    public void Init(TerrainMap terrainMap)
    {
        _terrainMap = terrainMap;
    }

    public void RemoveResource(Vector3Int resourcePos)
    {
        if(resourcePos.x > _terrainMap.Width || resourcePos.y > _terrainMap.Height) return;

        _resourceMap.SetTile(resourcePos, null);
        _terrainMap.SetResource(resourcePos.x, resourcePos.y, Resource.None);
    }
}