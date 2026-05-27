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
    
    [Header("Texts")]
    [SerializeField] private TMP_Text _cycleText;
    [SerializeField] private TMP_Text _dayText;

    private float _time = 0.25f;

    private CycleType _cycleNow;
    private int _currentDay = 1;
    
    private static readonly WaitForSeconds waitSecond = new WaitForSeconds(1);

    public float Time => _time;
    public float Intensity => _globalLight.intensity;


    public void Init()
    {
        CoroutinesRun();

        UpdateUI();
    }

    private void CoroutinesRun()
    {
        StartCoroutine(TimeCycle());
        StartCoroutine(DuringNight());
    }

    private void UpdateUI()
    {
        UpdateCycleText();
        UpdateDayText();
    }

    private void UpdateCycleText()
    {
        _cycleNow = CheckCycle();
        _cycleText.text = _cycleNow.ToString();
    }

    private void UpdateDayText()
    {
        _dayText.text = _currentDay.ToString();
    }

    private IEnumerator TimeCycle()
    {
        float duration = _config.DayLength + _config.NightLength;
        while(true)
        {
            float dayLeftover = _time * duration;

            float iterations = duration - dayLeftover;
            float step = 1f / duration;

            for(int i = 0; i < iterations; i++)
            {
                _time += step;
                
                UpdateCycleText();

                yield return waitSecond;
            }

            _currentDay += 1;
            _time = 0;

            UpdateDayText();
        }
    }

    private void ChangeToNight()
    {
        StartCoroutine(DuringDay());
    }

    private void ChangeToDay()
    {
        StartCoroutine(DuringNight());
    }

    private IEnumerator DuringDay()
    {
        float way = -(_config.MinIntesity - _globalLight.intensity);
        float step = way / _config.DayLength;

        while(_globalLight.intensity > _config.MinIntesity)
        {
            _globalLight.intensity -= step;

            yield return waitSecond;
        }
        _globalLight.intensity = _config.MinIntesity;

        ChangeToDay();
    }

    private IEnumerator DuringNight()
    {
        float step = (_config.MaxIntensity - _globalLight.intensity) / _config.DayLength;
        while(_globalLight.intensity < _config.MaxIntensity)
        {
            _globalLight.intensity += step;

            yield return waitSecond;
        }
        
        _globalLight.intensity = _config.MaxIntensity;

        ChangeToNight();
    }

    private CycleType CheckCycle()
    {
        float intensity = _globalLight.intensity;
        float localTime = _time;

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
