using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : Entity, IDamageable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BuildingUI _buildingUI;

    private BuildingData _buildingData;
    private Vector2Int _gridPos;
    private int _level = 1;

    private bool _isBuilded;
    private bool _noResourcesOutside;

    private int _avaliableWorkersSlots;
    private List<Worker> _assignedWorkers = new();
    private List<Vector3Int> _assignedResources = new();

    public BuildingData buildingData => _buildingData;
    public Vector2Int GridPosition => _gridPos;
    public int Level => _level;

    public bool IsBuilded => _isBuilded;
    public bool NoResourcesOutside => _noResourcesOutside;

    public List<Worker> AssignedWorkers => _assignedWorkers;
    public List<Vector3Int> AssignedResources => _assignedResources;

    public bool HasAvailableSlot => _assignedWorkers.Count < _avaliableWorkersSlots;

    private void Awake()
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

    private void SetData(BuildingData buildingData, Vector2Int pos)
    {
        _buildingData = buildingData;
        _gridPos = pos;
        _level = 1;
        _isBuilded = false;
    }

    public void StartBuild()
    {
        StartCoroutine(Build());
    }

    private IEnumerator Build()
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

    private void BuildFinish()
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

    public Job GetAvailableJob(Job lastJob = null)
    {
        if(!_noResourcesOutside)
        {
            if(lastJob != null)
            {
                Vector3Int resPos = lastJob.resourceNeighbour.resourcePos;
                if(ServiceLocator.GetService<TerrainMapManager>().IsResource(resPos))
                {
                    ResourceNeighbour currentResNeighbour = lastJob.resourceNeighbour;

                    return new Job(this, _buildingData.jobType, new Vector3Int(_gridPos.x, _gridPos.y, 0), currentResNeighbour);
                }
            }

            ResourceNeighbour positionData = ResourcePosition();

            if(!IsNoneResource(positionData))
            {
                return new Job(this, _buildingData.jobType, new Vector3Int(_gridPos.x, _gridPos.y, 0), positionData);
            }

            _noResourcesOutside = true;
        }
        return null;
    }

    private bool IsNoneResource(ResourceNeighbour rn)
    {
        return rn.resourceType == ResourceType.None;
    }

    private ResourceNeighbour ResourcePosition()
    {
        //refactor
        if(buildingData.jobType == JobType.Wood_Cutting)
        {
            ResourceLocator rl = ServiceLocator.GetService<ResourceLocator>();
            ResourceNeighbour resPos = rl.GetCellNearResource(_gridPos, ResourceType.Wood, 5);

            return resPos;
        }
        else if(buildingData.jobType == JobType.Stone_Mining)
        {
            ResourceLocator rl = ServiceLocator.GetService<ResourceLocator>();
            ResourceNeighbour resPos = rl.GetCellNearResource(_gridPos, ResourceType.Stone, 3);

            return resPos;
        }

        //refactor
        return ResourceNeighbour.None;
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
}
