using UnityEngine;
using System.Collections;

public class Worker : MonoBehaviour
{
    private Building _assignedBuilding;
    private Job _currentJob;

    private Vector3Int _destinition;

    public void AssignBuilding(Building building)
    {
        _assignedBuilding = building;
    }

    private void Update()
    {
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
        yield return new WaitForSeconds(3f);

        ServiceLocator.GetService<EventBus>().Invoke<OnJobFinished>(new OnJobFinished(_currentJob, this));
    }

    public void OnJobCompleted()
    {
        _currentJob = null;
    }
}
