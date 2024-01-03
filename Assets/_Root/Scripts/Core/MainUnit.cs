using Abstractions;
using Core.CommandExecutors;
using UnityEngine;

public class MainUnit : MonoBehaviour, ISelectable, IAttackable, IUnit, IDamageDealer, IAutomaticAttacker
{
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;
    public Transform Position => _transform;
    public int Damage => _damage;
    public float VisionRadius => _visionRadius;


    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Transform _transform;
    [SerializeField] private int _damage = 25;
    [SerializeField] private float _visionRadius = 8f;

    [SerializeField] private Animator _animator;
    [SerializeField] private StopCommandExecutor _stopCommand;

    private float _health = 100f;

    private readonly int PlayDead = Animator.StringToHash("PlayDead");


    public void RecieveDamage(int amount)
    {
        if (_health <= 0)
        {
            return;
        }

        _health -= amount;

        if (_health <= 0)
        {
            _animator.SetTrigger(PlayDead);
            Invoke(nameof(Destroy), 1f);
        }
    }

    private async void Destroy()
    {
        await _stopCommand.ExecuteSpecificCommand(new StopCommand());
        Destroy(gameObject);
    }
}
