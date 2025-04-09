using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionInferior : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && this.CompareTag("DownCollision"))
        {
            Debug.Log("colisiono");
            collision.GetComponent<controllerCheckPoint>().volverAlCheckPoint();
        }

        if (collision.CompareTag("Player") && this.CompareTag("CheckPoint"))
        {
            Debug.Log("checkPointactivete");
            collision.GetComponent<controllerCheckPoint>().ActualizarCheckPoint(collision.transform);
            this.GetComponent<BoxCollider2D>().enabled = false;
        }

    }
}
