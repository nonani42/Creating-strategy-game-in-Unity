using Abstractions;
using UnityEngine;
using UserControlSystem;

public class OutlinePresenter : MonoBehaviour
{
    [SerializeField] private SelectableValue _selectedValue;
    private Outline _outline;

    private void Start()
    {
        _selectedValue.OnSelected += OnSelected;
        OnSelected(_selectedValue.CurrentValue);
    }

    private void OnSelected(ISelectable obj)
    {
        if(_outline != null)
            _outline.enabled = false;

        if (obj != null)
        {
            _outline = obj.GameObject.TryGetComponent(out _outline) ? _outline : obj.GameObject.AddComponent<Outline>();
            _outline.enabled = obj != null;
        }
    }
}
