using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OnWindowDestroyed : ISignal
{
    public Light2D _spriteLight;
    public Light2D _spotLight;

    public OnWindowDestroyed(Light2D spriteLight = null, Light2D spotLight = null)
    {
        _spriteLight = spriteLight;
        _spotLight = spotLight;
    }
}
