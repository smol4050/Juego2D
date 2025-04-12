using UnityEngine;
using UnityEngine.UI;

public class SpawnBossTrigger : MonoBehaviour
{
    public GameObject bossObject;
    public Slider healthBarSlider;
    public Collider2D limite3Collider;
    public GameObject spellPrefab;
    public SonidoBoss sonidoBoss;

    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || hasSpawned)
            return;

        hasSpawned = true;

        
        if (limite3Collider != null)
        {
            limite3Collider.isTrigger = false;
            Debug.Log("Trigger de limite3 desactivado.");
        }

        
        if (bossObject != null)
        {
            bossObject.SetActive(true);
            if (healthBarSlider != null)
            {
                healthBarSlider.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("El GameObject del boss no está asignado.");
        }
    }
}

