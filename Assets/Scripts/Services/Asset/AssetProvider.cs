using UnityEngine;

namespace ShapesGame.Services.Asset
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Get(string path) => 
            Resources.Load<GameObject>(path);
    }
}