using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using System.Collections;

public class WindowLightManager : MonoBehaviour, IService
{
    [Header("Settings")]
    [SerializeField] private DayCycle _cycleManager;
    [SerializeField] private float _maxIntensity = 1f;

    //Lights
    private List<Light2D> _spotLights = new();
    private List<Light2D> _spriteLights = new();

    public void Init()
    {
        ServiceLocator.ProvideService<WindowLightManager>(this);

        ServiceLocator.GetService<EventBus>().Subscribe<OnWindowAdded>(AddWindowSignal);
        ServiceLocator.GetService<EventBus>().Subscribe<OnWindowDestroyed>(DeleteWindowSignal);

        StartCoroutine(CheckTime());
    }

    private void AddWindowSignal(OnWindowAdded signal)
    {
        AddWindow(signal._spriteLight, signal._spotLight);
        WindowChange();
    }

    private void AddWindow(Light2D spriteLight, Light2D spotLight)
    {
        if(spotLight != null)
        {
            _spotLights.Add(spotLight);
        }

        if(spriteLight != null)
        {
            _spriteLights.Add(spriteLight);
        }
    }

    private void DeleteWindowSignal(OnWindowDestroyed signal)
    {
        DeleteWindow(signal._spriteLight, signal._spotLight);
    }

    private void DeleteWindow(Light2D spriteLight, Light2D spotLight)
    {
        if(_spotLights.Contains(spotLight))
        {
            _spotLights.Remove(spotLight);
        }

        if(_spriteLights.Contains(spriteLight))
        {
            _spriteLights.Remove(spriteLight);
        }
    }

    private IEnumerator CheckTime()
    {
        while(true)
        {
            WindowChange();

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void WindowChange()
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

            foreach(Light2D spriteLight in _spriteLights)
            {
                float multiplier = currentIntensity * 10;
                float decrease = multiplier * 1.25f;

                spriteLight.intensity = _maxIntensity * 10 - decrease;
            }
        }
        else
        {
            foreach(Light2D spotLight in _spotLights)
            {
                spotLight.intensity = 0; 
            }
            foreach(Light2D spriteLight in _spriteLights)
            {
                spriteLight.intensity = 0;
            }
        }
    }

    private void OnDestroy()
    {
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnWindowAdded>(AddWindowSignal);
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnWindowDestroyed>(DeleteWindowSignal);
        
        StopAllCoroutines();
    }
}
