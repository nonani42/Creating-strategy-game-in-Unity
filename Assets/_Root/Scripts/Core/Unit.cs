using Abstractions;
using UnityEngine;

public class Unit : MonoBehaviour, ISelectable
{
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;
    public GameObject GameObject => _gameObject;

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private Sprite _icon;

    [SerializeField] private GameObject _gameObject;

    private float _health = 100f;
}
