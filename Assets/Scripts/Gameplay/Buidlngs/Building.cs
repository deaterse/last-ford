using UnityEngine;
using System.Collections;

public class Building : Entity, IDamageable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BuildingUI _buildingUI;

    private BuildingData _buildingData;
    private Vector2Int _gridPos;
    private int _level = 1;

    private bool _isBuilded;

    public BuildingData buildingData => _buildingData;
    public Vector2Int GridPosition => _gridPos;
    public int Level => _level;
    public bool IsBuilded => _isBuilded;

    private void Awake()
    {
        _buildingUI.Init();
    }

    public void Init(BuildingData buildingData, Vector2Int pos)
    {
        _buildingData = buildingData;
        _gridPos = pos;
        _level = 1;
        _isBuilded = false;

        _spriteRenderer.sprite = buildingData.BuildingFrameSprite;

        _buildingUI.OnBuildingBuilded();
        StartBuild();
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
