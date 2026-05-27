using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;

public class Worker : MonoBehaviour
{
    [SerializeField] private List<StateString> _statesByString;
    
    [SerializeField] private ResourceAmount _inventory;
    
    [SerializeField] private SpriteRenderer _workerRenderer;
    [SerializeField] private SpriteRenderer _attributeRenderer;

    private State _currentState;
    private bool _atWork = false;

    public ResourceAmount CurrentInventoryResource => _inventory;

    private Building _assignedBuilding;
    private Job _currentJob;
    private Job _lastJob;

    private NPCsConfig _npcsConfig;
    private WorkAttributesConfig _attributesConfig;

    private Vector3Int _destinition;

    private WorkerUI _workerUI;

    public State CurrentState => _currentState;
    public Job CurrentJob => _currentJob;
    public Job LastJob => _lastJob;
    public Building AssignedBuilding => _assignedBuilding;

    public void Init(NPCsConfig npcsConfig, WorkAttributesConfig attributesConfig)
    {
        if(TryGetComponent<WorkerUI>(out WorkerUI workerUI))
        {
            _workerUI = workerUI;
            _workerUI.Init(this);
        }

        ChangeState<IdleState>();

        _npcsConfig = npcsConfig;
        _attributesConfig = attributesConfig;

        ChooseRandomParameters();
    }

    private void ChooseRandomParameters()
    {
        ChooseRandomSprite();
        ChooseRandomSpeed();
    }

    private void ChooseRandomSpeed()
    {
        if(_npcsConfig != null && _npcsConfig.MinSpeed > 0 && _npcsConfig.MaxSpeed > 0)
        {
            foreach(StateString stateStr in _statesByString)
            {
                if(stateStr.name == "IdleState")
                {
                    try
                    {
                        IdleState idleState = (IdleState) stateStr.state;
                        float randomSpeed = UnityEngine.Random.Range(_npcsConfig.MinSpeed, _npcsConfig.MaxSpeed);

                        idleState.SetSpeed(randomSpeed);
                    }
                    catch
                    {
                        Debug.LogWarning("Something wrong with choosing Random Speed,\nlooks like IdleState is not a IdleState\n, check properties!");
                    }
                }
            }
        }
    }

    private void ChooseRandomSprite()
    {
        if(_npcsConfig != null && _npcsConfig.VillagersTypes.Count > 0 && _workerRenderer != null)
        {
            Sprite choosenSprite = _npcsConfig.VillagersTypes[UnityEngine.Random.Range(0, _npcsConfig.VillagersTypes.Count)];

            _workerRenderer.sprite = choosenSprite;
        }
        else
        {
            Debug.LogWarning("Something wrong with NPCsConfig");
        }
    }

    private void ChangeAttribute(JobType jobType, ResourceType resourceType)
    {
        if(_attributeRenderer != null)
        {
            foreach(JobAttribute attribute in _attributesConfig.AttributesList)
            {
                if(attribute.jobType == jobType && attribute.resourceType == resourceType)
                {
                    _attributeRenderer.sprite = attribute.sprite;
                }
            }
        }
        else
        {
            Debug.LogWarning("AttributeRenderer is null");
        }
    }

    private void ClearAttribute()
    {
        _attributeRenderer.sprite = null;
    }

    public void StopJob()
    {
        _assignedBuilding = null;
        ClearAttribute();
        if(_currentJob != null)
        {
            JobFailed();
        }
    }

    public void ChangeState<T>(object data = null) where T : State
    {
        Debug.Log($"ChangeState<{typeof(T).Name}> called on {gameObject.name}, frame: {Time.frameCount}, stack: {Environment.StackTrace}");
    
        foreach(StateString sstr in _statesByString)
        {
            if(sstr.name == typeof(T).Name)
            {
                _currentState?.Exit();
                _currentState = sstr.state;

                if (data != null)
                    _currentState.SetData(data);
                else
                    _currentState.ClearData();
                    
                _currentState.Enter();
                return;
            }
        }

        Debug.LogWarning($"State:{typeof(T).Name} is not exist");
    }
    
    public bool ResourceIsGone()
    {
        if(_currentJob != null)
        {
            return !ServiceLocator.GetService<TerrainMapManager>().IsResource(_currentJob.ResourcePos);
        }
        else
        {
            return true;
        }
    }

    private void Update()
    {
        _currentState?.OnUpdate();

        // TryGetJob();
    }

    // public void TryGetJob()
    // {
    //     if (!_assignedBuilding) return;

    //     if (_currentJob == null && !_assignedBuilding.HaveJob)
    //     {
    //         _currentJob = _assignedBuilding.GetAvailableJob(_lastJob);
    //         if (_currentJob != null)
    //             StartJob();
    //     }
    // }

