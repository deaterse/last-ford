using Unity.Mathematics;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystemConfig _config;

    private void Start()
    {
        ServiceLocator.GetService<EventBus>().Subscribe<OnMiningJobStarted>(SpawnCuttingParticle);
    }

    private void SpawnCuttingParticle(OnMiningJobStarted signal)
    {
        GameObject newParticle = Instantiate(_config.CuttingParticle, signal._job.JobPos, Quaternion.identity);

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
}
