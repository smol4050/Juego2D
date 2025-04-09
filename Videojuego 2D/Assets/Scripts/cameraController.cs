using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform player;
    public float velocidadCamera = 0.025f;
    public Vector3 desplazamiento;


    private void LateUpdate()
    {
        Vector3 posicionDeseada = player.position + desplazamiento;
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCamera);

        transform.position = posicionSuavizada;

    }
}
