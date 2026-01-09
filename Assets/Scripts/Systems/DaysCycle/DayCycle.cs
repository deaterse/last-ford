using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayCycle : MonoBehaviour
{
    public static DayCycle Instance { get; private set; }

    private bool _dayNow = true;

    private int _currentDay = 1;

    [SerializeField] private int _dayLength;
    [SerializeField] private int _nightLength;

    [SerializeField] private Light2D _globalLight;

    [SerializeField] private TMP_Text _dayText;

    private void Awake()
    {
        Instance = this;    
    }

    private void Start()
    {
        StartCoroutine(StartCycle());
    }

    private IEnumerator StartCycle()
    {
        if(_dayNow)
        {
            yield return new WaitForSeconds(_dayLength);
            ChangeToNight();
        }
        else
        {
            yield return new WaitForSeconds(_nightLength);
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
        
        UpdateUI();

        while(_globalLight.intensity < 1f)
        {
            yield return new WaitForSeconds(0.1f);

            _globalLight.intensity += 0.05f;
        }

        StartCoroutine(StartCycle());
    }

    private void UpdateUI()
    {
        _dayText.text = $"DAY {_currentDay}";
    }

    public bool IsDay()
    {
        return _dayNow;
    }
}
