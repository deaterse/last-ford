using UnityEngine;

public class PulseLight : MonoBehaviour
{
    [Header("Pulse Settings")]
    [SerializeField] private float _pulseAmount = 0.05f;
    [SerializeField] private float _pulseSpeed = 1f;

    private Vector3 _baseScale;

    void Start()
    {
        _baseScale = transform.localScale;
    }

    void Update()
    {
        float pulse = 1 + Mathf.Sin(Time.time * _pulseSpeed) * _pulseAmount;

        transform.localScale = _baseScale * pulse;
    }
}
