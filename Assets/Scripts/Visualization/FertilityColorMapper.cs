using UnityEngine;

public class FertilityColorMapper
{
    public float GetColorFloat(float weight)
    {
        if(weight < 0.5f)
        {
            return 0.3f;
        }
        else if(weight < 0.7f)
        {
            return 0.6f; 
        }
        else
        {
            return 0.9f;
        }
    }
}
