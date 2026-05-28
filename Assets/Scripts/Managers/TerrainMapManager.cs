using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class TerrainMapManager: MonoBehaviour, IService
{
    private TerrainMap _terrainMap;
    private GameObject[,] _resourceMap;

    [Header("Tilemaps")]
    [SerializeField] private Tilemap _terrainTilemap;

    public TerrainMap terrainMap => _terrainMap;

    public void Init(TerrainMap terrainMap)
    {
        _terrainMap = terrainMap;

        ServiceLocator.GetService<EventBus>().Subscribe<OnResourcesVisualized>(SetResources);
        ServiceLocator.GetService<EventBus>().Subscribe<OnResourceMined>(DecreaseResource);
    }

    private void SetResources(OnResourcesVisualized signal)
    {
        _resourceMap = signal._resourcesObj;
    }

    public bool IsResource(Vector3Int pos)
    {
        return terrainMap.HasResource(pos.x, pos.y);
    }

    public void RemoveResource(Vector3Int resourcePos)
    {
        if(resourcePos.x > _terrainMap.Width || resourcePos.y > _terrainMap.Height) return;

        GameObject deletingRes = _resourceMap[resourcePos.x, resourcePos.y];

        DOTween.Kill(deletingRes.transform);
        deletingRes.transform.DOScale(Vector3.zero, 0.3f)
            .OnComplete(() => Destroy(_resourceMap[resourcePos.x, resourcePos.y]));

        _resourceMap[resourcePos.x, resourcePos.y] = null;
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

    public void AnimateResource(Vector3Int resourcePos)
    {
        if(_resourceMap[resourcePos.x, resourcePos.y] != null)
        {
            GameObject currentObj = _resourceMap[resourcePos.x, resourcePos.y];

            ResourceAnimation(resourcePos.x, resourcePos.y);
        }
    }

    public void StopAnimation(Vector3Int resourcePos)
    {
        if(_resourceMap[resourcePos.x, resourcePos.y] != null)
        {
            GameObject currentObj = _resourceMap[resourcePos.x, resourcePos.y];

            DOTween.Kill(currentObj.transform);
        }
    }

    private void ResourceAnimation(int x, int y)
    {
        Transform resTransform = _resourceMap[x, y].transform;

        Vector3 scaleBefore = resTransform.localScale;

        resTransform.DOScale(scaleBefore * 1.2f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .OnKill(() => resTransform.localScale = scaleBefore);
    }
}