using UnityEngine;

public class Building : Entity, IDamageable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private BuildingData _buildingData;
    private Vector2Int _gridPos;
    private int _level = 1;

    public BuildingData buildingData => _buildingData;
    public Vector2Int GridPosition => _gridPos;
    public int Level => _level;

    public void Init(BuildingData buildingData, Vector2Int pos)
    {
        _buildingData = buildingData;
        _gridPos = pos;
        _level = 1;
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
