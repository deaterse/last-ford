using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Building : Entity, IDamageable
{
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected BuildingUI _buildingUI;

    protected BuildingData _buildingData;
    protected Vector2Int _gridPos;
    protected int _level = 1;

    protected bool _isBuilded;
    protected bool _haveJob;

    protected int _avaliableWorkersSlots;
    protected List<Worker> _assignedWorkers = new();

    public BuildingData buildingData => _buildingData;
    public Vector2Int GridPosition => _gridPos;
    public int Level => _level;

    public bool IsBuilded => _isBuilded;
    public bool HaveJob => _haveJob;

    public List<Worker> AssignedWorkers => _assignedWorkers;

    public bool HasAvailableSlot => _assignedWorkers.Count < _avaliableWorkersSlots;

    protected void Awake()
    {
        _buildingUI.Init();
    }

    public void Init(BuildingData buildingData, Vector2Int pos)
    {
        SetData(buildingData, pos);

        _spriteRenderer.sprite = buildingData.BuildingFrameSprite;

        _buildingUI.OnBuildingBuilded();
        StartBuild();
    }

    protected void SetData(BuildingData buildingData, Vector2Int pos)
    {
        _buildingData = buildingData;
        _gridPos = pos;
        _level = 1;
        _isBuilded = false;
    }

    protected void StartBuild()
    {
        StartCoroutine(Build());
    }

    protected IEnumerator Build()
    {
        float buildingTime = _buildingData.BuildingTime;
        
        float timer = 0;
        float step = 0.1f;
        while(timer < buildingTime)
        {
            yield return new WaitForSeconds(step);

            timer += step;
            _buildingUI.UpdateSlider(timer);
        }

        BuildFinish();
        _buildingUI.HideSlider();
    }

    protected void BuildFinish()
    {
        _isBuilded = true;
        _spriteRenderer.sprite = buildingData.GetLevel(_level).UpgradeSprite;
        _avaliableWorkersSlots = _buildingData.GetLevel(_level).WorkerSlots;

        ServiceLocator.GetService<EventBus>().Invoke<OnBuildingFinished>(new OnBuildingFinished(this, buildingData.GetLevel(_level).WorkerSlots));
    }

    public void AssignWorker(Worker worker)
    {
        if (HasAvailableSlot)
        {
            _assignedWorkers.Add(worker);
            worker.AssignToBuilding(this);
        }
    }

    public void UnassignWorker(Worker worker)
    {
        _assignedWorkers.Remove(worker);
    }

    public void ChangeColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public void TakeDamage(int damage)
    {
        DecreaseHealth(damage);
    }
    
    public void Upgrade()
    {
        if(_level < _buildingData.MaxLevel)
        {
            _level++;
            _spriteRenderer.sprite = _buildingData.GetLevel(_level).UpgradeSprite;

            Debug.Log("Succesfully upgraded.");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public abstract Job GetAvailableJob(Job lastJob = null);
}
