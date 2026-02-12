using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class JobManager : MonoBehaviour, IService
{
    [SerializeField] private JobsRewardsConfig _jobsRewConfig;

    private Dictionary<Building, int> _freeBuildings = new(); // Building = Free Places
    private List<Worker> _freeWorkers = new();

    public void Init()
    {
        ClearAll();

        ServiceLocator.ProvideService<JobManager>(this);

        ServiceLocator.GetService<EventBus>().Subscribe<OnJobFinished>(JobFinished);

        ServiceLocator.GetService<EventBus>().Subscribe<OnBuildingFinished>(NewFreeBuilding);
        ServiceLocator.GetService<EventBus>().Subscribe<OnWorkerSpawned>(NewFreeWorker);
    }

    private void OnDisable()
    {
        ClearAll();

        ServiceLocator.GetService<EventBus>().Unsubscribe<OnJobFinished>(JobFinished);

        ServiceLocator.GetService<EventBus>().Unsubscribe<OnBuildingFinished>(NewFreeBuilding);
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnWorkerSpawned>(NewFreeWorker);
    }

    private void ClearAll()
    {
        _freeBuildings.Clear();
        _freeWorkers.Clear();

        StopAllCoroutines();
    }

    private void NewFreeBuilding(OnBuildingFinished signal)
    {
        Building freeBuilding = signal.building;
        int workersCount = signal.WorkersCount;

        _freeBuildings[freeBuilding] = workersCount;

        TryFillBuilding(freeBuilding);
    }

    private void TryFillBuilding(Building building)
    {
        int needed = _freeBuildings[building];
        
        while (needed > 0 && _freeWorkers.Count > 0)
        {
            Worker freeWorker = _freeWorkers[0];
            AssignWorker(freeWorker, building);
            needed--;
        }
        
        if(needed > 0)
        {
            _freeBuildings[building] = needed;
        }
        else
        {
            _freeBuildings.Remove(building);
        }
    }

    private void TryAssignWorker(Worker worker)
    {
        if(_freeBuildings.Count > 0)
        {
            var pair = _freeBuildings.ElementAt(0);
            int needed = _freeBuildings[pair.Key];

            AssignWorker(worker, pair.Key);
            if(_freeBuildings[pair.Key] - 1 > 0)
            {
                _freeBuildings[pair.Key] -= 1;
            }
            else
            {
                _freeBuildings.Remove(pair.Key);
            }
        }
    }

    private void AssignWorker(Worker worker, Building building)
    {
        _freeWorkers.Remove(worker);
        building.AssignWorker(worker);
    }

    private void NewFreeWorker(OnWorkerSpawned signal)
    {
        Worker freeWorker = signal._worker;

        _freeWorkers.Add(freeWorker);

        TryAssignWorker(freeWorker);
    }

    private void JobFinished(OnJobFinished signal)
    {
        RewardJob(signal._job);
    }

    private void RewardJob(Job job)
    {
        List<ResourceAmount> resourcesAmount = _jobsRewConfig.GetRewards(job.jobType);
        foreach(ResourceAmount ra in resourcesAmount)
        {
            ServiceLocator.GetService<ResourceManager>().AddResource(ra.Type, ra.Amount);
        }
    }
}
