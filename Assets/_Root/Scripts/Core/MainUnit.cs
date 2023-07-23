using Abstractions;
using UnityEngine;

public class MainUnit : MonoBehaviour, ISelectable, IAttackable
{
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;
    public Transform Position => _transform;

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Transform _transform;

    private float _health = 100f;
}
