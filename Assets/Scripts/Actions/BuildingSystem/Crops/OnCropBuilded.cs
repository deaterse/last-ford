using UnityEngine;

public class OnCropBuilded: ISignal
{
    public Crop _crop;
    public Vector2Int _pos;

    public OnCropBuilded(Crop crop, Vector2Int pos)
    {
        _crop = crop;
        _pos = pos;
    }
}
