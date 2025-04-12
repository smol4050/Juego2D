using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Limite3Trigger : MonoBehaviour
{
    public Slider healthBarSlider;
    private bool hasActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || hasActivated)
            return;

        hasActivated = true;

        
        if (healthBarSlider != null)
        {
            healthBarSlider.interactable = false;
            healthBarSlider.gameObject.SetActive(true);
            healthBarSlider.value = healthBarSlider.maxValue;
            Debug.Log("Slider de salud activado.");
        }
        else
        {
            Debug.LogWarning("Slider de salud no asignado en el Inspector.");
        }

        
        StartCoroutine(DesactivarTriggerDespuesDeTiempo());
    }

    private IEnumerator DesactivarTriggerDespuesDeTiempo()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<Collider2D>().isTrigger = false;
        Debug.Log("Trigger de limite3 desactivado después de 5 segundos.");
    }
}
