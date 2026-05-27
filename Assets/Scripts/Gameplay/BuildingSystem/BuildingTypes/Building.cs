using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public abstract class Building : Entity, IDamageable
{
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected BuildingUI _buildingUI;
    [SerializeField] protected List<Light2D> _spotLights = new();
    [SerializeField] protected List<Light2D> _spriteLights = new();

    protected BuildingData _buildingData;
    protected Vector2Int _gridPos;
    protected int _level = 1;

    protected bool _isBuilded;
    protected bool _dontHaveJob;

    protected int _avaliableWorkersSlots;
    protected List<Worker> _assignedWorkers = new();

    public BuildingData buildingData => _buildingData;
    public Vector2Int GridPosition => _gridPos;
    public GameObject WorkParticles => _buildingData.Particles;
    public int Level => _level;

    public bool IsBuilded => _isBuilded;
    public bool DontHaveJob => _dontHaveJob;

    public List<Worker> AssignedWorkers => _assignedWorkers;

    public bool HasAvailableSlot => _assignedWorkers.Count < _avaliableWorkersSlots;

    public List<Light2D> SpotLights => _spotLights;
    public List<Light2D> SpriteLights => _spriteLights;

    private static readonly WaitForSeconds waitStep = new WaitForSeconds(0.1f);

    protected void Awake()
    {
        _buildingUI.Init();
    }

    public void Init(BuildingData buildingData, Vector2Int pos)
    {
        SetData(buildingData, pos);
        SetEvents();

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

    private void SetEvents()
    {
        ServiceLocator.GetService<EventBus>().Subscribe<CanDestroyBuilding>(DestroySignal);
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
            yield return waitStep;

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

    public bool AssignWorker(Worker worker)
    {
        if (HasAvailableSlot)
        {
            _assignedWorkers.Add(worker);
            worker.AssignToBuilding(this);

            ServiceLocator.GetService<EventBus>().Invoke<OnWorkerAssigned>(new OnWorkerAssigned(this));

            return true;
        }

        return false;
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

    public void DestroyMethod()
    {
        ServiceLocator.GetService<EventBus>().Invoke(new OnBuildingDestroyed(this));
    }

    private void DestroySignal(CanDestroyBuilding signal)
    {
        if(signal._building == this)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnDestroy()
    {
        ServiceLocator.GetService<EventBus>().Unsubscribe<CanDestroyBuilding>(DestroySignal);
    }

    public abstract Job GetAvailableJob(Job lastJob = null);
}