    public void SetJob(Job job)
    {

        if(!_atWork)
        {
            _currentJob = job;
            if(_currentJob != null)
            {
                StartJob();
            }
        }
    }

    public void AssignToBuilding(Building building)
    {
        StopJob();

        _assignedBuilding = building;

        ChangeAttribute(_assignedBuilding.buildingData.jobType, _assignedBuilding.buildingData.resourceType);
    }

    private void ChangeInventoryResource(ResourceType resource = ResourceType.None, int amount = 0)
    {
        _inventory.Type = resource;
        _inventory.Amount = amount;

        ServiceLocator.GetService<EventBus>().Invoke<OnInventoryChanged>(new OnInventoryChanged(this, _inventory));
    }

    public void StartJob()
    {
        if(_currentJob != null)
        {
            if(_currentJob.jobType == JobType.Mining)
            {
                //CHANGE LATER
                Vector3Int buildingPos = new Vector3Int(_currentJob.BuildingPos.x, _currentJob.BuildingPos.y-1,_currentJob.BuildingPos.z);
                //
                MovingData toBuildingData = new MovingData(buildingPos, () => JobEnded());
                WorkingData endJobData = new WorkingData(_currentJob.JobTime, JobType.Mining, () => AfterMiningJob(toBuildingData));
                MovingData toJobData = new MovingData(_currentJob.JobPos,() => StartMiningJob(endJobData));

                _atWork = true;
                
                ChangeState<MovingState>(toJobData);
            }
            else if(_currentJob.jobType == JobType.Production && _currentJob.StoragePos != default(Vector3Int))
            {
                MovingData backToStorageData = new MovingData(_currentJob.StoragePos, () => JobEnded()); 
                WorkingData workingData = new WorkingData(_currentJob.JobTime, JobType.Production, () => BackToStorage(backToStorageData));
                MovingData toBuildingData = new MovingData(_currentJob.BuildingPos, () => WorkingProduction(workingData));
                MovingData toStorageData = new MovingData(_currentJob.StoragePos, () => AfterTakingJob(toBuildingData));

                _atWork = true;
                
                ChangeState<MovingState>(toStorageData);
            }
            else
            {
                Debug.LogError("Problem with job in Worker!");
            }
        }
    }
    
    private void StartMiningJob(WorkingData wokringData)
    {
        GameObject particleType = _assignedBuilding.WorkParticles;
        ServiceLocator.GetService<EventBus>().Invoke<OnMiningJobStarted>(new OnMiningJobStarted(_currentJob, this, particleType));

        ChangeState<WorkingState>(wokringData);
    }

    //1
    private void AfterTakingJob(MovingData toBuildingData)
    {
        ResourceAmount spendResource = ServiceLocator.GetService<JobManager>().GetSpendingResource(_currentJob);
        ChangeInventoryResource(spendResource.Type, spendResource.Amount);

        ServiceLocator.GetService<EventBus>().Invoke<OnResourcesTakenFromStorage>(new OnResourcesTakenFromStorage(_currentJob, this));
        
        ChangeState<MovingState>(toBuildingData);
    }

    //2 => 3 (after moving to building)
    private void WorkingProduction(WorkingData workingData)
    {
        ChangeInventoryResource();

        ChangeState<WorkingState>(workingData);
    }

    // 3 => 4 (after work at building)
    private void BackToStorage(MovingData movingData)
    {
        ResourceAmount rewardResource = ServiceLocator.GetService<JobManager>().GetRewardResource(_currentJob);
        ChangeInventoryResource(rewardResource.Type, rewardResource.Amount);
        
        ChangeState<MovingState>(movingData);
    }

    private void AfterMiningJob(MovingData toBuildingData)
    {
        //refactor, amount need to move to another place (5)
        ResourceAmount rewardResource = ServiceLocator.GetService<JobManager>().GetRewardResource(_currentJob);
        ChangeInventoryResource(rewardResource.Type, rewardResource.Amount);

        ServiceLocator.GetService<EventBus>().Invoke<OnResourceMined>(new OnResourceMined(this, _currentJob.ResourcePos, 5, _assignedBuilding.buildingData.resourceType));
        
        ChangeState<MovingState>(toBuildingData);
    }

    public void JobEnded()
    {
        ChangeInventoryResource();

        ChangeState<IdleState>();

        if(_currentJob != null)
            ServiceLocator.GetService<EventBus>().Invoke<OnJobFinished>(new OnJobFinished(_currentJob, this));

        OnJobCompleted();
    }

    public void JobFailed()
    {
        ChangeInventoryResource();

        ServiceLocator.GetService<EventBus>().Invoke<OnJobFailed>(new OnJobFailed(_currentJob, this));

        OnJobCompleted();

        ChangeState<IdleState>();
    }

    public void OnJobCompleted()
    {
        _lastJob = _currentJob;
        _currentJob = null;
        _atWork = false;
    }
}
