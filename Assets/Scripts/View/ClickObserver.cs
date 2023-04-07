using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ShapesGame.View
{
    public class ClickObserver : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Vector2> Clicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(eventData.position);
        }
    }
}