using UnityEngine;

namespace ShapesGame.Services.Player
{
    public interface IPlayerProvider
    {
        GameObject Player { get; set; }
    }
}