using UnityEngine;

public class Building : Entity, IDamageable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private BuildingData _buildingData;
    private int _level;
    
    

    public void ChangeColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public void TakeDamage(int damage)
    {
        DecreaseHealth(damage);
    }
}
