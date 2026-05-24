using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using System.Collections;

public class WindowLightManager : MonoBehaviour
{
    [SerializeField] private DayCycle _cycleManager;
    [SerializeField] private float _maxIntensity = 1f;

    [SerializeField]private List<Light2D> _spotLights;
    private List<Light2D> _spriteLights;

    private void Start()
    {
        StartCoroutine(CheckTime());
    }

    private IEnumerator CheckTime()
    {
        while(true)
        {
            float currentIntensity = _cycleManager.Intensity;
            if(currentIntensity < 0.6f)
            {
                foreach(Light2D spotLight in _spotLights)
                {
                    float multiplier = currentIntensity * 10;
                    float decrease = multiplier * 0.125f;

                    spotLight.intensity = _maxIntensity - decrease;
                }
            }
            else
            {
                foreach(Light2D spotLight in _spotLights)
                {
                    spotLight.intensity = 0; 
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
