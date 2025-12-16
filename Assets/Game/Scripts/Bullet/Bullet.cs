using PlayerBase;
using UnityEngine;

namespace BulletBase
{
    public class Bullet : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Ship>(out Ship ship))
            {
                return;
            }
            
            gameObject.SetActive(false);
        }
    }
}