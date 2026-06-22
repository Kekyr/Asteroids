using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Game;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AssetLoader
{
    public class LocalAssetLoader : IAssetLoader
    {
        public UniTask<T> Load<T>(string assetId)
        {
            var handle = Addressables.LoadAssetAsync<T>(assetId);
            var asset = handle.ToUniTask();
            return asset;
        }

        public void UnLoad<T>(T asset)
        {
            Addressables.Release(asset);
        }
    }
}