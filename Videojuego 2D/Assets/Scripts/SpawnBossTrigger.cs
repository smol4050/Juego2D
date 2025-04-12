using UnityEngine;

public class SpawnBossTrigger : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public GameObject skeletonWarrior;
    public GameObject limite3;

    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasSpawned) return;

        if (other.CompareTag("Player"))
        {
            hasSpawned = true;

            GameObject bossInstance = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);

            
            BossController bossController = bossInstance.GetComponent<BossController>();
            bossController.healthBarUI.Hide();

            if (skeletonWarrior != null)
            {
                Destroy(skeletonWarrior);
            }

            if (limite3 != null)
            {
                Collider2D limiteCollider = limite3.GetComponent<Collider2D>();
                if (limiteCollider != null)
                {
                    limiteCollider.isTrigger = false;
                }
            }

            Destroy(gameObject);
        }
    }
}
