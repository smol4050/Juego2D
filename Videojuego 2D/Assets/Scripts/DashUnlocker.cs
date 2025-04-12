using UnityEngine;

public class DashUnlocker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.UnlockDash();
            Destroy(gameObject);
        }
    }
}

