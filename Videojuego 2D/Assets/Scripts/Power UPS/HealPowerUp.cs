using UnityEngine;
public class HealPowerUp : PowerUp
{
    public int healAmount = 20;

    public override void Apply(GameObject player)
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.Heal(healAmount);
        }

        Destroy(gameObject);
    }
}
