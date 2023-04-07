using UnityEngine;

namespace ShapesGame.Infrastructure.Factories
{
    public interface IPlayerFactory
    {
        GameObject Create(Vector3 position);
    }
}