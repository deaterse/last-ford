using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections;
using System.Collections.Generic;

public class Worker : MonoBehaviour
{
    public enum WorkPhase { GoingResource, Working, GoingBuilding }
    public WorkPhase CurrentPhase { get; private set; }

    [SerializeField] private List<StateString> _statesByString;

    private State _currentState;

    private Building _assignedBuilding;
    private Job _currentJob;
    private Job _lastJob;

    private Vector3Int _destinition;

    public State CurrentState => _currentState;

    private void Start()
    {
        ChangeState<IdleState>();
    }

    public void ChangeState<T>(object data = null) where T : State
    {
        Debug.Log($"changed to {typeof(T).Name}");
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

        if (_currentJob == null && !_assignedBuilding.NoResourcesOutside)
        {
            _currentJob = _assignedBuilding.GetAvailableJob(_lastJob);
            if (_currentJob != null)
                StartJob();
        }
    }

    public void AssignToBuilding(Building building)
    {
        _assignedBuilding = building;
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
        ServiceLocator.GetService<EventBus>().Invoke<OnResourceMined>(new OnResourceMined(_currentJob.ResourcePos, 5));
        
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
