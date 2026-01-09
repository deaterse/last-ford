using UnityEngine;

[CreateAssetMenu(fileName = "NoiseConfig", menuName = "Scriptable Objects/Noise Config")]
public class NoiseConfig : ScriptableObject
{
    [SerializeField] private float _scale;

    public float Scale => _scale;
}
