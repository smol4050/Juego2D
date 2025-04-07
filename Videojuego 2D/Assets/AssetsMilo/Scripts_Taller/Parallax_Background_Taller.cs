using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_Background_Taller : MonoBehaviour
{

    Transform cam; //Main Camera
    Vector3 camStartPos;
    float distance_x; //jarak antara start camera posisi dan current posisi
    float distance_y;

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;

    float farthestBack;

    [Range(0.01f, 1f)]
    public float parallaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;



        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++) //find the farthest background
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++) //set the speed of bacground
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distance_x = cam.position.x - camStartPos.x;
        distance_y = cam.position.y - camStartPos.y;

        // Que el objeto contenedor general siga a la cámara en X (no en Y)
        transform.position = new Vector3(cam.position.x-0.42f, transform.position.y, transform.position.z);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;

            if (i == 0) // Si es el cielo
            {
                // Parallax solo en X (para textura)
                mat[i].SetTextureOffset("_MainTex", new Vector2(distance_x, 0f) * speed);

                // Pero hacemos que el objeto siga en Y a la cámara (para que siempre esté a su altura)
                Vector3 pos = backgrounds[i].transform.position;
                backgrounds[i].transform.position = new Vector3(pos.x, cam.position.y, pos.z);
            }
            else
            {
                // Capas normales: parallax solo en X
                mat[i].SetTextureOffset("_MainTex", new Vector2(distance_x, 0f) * speed);
            }
        }
    }
}
