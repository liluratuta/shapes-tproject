using UnityEngine;

namespace ShapesGame.Factories
{
    public interface IPlayerFactory
    {
        GameObject Create(Vector3 position);
    }
}