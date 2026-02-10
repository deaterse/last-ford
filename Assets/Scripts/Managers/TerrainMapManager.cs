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
        ServiceLocator.GetService<EventBus>().Subscribe<OnResourceMined>(DecreaseResource);
    }

    public bool IsResource(Vector3Int pos)
    {
        return terrainMap.HasResource(pos.x, pos.y);
    }

    public void RemoveResource(Vector3Int resourcePos)
    {
        if(resourcePos.x > _terrainMap.Width || resourcePos.y > _terrainMap.Height) return;

        _resourceMap.SetTile(resourcePos, null);
        _terrainMap.SetResource(resourcePos.x, resourcePos.y, Resource.None);
    }
    
    public void DecreaseResource(OnResourceMined signal)
    {
        Vector3Int resourcePos = signal._resourcePosition;
        int amount = signal._value;

        if(resourcePos.x > _terrainMap.Width || resourcePos.y > _terrainMap.Height) return;

        bool isDecreased = terrainMap.TerrainData[resourcePos.x, resourcePos.y].TryDecreaseResource(amount);

        if(!isDecreased)
        {
            RemoveResource(resourcePos);
        }
    }
}