using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class WorkingState: State
{
    private Worker _worker;
    private JobType _jobType;
    private float _workingTime;
    private bool _animationStarted = false;
    private Vector3Int _resourcePos;
    private System.Action _onReachedCallback;

    private WaitForSeconds waitWorkingTime;

    public override void SetData(object data)
    {
        if(TryGetComponent<Worker>(out Worker worker))
        {
            _worker = worker;
        }

        if (data is WorkingData workingData)
        {
            _workingTime = workingData.Time;
            waitWorkingTime = new WaitForSeconds(_workingTime);
            _jobType = workingData._jobType;
            _resourcePos = workingData._resourcePos;
            _onReachedCallback = workingData.OnReached;
        }
    }

    public override void ClearData()
    {
        _onReachedCallback = null;
    }

    public override void Enter()
    {
        StartCoroutine(StartWork());
    }

    public override void OnUpdate()
    {
        if(_jobType == JobType.Mining)
        {
            if(_worker.ResourceIsGone())
            {
                _worker.JobFailed();
                return;
            }

            if(!_animationStarted)
            {
                ServiceLocator.GetService<TerrainMapManager>().AnimateResource(_resourcePos);
                _animationStarted = true;
            }
        }
    }

    public override void Exit()
    {
        _animationStarted = false;
        ServiceLocator.GetService<TerrainMapManager>().StopAnimation(_resourcePos);
        
        StopAllCoroutines();
    }

    private IEnumerator StartWork()
    {
        yield return waitWorkingTime;
    
        _onReachedCallback?.Invoke();
    }
}