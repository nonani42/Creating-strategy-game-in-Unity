using Abstractions;
using UnityEngine;

public class MainBuilding : MonoBehaviour, ISelectable
{
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;

    [SerializeField] private float _maxHealth = 1000f;
    [SerializeField] private Sprite _icon;

    private float _health = 1000f;
}
