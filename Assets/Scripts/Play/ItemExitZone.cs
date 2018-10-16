using UnityEngine;

namespace Play
{
    public class ItemExitZone : MonoBehaviour 
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Item")) 
                return;

            other.gameObject.SetActive(false);
        }
    }
}