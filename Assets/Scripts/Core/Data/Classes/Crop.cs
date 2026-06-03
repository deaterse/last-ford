using UnityEngine;

public class Crop : MonoBehaviour
{
    public CropType Type;
    public int Amount;
    public float Progress;

    public Crop(CropType type, int amount = 0)
    {
        Type = type;
        Amount = amount;
        Progress = 0f;
    }
}
