using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private NPCBehaviorMode _currentMode = NPCBehaviorMode.Idle;
    [SerializeField] private float _moveSpeed; // later add config for this
    
    private Job _currentJob;

    private Vector3Int _destinition;

    private void Update()
    {
        switch (_currentMode)
        {
            case NPCBehaviorMode.Idle:
                HandleIdle();
                break;
                
            case NPCBehaviorMode.Move:
                HandleMove();
                break;
            case NPCBehaviorMode.Work:
                HandleWork();
                break;
            default:
                Debug.LogWarning("Unknown Behaviour Mode");
                break;
        }
    }

    private void HandleIdle()
    {
        ///...
    }

    private void HandleMove()
    {
        ///...
    }    

    private void HandleWork()
    {
        ///...
    }

    public void StartJob(Job job)
    {
        if(_currentJob != null)
        {
            ServiceLocator.GetEventBus().Invoke<OnJobFailed>(new OnJobFailed(_currentJob));
        }

        _currentJob = job;

        ///...
    }
}
