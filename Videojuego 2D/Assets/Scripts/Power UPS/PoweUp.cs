using UnityEngine;
public abstract class PowerUp : MonoBehaviour
{
    public float duration = 5f; //el power-up es temporal
    public abstract void Apply(GameObject player);
}