using UnityEngine;
using UnityEngine.Tilemaps;

//ONLY FOR TESTING PURPOSE
public class SpawnWorker : MonoBehaviour
{
    [SerializeField] private GameObject _workerPrefab;
    [SerializeField] private NPCsSpritesConfig _npcsConfig;
    [SerializeField] private WorkAttributesConfig _attributesConfig;

    public void SpawnWorkerButton()
    {
        Transform spawnPos = Camera.main.transform;

        var newWorker = Instantiate(_workerPrefab);
        if(newWorker.TryGetComponent<Worker>(out Worker worker))
        {
            Debug.Log("initing");
            worker.Init(_npcsConfig, _attributesConfig);
        }
        newWorker.transform.position = transform.position;
        
        ServiceLocator.GetService<EventBus>().Invoke<OnWorkerSpawned>(new OnWorkerSpawned(newWorker.GetComponent<Worker>()));
    }
}
