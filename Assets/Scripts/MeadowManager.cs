using UnityEngine;
using System.Collections.Generic;

public class MeadowManager : MonoBehaviour
{
    private List<Meadow> _allMeadows = new();

    public void AddMeadow(Meadow newMeadow)
    {
        if(!_allMeadows.Contains(newMeadow))
        {
            _allMeadows.Add(newMeadow);
        }
    }
}
