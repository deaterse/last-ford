using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class JobManager : MonoBehaviour
{
    private Dictionary<Worker, Job> _assignedJobs = new();
    private Dictionary<Job, Worker> _assignedJobsRevert = new();

    private List<Job> _freeJobs = new();
    private List<Worker> _freeWorkers = new();

    private void Init()
    {
        ClearAll();

        GameEvents.OnJobFinished += JobFinished;
        GameEvents.OnJobCreated += NewFreeJob;
    }

    private void OnDisable()
    {
        ClearAll();

        GameEvents.OnJobFinished -= JobFinished;
        GameEvents.OnJobCreated -= NewFreeJob;
    }

    private void ClearAll()
    {
        _assignedJobs.Clear();
        _assignedJobsRevert.Clear();
        _freeJobs.Clear();
        _freeWorkers.Clear();
    }

    public void NewFreeJob(Job job)
    {
        if(!job.IsAssigned && !_freeJobs.Contains(job))
        {
            _freeJobs.Add(job);
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
