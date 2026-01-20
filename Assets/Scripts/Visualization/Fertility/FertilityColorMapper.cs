using UnityEngine;

public class FertilityColorMapper
{
    public Color GetColor(float value)
    {
        if (value < 0.2f) return new Color(0.63f, 0.32f, 0.18f);
        else if (value < 0.5f) return new Color(0.96f, 0.64f, 0.38f);
        else if (value < 0.8f) return new Color(0.56f, 0.93f, 0.56f);
        else return new Color(0.13f, 0.55f, 0.13f);
    }
}
