using System;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private int _count;

        [SerializeField] private GameObject _prefab;

        private Queue<GameObject> _instances = new Queue<GameObject>();

        public int Count => _count;

        private void Awake()
        {
            if (_count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_count));
            }

            for (int i = 0; i < _count; i++)
            {
                GameObject instance = Instantiate(_prefab, transform);
                instance.SetActive(false);
                _instances.Enqueue(instance);
            }
        }

        public GameObject Spawn(Vector3 position)
        {
            GameObject instance = _instances.Dequeue();
            
            instance.transform.position = position;
            instance.gameObject.SetActive(true);

            _instances.Enqueue(instance);

            return instance;
        }
    }
}