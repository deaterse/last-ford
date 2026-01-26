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

    private bool _dayNow = true;
    private int _currentDay = 1;

    public void Init()
    {
        StartCoroutine(StartCycle());
    }

    private IEnumerator StartCycle()
    {
        if(_dayNow)
        {
            yield return new WaitForSeconds(_config.DayLength);
            ChangeToNight();
        }
        else
        {
            yield return new WaitForSeconds(_config.NightLength);
            ChangeToDay();
        }
    }

    private void ChangeToNight()
    {
        StartCoroutine(StartNight());
    }

    private IEnumerator StartNight()
    {
        _dayNow = false;

        while(_globalLight.intensity > 0.03f)
        {
            yield return new WaitForSeconds(0.05f);

            _globalLight.intensity -= 0.025f;
        }

        StartCoroutine(StartCycle());
    }

    private void ChangeToDay()
    {
        StartCoroutine(StartDay());
    }


    private IEnumerator StartDay()
    {
        _dayNow = true;
        _currentDay++;

        while(_globalLight.intensity < 1f)
        {
            yield return new WaitForSeconds(0.1f);

            _globalLight.intensity += 0.05f;
        }

        StartCoroutine(StartCycle());
    }

    public bool IsDay()
    {
        return _dayNow;
    }
}
