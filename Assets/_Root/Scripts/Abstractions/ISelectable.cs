using UnityEngine;

namespace Abstractions
{
    public interface ISelectable : IHealthHolder
    {
        Sprite Icon { get; }
        Transform Position { get; }
    }
}