using UnityEngine;

public class HeightColorMapper
{
    public Color GetColor(float weight)
    {
        if(weight < 0.5f)
        {
            return Color.white;
        }
        else if(weight < 0.7f)
        {
            return new Color(0.9f, 0.9f, 0.9f);
        }
        else
        {
            return new Color(0.85f, 0.85f, 0.85f);
        }
    }
}
