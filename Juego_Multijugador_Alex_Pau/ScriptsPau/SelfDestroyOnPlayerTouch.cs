using UnityEngine;

public class SelfDestroyOnPlayerTouch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate que el Player tiene ese tag
        {
            Destroy(gameObject); // Destruye este objeto
        }
    }
}

