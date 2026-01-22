using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class JobManager : MonoBehaviour
{
    private Dictionary<Worker, Job> _assignedJobs = new();
    private Dictionary<Job, Worker> _assignedJobsRevert = new();

    private List<Job> _freeJobs = new();
    private List<Worker> _freeWorkers = new();

    public void Init()
    {
        ClearAll();

        GameEvents.OnJobFinished += JobFinished;
        GameEvents.OnJobCreated += NewFreeJob;

        GameEvents.OnWorkerSpawned += NewFreeWorker;

        StartCoroutine(FindJobToWorkers());
    }

    private void OnDisable()
    {
        ClearAll();

        GameEvents.OnJobFinished -= JobFinished;
        GameEvents.OnJobCreated -= NewFreeJob;

        GameEvents.OnWorkerSpawned -= NewFreeWorker;
    }

    private void ClearAll()
    {
        _assignedJobs.Clear();
        _assignedJobsRevert.Clear();
        _freeJobs.Clear();
        _freeWorkers.Clear();

        StopAllCoroutines();
    }

    private IEnumerator FindJobToWorkers()
    {
        while(true)
        {
            for(int i = 0; i < _freeJobs.Count; i++)
            {
                if(i < _freeWorkers.Count)
                {
                    AssignJob(_freeWorkers[i], _freeJobs[i]);
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void NewFreeJob(Job job)
    {
        if(!job.IsAssigned && !_freeJobs.Contains(job))
        {
            _freeJobs.Add(job);
        }
    }

    public void NewFreeWorker(Worker worker)
    {
        if(!_assignedJobs.ContainsKey(worker) && !_freeWorkers.Contains(worker))
        {
            _freeWorkers.Add(worker);
        }
    }

    public void AssignJob(Worker worker, Job job)
    {
        if(_assignedJobs.ContainsKey(worker))
        {
            Job oldJob = _assignedJobs[worker];

            oldJob.AssignJob(false);
            NewFreeJob(oldJob);
        }

        if(_freeJobs.Contains(job))
        {
            _freeJobs.Remove(job);
        }
        if(_freeWorkers.Contains(worker))
        {
            _freeWorkers.Remove(worker);
        }

        _assignedJobs[worker] = job;
        _assignedJobsRevert[job] = worker;
    }

    private void JobFinished(Job job)
    {
        // also add logic of getting resources after job (using jobtype)

        if(_assignedJobsRevert.ContainsKey(job))
        {
            Worker currentWorker = _assignedJobsRevert[job];

            _assignedJobs.Remove(currentWorker);
            _assignedJobsRevert.Remove(job);

            _freeWorkers.Add(currentWorker);
        }
        else if(_freeJobs.Contains(job))
        {
            _freeJobs.Remove(job);
        }
    }

    private void JobFailed(Job job)
    {
        if(_assignedJobsRevert.ContainsKey(job))
        {
            Worker currentWorker = _assignedJobsRevert[job];

            _assignedJobs.Remove(currentWorker);
            _assignedJobsRevert.Remove(job);

            _freeWorkers.Add(currentWorker);
            _freeJobs.Add(job);
        }
    }
}
