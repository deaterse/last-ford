using UnityEngine;
using System.Collections.Generic;

public class CropManager : MonoBehaviour
{
    private CropMap _cropMap;
    private List<GameObject> _cropInstances = new();

    public void Init()
    {
        ServiceLocator.GetService<EventBus>().Subscribe<OnCropBuilded>(AddCrop);
    }

    public void AddCrop(OnCropBuilded signal)
    {
        Crop crop = signal._crop;
        GameObject cropObj = crop.gameObject;
        Vector2Int pos = signal._pos;

        _cropMap.SetTile(pos.x, pos.y, new CropData(crop));

        _cropInstances.Add(cropObj);
    }
}
