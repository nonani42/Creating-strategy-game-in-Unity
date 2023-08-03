using UnityEngine;

namespace Abstractions
{
    public interface ISelectable : IHealthHolder, IIconHolder
    {
        Transform Position { get; }
    }
}