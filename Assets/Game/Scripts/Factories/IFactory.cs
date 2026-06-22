using UnityEngine;

namespace Factories
{
    public interface IFactory
    {
        public T Create<T>(T prefab, Transform parent) where T : Object;
    }
}