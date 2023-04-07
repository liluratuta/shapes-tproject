using UnityEngine;

namespace ShapesGame.Services.Popup
{
    public interface IPopupService
    {
        void Show(string text, Vector3 position);
        void Hide();
        void Prewarm();
    }
}