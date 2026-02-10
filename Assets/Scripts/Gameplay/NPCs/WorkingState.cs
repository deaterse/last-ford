using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class WorkingState: State
{
    private Worker _worker;
    private float _workingTime;
    private System.Action _onReachedCallback;

    public override void SetData(object data)
    {
        if(TryGetComponent<Worker>(out Worker worker))
        {
            _worker = worker;
        }

        if (data is WorkingData workingData)
        {
            _workingTime = workingData.Time;
            _onReachedCallback = workingData.OnReached;
        }
    }

    public override void Enter()
    {
        StartCoroutine(StartWork());
    }

    public override void OnUpdate()
    {
        // check here if resource is not mined by other worker, if mined, need to find another job.
        if(_worker.ResourceIsGone())
        {
            _worker.JobFailed();
            return;
        }
    }

    public override void Exit()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartWork()
    {
        yield return new WaitForSeconds(_workingTime);
    
        _onReachedCallback?.Invoke();
    }
}