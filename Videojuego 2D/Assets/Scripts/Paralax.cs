using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier;

    private Transform cameraTransform; 
    private Vector2 previusCamera; 

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        previusCamera = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previusCamera.x)*parallaxMultiplier; 
        transform.Translate(new Vector3 (deltaX, 0, 0));
        previusCamera = cameraTransform.position;

    }
}
