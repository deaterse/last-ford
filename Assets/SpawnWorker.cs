using UnityEngine;

//ONLY FOR TESTING PURPOSE
public class SpawnWorker : MonoBehaviour
{
    [SerializeField] private GameObject _workerPrefab;

    public void SpawnWorkerButton()
    {
        Transform spawnPos = Camera.main.transform;

        var newWorker = Instantiate(_workerPrefab);

        ServiceLocator.GetService<EventBus>().Invoke<OnWorkerSpawned>(new OnWorkerSpawned(newWorker.GetComponent<Worker>()));
    }
}
