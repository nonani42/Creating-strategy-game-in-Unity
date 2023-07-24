using Abstractions;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public sealed class MouseInteractionPresenter : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private Camera _camera;

    [Inject] private ValueBase<ISelectable> _selectedObject;
    [Inject] private ValueBase<IAttackable> _attackedObject;
    [Inject] private ValueBase<Vector3> _groundClicksRMB;

    [Inject(Id = "Ground")] private Transform _groundTransform;


    private Plane _groundPlane;

    private int leftMB = 0;
    private int rightMB = 1;

    private void Start()
    {
        _groundPlane = new Plane(_groundTransform.up, 0);
    }

    private void Update()
    {
        if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButtonUp(1))
            return;

        if (_eventSystem.IsPointerOverGameObject())
            return;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);

        if (Input.GetMouseButtonUp(leftMB))
        {
            if(CheckHits(hits, out ISelectable selectable))
                _selectedObject.SetValue(selectable);
        }

        if (Input.GetMouseButtonUp(rightMB))
        {
            if (_groundPlane.Raycast(ray, out var enter))
                _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);

            if (CheckHits(hits, out IAttackable selectable))
                _attackedObject.SetValue(selectable);
        }
    }

    private bool CheckHits<T>(RaycastHit[] hits, out T result) where T : class
    {
        result = default;

        if (hits.Length != 0)
        {
            result = hits
            .Select(hit => hit.collider.GetComponentInParent<T>())
            .FirstOrDefault(c => c != null);
        }

        return result != default;
    }
}
