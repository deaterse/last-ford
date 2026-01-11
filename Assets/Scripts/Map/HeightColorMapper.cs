using UnityEngine;

public class HeightColorMapper
{
    public float GetColorFloat(float weight)
    {
        if(weight < 0.5f)
        {
            return 0f;
        }
        else if(weight < 0.7f)
        {
            return 0.15f; 
        }
        else
        {
            return 0.3f;
        }
    }
}
