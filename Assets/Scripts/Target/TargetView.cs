using UnityEngine;

namespace ShapesGame.Target
{
    public class TargetView : MonoBehaviour, ITarget
    {
        public Vector3 Position => transform.position;
    }
}