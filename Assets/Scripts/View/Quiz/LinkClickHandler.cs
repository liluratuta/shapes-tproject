using TMPro;
using UnityEngine;

namespace ShapesGame.View.Quiz
{
    public class LinkClickHandler
    {
        private readonly TMP_Text _label;

        public LinkClickHandler(TMP_Text label)
        {
            _label = label;
        }

        public bool TryHandle(Vector2 clickPosition, out TMP_LinkInfo linkInfo)
        {
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(_label, clickPosition, null);

            if (linkIndex < 0)
            {
                linkInfo = default;
                return false;
            }
            
            linkInfo = _label.textInfo.linkInfo[linkIndex];
            return true;
        }
    }
}