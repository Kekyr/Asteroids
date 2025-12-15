using System;
using PlayerBase;
using UnityEngine;

namespace AsteroidBase
{
    public class Asteroid: MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Ship>(out Ship ship) == false)
            {
                return;
            }
            Debug.Log("CollisionEnter!");
            gameObject.SetActive(false);
        }
    }
}