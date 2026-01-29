using UnityEngine;

public class Entity : MonoBehaviour
{
    private int _maxHealth;
    private int _health;
    
    public int MaxHealth => _maxHealth;
    public int Health => _health;

    public virtual void DecreaseHealth(int value)
    {
        int _damage = Mathf.Abs(value);

        _health -= _damage;

        if(_health < 0)
        {
            _health = 0;
        }
    }

    public virtual void IncreaseHealth(int value)
    {
        int _value = Mathf.Abs(value);

        _health += _value;

        if(_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }
}