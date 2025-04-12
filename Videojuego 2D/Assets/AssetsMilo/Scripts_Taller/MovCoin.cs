using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCoin : MonoBehaviour
{

    public float velocidad = 2f;
    public float altura = 0.3f;
    private Vector3 puntoA;
    private Vector3 puntoB;
    private float t = 0;

    void Start()
    {
        puntoA = transform.position;
        puntoB = new Vector3(transform.position.x, transform.position.y + altura, transform.position.z);
    }

    void Update()
    {
        t += Time.deltaTime * velocidad;
        transform.position = Vector3.Lerp(puntoA, puntoB, Mathf.PingPong(t, 1));
    }
}

