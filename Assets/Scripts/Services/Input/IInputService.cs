using System;
using UnityEngine;

namespace ShapesGame.Services.Input
{
    public interface IInputService : IDisposable
    {
        event Action<Vector2> GameFieldClicked;
        Vector2 Axis { get; }
        bool IsSelected { get; }
    }
}