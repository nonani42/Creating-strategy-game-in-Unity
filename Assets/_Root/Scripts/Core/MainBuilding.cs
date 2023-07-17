using Abstractions;
using UnityEngine;

public class MainBuilding : MonoBehaviour, IUnitProducer, ISelectable
{
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;
    public GameObject GameObject => _gameObject;

    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private Transform _unitsParent;

    [SerializeField] private float _maxHealth = 1000f;
    [SerializeField] private Sprite _icon;

    [SerializeField] private GameObject _gameObject;


    private float _health = 1000f;

    public void ProduceUnit()
    {
        Instantiate(_unitPrefab, new Vector3(Random.Range(-10,10), 0, Random.Range(-10, 10)), Quaternion.identity, _unitsParent);
    }
}
