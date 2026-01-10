using UnityEngine;

public class HeightColorMapper
{
    public int GetColorInt(float weight)
    {
        if(weight < 0.5f)
        {
            return 0;
        }
        else if(weight < 0.7f)
        {
            return 1; 
           // new Color(0.9f, 0.9f, 0.9f);
        }
        else
        {
            return 2;
             //new Color(0.85f, 0.85f, 0.85f);
        }
    }
}
