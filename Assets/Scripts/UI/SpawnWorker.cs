using UnityEngine;
using UnityEngine.Tilemaps;

//ONLY FOR TESTING PURPOSE
public class SpawnWorker : MonoBehaviour
{
    [SerializeField] private GameObject _workerPrefab;
    [SerializeField] private NPCsConfig _npcsConfig;
    [SerializeField] private WorkAttributesConfig _attributesConfig;

    public void SpawnWorkerButton()
    {
        GameObject newWorker = Instantiate(_workerPrefab, transform.position, Quaternion.identity);
        Worker _currentWorker;

        newWorker.transform.parent = null;

        if(newWorker.TryGetComponent<Worker>(out _currentWorker))
        {
            _currentWorker.Init(_npcsConfig, _attributesConfig);
        }
        else
        {
            Debug.LogWarning("U are trying to spawn not a Worker");
        }

        ServiceLocator.GetService<EventBus>().Invoke<OnWorkerSpawned>(new OnWorkerSpawned(_currentWorker));
    }
}
