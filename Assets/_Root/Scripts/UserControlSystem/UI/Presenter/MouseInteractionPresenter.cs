using Abstractions;
using System.Linq;
using UniRx;
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

        var notBlockedByUiClicksStream = Observable.EveryUpdate().Where(_ => !_eventSystem.IsPointerOverGameObject());

        var leftclickRaysObservable = notBlockedByUiClicksStream.
            Where(_ => Input.GetMouseButtonDown(leftMB)).
            Select(_ =>_camera.ScreenPointToRay(Input.mousePosition)).
            Select(ray => Physics.RaycastAll(ray));

        leftclickRaysObservable.Subscribe(hits =>
        {
            if (CheckHits(hits, out ISelectable selectable))
                _selectedObject.SetValue(selectable);
        });

        var rightClickRaysObservable = notBlockedByUiClicksStream.
            Where(_ => Input.GetMouseButtonDown(rightMB)).
            Select(_ =>_camera.ScreenPointToRay(Input.mousePosition)).
            Select(ray => (ray, Physics.RaycastAll(ray)));

        rightClickRaysObservable.Subscribe(cortege =>
        {
            var (ray, hits) = cortege;

            if (_groundPlane.Raycast(ray, out var enter))
                _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);

            if (CheckHits(hits, out IAttackable selectable))
                _attackedObject.SetValue(selectable);
        });
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
