using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections;
using System.Collections.Generic;

public class Worker : MonoBehaviour
{
    [SerializeField] private List<StateString> _statesByString;

    private State _currentState;

    private Building _assignedBuilding;
    private Job _currentJob;

    private Vector3Int _destinition;

    public State CurrentState => _currentState;

    private void Start()
    {
        ChangeState<IdleState>();
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
                    
                _currentState.Enter();
                return;
            }
        }
    }

    private void Update()
    {
        _currentState?.OnUpdate();

        if (_assignedBuilding == null) return;
        if (_currentJob == null)
        {
            _currentJob = _assignedBuilding.GetAvailableJob();
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
            StartCoroutine(DoingJob());
        }
    }

    //TEST PURPOSE
    IEnumerator DoingJob()
    {
        transform.position = _assignedBuilding.transform.position;

        yield return new WaitForSeconds(3f);

        ServiceLocator.GetService<EventBus>().Invoke<OnJobFinished>(new OnJobFinished(_currentJob, this));
    }

    public void OnJobCompleted()
    {
        _currentJob = null;
    }
}
