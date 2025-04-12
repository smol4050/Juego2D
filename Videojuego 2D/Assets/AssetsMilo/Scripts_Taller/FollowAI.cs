using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowAI : MonoBehaviour
{
    [SerializeField] private Transform player;

    private bool isFacingRight = true;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Stiven")
        {
            this.enabled = false;
        }
        bool isPlayerRight = transform.position.x < player.transform.position.x;
        Flip(isPlayerRight);
    }

    private void Flip(bool isPlayerRight)
    {
        if((isFacingRight && !isPlayerRight) || (!isFacingRight && isPlayerRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
