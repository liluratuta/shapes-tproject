using UnityEngine;

namespace ShapesGame.Services.Asset
{
    public interface IAssetProvider
    {
        GameObject Get(string path);
    }
}