using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayCycle : MonoBehaviour, IService
{
    [Header("Config")]
    [SerializeField] private DayNightConfig _config;

    [Header("Light")]
    [SerializeField] private Light2D _globalLight;
    [SerializeField] private TMP_Text _cycleText;

    [SerializeField] private float time = 0.25f;

    private CycleType _cycleNow;
    private int _currentDay = 1;

    public void Init()
    {
        CoroutinesRun();
    }

    private void CoroutinesRun()
    {
        StartCoroutine(TimeCycle());
        StartCoroutine(DuringNight());
    }

    private void CycleText()
    {
        _cycleNow = CheckCycle();
        _cycleText.text = _cycleNow.ToString();
    }

    private IEnumerator TimeCycle()
    {
        float duration = _config.DayLength + _config.NightLength;
        while(true)
        {
            float dayLeftover = time * duration;

            float iterations = duration - dayLeftover;
            float step = 1f / duration;

            for(int i = 0; i < iterations; i++)
            {
                time += step;
                
                CycleText();

                yield return new WaitForSeconds(1);
            }

            time = 0;
        }
    }

    private void ChangeToNight()
    {
        StartCoroutine(DuringDay());
    }

    private IEnumerator DuringDay()
    {
        float way = -(_config.MinIntesity - _globalLight.intensity);
        float step = way / _config.DayLength;

        while(_globalLight.intensity > _config.MinIntesity)
        {
            _globalLight.intensity -= step;

            yield return new WaitForSeconds(1);
        }
        _globalLight.intensity = _config.MinIntesity;

        ChangeToDay();
    }

    private void ChangeToDay()
    {
        StartCoroutine(DuringNight());
    }

    private IEnumerator DuringNight()
    {
        float step = (_config.MaxIntensity - _globalLight.intensity) / _config.DayLength;
        while(_globalLight.intensity < _config.MaxIntensity)
        {
            _globalLight.intensity += step;

            yield return new WaitForSeconds(1);
        }
        
        _globalLight.intensity = _config.MaxIntensity;

        ChangeToNight();
    }

    private CycleType CheckCycle()
    {
        float intensity = _globalLight.intensity;
        float localTime = time;

        if(intensity >= 0.5f && localTime >= 0.25f && localTime <= 0.5f)
        {
            return CycleType.Morning;
        }
        else if(intensity >= 0.5f && localTime >= 0.5f && localTime <= 0.75f)
        {
            return CycleType.AfterNoon;
        }
        else if(intensity >= 0.2f && localTime >= 0.75f && localTime <= 0.92f)
        {
            return CycleType.Evening;
        }
        else
        {
            return CycleType.Night;
        }
    }
}
