using Abstractions;
using UnityEngine;

public class MainBuilding : MonoBehaviour, ISelectable, IAttackable
{
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;
    public Transform Position => _transform;

    public Vector3 RallyPoint { get; set; }

    [SerializeField] private float _maxHealth = 1000f;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Transform _transform;

    private float _health = 1000f;

    public void RecieveDamage(int amount)
    {
        if (_health <= 0)
            return;

        _health -= amount;

        if (_health <= 0)
            Destroy(gameObject);
    }
}
