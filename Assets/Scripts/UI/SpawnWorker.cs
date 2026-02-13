using UnityEngine;
using UnityEngine.Tilemaps;

//ONLY FOR TESTING PURPOSE
public class SpawnWorker : MonoBehaviour
{
    [SerializeField] private GameObject _workerPrefab;

    public void SpawnWorkerButton()
    {
        Transform spawnPos = Camera.main.transform;

        var newWorker = Instantiate(_workerPrefab);
        newWorker.transform.position = new Vector3(9.5f,9.5f, 0);
        
        ServiceLocator.GetService<EventBus>().Invoke<OnWorkerSpawned>(new OnWorkerSpawned(newWorker.GetComponent<Worker>()));
    }
}
