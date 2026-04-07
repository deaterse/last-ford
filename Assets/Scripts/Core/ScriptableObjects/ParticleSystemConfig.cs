using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ParticleConfig", menuName = "Scriptable Objects/Particles/ParticleConfig")]
public class ParticleSystemConfig : ScriptableObject
{
    [SerializeField] private GameObject _cuttingParticle;

    public GameObject CuttingParticle => _cuttingParticle;
}
