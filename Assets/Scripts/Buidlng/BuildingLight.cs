using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using System.Collections.Generic;

public class BuildingLight : MonoBehaviour
{
    [SerializeField] private List<Light2D> _allLights;

    void Update()
    {
        if(DayCycle.Instance.IsDay())
        {
            foreach(Light2D light in _allLights)
            {
                light.intensity = 0;
            }
        }
        else
        {
            foreach(Light2D light in _allLights)
            {
                light.intensity = 1;
            }
        }
    }
}
