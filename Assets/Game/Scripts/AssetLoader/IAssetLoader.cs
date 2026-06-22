using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AssetLoader
{
    public interface IAssetLoader
    {
        public UniTask<T> Load<T>(string assetId);
        public void UnLoad<T>(T asset);
    }
}