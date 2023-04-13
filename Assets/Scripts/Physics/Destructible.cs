using UnityEngine;
namespace Boxfight2.Physics
{
    public class Destructible : MonoBehaviour
    {
        public void DestroyMe()
        {
            // Destroy the game object this script is attached to
            Destroy(gameObject);
        }
    }
}
