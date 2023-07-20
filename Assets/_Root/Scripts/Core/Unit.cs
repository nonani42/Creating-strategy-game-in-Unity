using Abstractions;
using UnityEngine;

public class Unit : MonoBehaviour, ISelectable
{
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private Sprite _icon;

    private float _health = 100f;
}
