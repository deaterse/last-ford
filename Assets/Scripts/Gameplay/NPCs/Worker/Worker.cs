using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections;
using System.Collections.Generic;

public class Worker : MonoBehaviour
{
    [SerializeField] private List<StateString> _statesByString;
    
    [SerializeField] private SpriteRenderer _workerRenderer;
    [SerializeField] private SpriteRenderer _attributeRenderer;

    private State _currentState;

    private Building _assignedBuilding;
    private Job _currentJob;
    private Job _lastJob;

    private NPCsSpritesConfig _npcsConfig;
    private WorkAttributesConfig _attributesConfig;

    private Vector3Int _destinition;

    private WorkerUI _workerUI;

    public State CurrentState => _currentState;

    public void Init(NPCsSpritesConfig npcsConfig, WorkAttributesConfig attributesConfig)
    {
        _npcsConfig = npcsConfig;
        _attributesConfig = attributesConfig;

        ChooseRandomSprite();
    }

    private void Start()
    {
        if(TryGetComponent<WorkerUI>(out WorkerUI workerUI))
        {
            _workerUI = workerUI;
            _workerUI.Init(this);
        }

        ChangeState<IdleState>();
    }

    private void ChooseRandomSprite()
    {
        if(_npcsConfig != null && _npcsConfig.VillagersTypes.Count > 0 && _workerRenderer != null)
        {
            Sprite choosenSprite = _npcsConfig.VillagersTypes[UnityEngine.Random.Range(0, _npcsConfig.VillagersTypes.Count)];

            Debug.Log(choosenSprite);

            _workerRenderer.sprite = choosenSprite;
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
    }

    public void ChangeState<T>(object data = null) where T : State
    {
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

        TryGetJob();
    }

    private void TryGetJob()
    {
        if (_assignedBuilding == null) return;

        if (_currentJob == null && !_assignedBuilding.HaveJob)
        {
            _currentJob = _assignedBuilding.GetAvailableJob(_lastJob);
            if (_currentJob != null)
                StartJob();
        }
    }

    public void AssignToBuilding(Building building)
    {
        _assignedBuilding = building;

        ChangeAttribute(_assignedBuilding.buildingData.jobType, _assignedBuilding.buildingData.resourceType);
    }

    public void StartJob()
    {
        if(_currentJob != null)
        {
            MovingData toBuildingData = new MovingData(_currentJob.BuildingPos, () => JobEnded());
            WorkingData endJobData = new WorkingData(5f, () => AfterJob(toBuildingData));
            MovingData toJobData = new MovingData(_currentJob.JobPos,() => ChangeState<WorkingState>(endJobData));
            
            ChangeState<MovingState>(toJobData);
        }
    }

    private void AfterJob(MovingData toBuildingData)
    {
        //refactor, amount need to move to another place (5)
        ServiceLocator.GetService<EventBus>().Invoke<OnResourceMined>(new OnResourceMined(this, _currentJob.ResourcePos, 5, _assignedBuilding.buildingData.resourceType));
        
        ChangeState<MovingState>(toBuildingData);
    }

    public void JobEnded()
    {
        ChangeState<IdleState>();

        if(_currentJob != null)
            ServiceLocator.GetService<EventBus>().Invoke<OnJobFinished>(new OnJobFinished(_currentJob, this));

        OnJobCompleted();
    }

    public void JobFailed()
    {
        ChangeState<IdleState>();

        OnJobCompleted();
    }

    public void OnJobCompleted()
    {
        _lastJob = _currentJob;
        _currentJob = null;
    }
}
