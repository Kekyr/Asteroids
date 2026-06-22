using UnityEngine;

namespace Factories
{
    public class Factory : IFactory
    {
        public T Create<T>(T prefab, Transform parent) where T : Object
        {
            return GameObject.Instantiate<T>(prefab, parent);
        }
    }
}