using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystemConfig _config;
    public Dictionary<Worker, GameObject> _workersParticles = new Dictionary<Worker, GameObject>();

    private void Start()
    {
        ServiceLocator.GetService<EventBus>().Subscribe<OnMiningJobStarted>(SpawnCuttingParticle);
        ServiceLocator.GetService<EventBus>().Subscribe<OnJobFailed>(DeleteParticleFailed);
    }

    private void SpawnCuttingParticle(OnMiningJobStarted signal)
    {
        GameObject newParticle = Instantiate(_config.CuttingParticle, signal._job.JobPos, Quaternion.identity);

        _workersParticles[signal._worker] = newParticle;

        SetUpParticle(newParticle, signal._job);
    }

    private void SetUpParticle(GameObject particle, Job job)
    {
        if(particle.TryGetComponent<ParticleSystem>(out ParticleSystem particleSystem))
        {
            particleSystem.Stop();

            var main = particleSystem.main;
            main.duration = 5f;

            particleSystem.Play();
        }
    }

    private void DeleteParticleFailed(OnJobFailed signal)
    {
        DeleteParticle(signal._worker);
    }

    private void DeleteParticle(Worker worker)
    {
        if(_workersParticles.ContainsKey(worker))
        {
            Destroy(_workersParticles[worker]);
        }
    }
}
