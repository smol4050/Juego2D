using UnityEngine;
public class SpeedBoost : PowerUp
{
    public int speedMultiplier = 2;
    public override void Apply(GameObject player)
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.ApplySpeedBoost(speedMultiplier, duration);
        }

        Destroy(gameObject);
    }
}
